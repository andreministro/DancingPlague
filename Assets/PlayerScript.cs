using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public Collider2D standCollider;
    public Collider2D crouchCollider;
    public float playerspeed;
    private bool facingRight = true;
    public int JumpPower;
    private float moveX;
    public float sanityPenalty;
    public GameObject cavalo, demon, inventario, demonCollider, MarketToVillageDoor;
    
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool isCrouching;
    private bool isCrawling;
    private bool isCoveringEars;
    private bool isCoveringEW;
    private bool isWalking;

    public bool movePlayer;
    public bool cutScene;
    public int cutSceneNumber = 1;
    public TextMeshProUGUI EText;
    public TextMeshProUGUI dialogueText;

    private string currentSceneName;
    private bool label;

    public bool enterMonster = false;

    public bool cutSceneMarket = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSceneName = SceneManager.GetActiveScene().name;
        sanityPenalty = 0;
        if (SceneManager.GetActiveScene().name == "LVL1 - Home")
        {
            movePlayer = false;
            cutScene = true;
            label = true;
        }
        else if (SceneManager.GetActiveScene().name == "LVL2 - Big Forest")
        {
            movePlayer = false;
            cutScene = true;
        }
        else
        {
            //gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
            movePlayer = true;
            cutScene = false;
        }
        if ((SceneManager.GetActiveScene().name == "LVL2 - Big Forest")) FlipPlayer();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (cutScene)
        {
            cutscene_Controller(currentSceneName);
        }
        else
        {
            if (movePlayer)
            {
                Player_Movement();
            }
        }
    }

    private static bool firstMonster = true;

    void Player_Movement()
    {
        //Controls 
        if(gameObject.transform.position.x > 8.3 && (SceneManager.GetActiveScene().name == "LVL2 - Forest") && firstMonster==true && inventario.GetComponent<InventoryDisplay>().checkItemList("Balde"))
        {
            demonCollider.SetActive(true);
            StartCoroutine(monsterAppear());
            firstMonster = false;
        }
        moveX = Input.GetAxis("Horizontal");
       /* if(Input.GetButtonDown("Horizontal"))
        {
            SoundManager.PlaySound("dirt_step");
        } */
        if (moveX < 0)
        {
            if (moveX + sanityPenalty < 0)
            {
                moveX += sanityPenalty;
            }
            isWalking = true;
        }
        else if (moveX > 0)
        {
            if (moveX - sanityPenalty > 0)
            {
                moveX -= sanityPenalty;
            }
        }
        rb.velocity = new Vector2(moveX * playerspeed, rb.velocity.y);

        if (moveX == 0 || Input.GetButton("Crouch") || Input.GetButton("Ears"))
        {
            animator.SetBool("IsRunning", false);
            isWalking = false;
        }
        else
        {
            animator.SetBool("IsCoveringEW", false);
            animator.SetBool("IsCrawling", false);
            animator.SetBool("IsRunning", true);
            isWalking = true;
        }

        //JUMP
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            animator.SetTrigger("TakeOf");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            Jump();
        }

        if (isGrounded == true)
        {
            animator.SetBool("IsJumping", false);
        }
        else 
        {
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                Jump();
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
            
        }

        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        //CROUCH
        if(Input.GetButton("Crouch") && isGrounded == true)
        {
            isCrouching = true;
            animator.SetBool("IsCrouching", true);
            standCollider.enabled = false;

            if (moveX!=0)
            {
                isCrawling = true;
                animator.SetBool("IsCrawling", true);
            }
            else
            {
                isCrawling = false;
                animator.SetBool("IsCrawling", false);
            }
        }

        if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", false);
            standCollider.enabled = true;
        }

        //COVER EARS
        if(Input.GetButton("Ears") && isGrounded == true)
        {
            isCoveringEars = true;
            animator.SetBool("IsCoveringEars", true);

            if (moveX != 0)
            {
                isCoveringEW = true;
                animator.SetBool("IsCoveringEW", true);
            }
            else
            {
                isCoveringEW = false;
                animator.SetBool("IsCoveringEW", false);
            }
        }

        if (Input.GetButtonUp("Ears"))
        {
            gameObject.GetComponent<BarsController>().isCoveringEars = false;
            isCoveringEars = false;
            animator.SetBool("IsCoveringEars", false);
        }

        if (isCoveringEars == true)
        {
            gameObject.GetComponent<BarsController>().isCoveringEars = true;
        }
        else
        {
            gameObject.GetComponent<BarsController>().isCoveringEars = false;
        }

        //Torch

       /* if(Input.GetButtonDown("Torch"))
        {
            animator.SetBool("");
        }*/

        //Player Direction
        if (moveX < 0.0f && facingRight == true)
            FlipPlayer();
        else if (moveX > 0.0f && facingRight == false)
            FlipPlayer();
    }

    public void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScaleParent = gameObject.transform.localScale;
        Vector2 localScaleChild= new Vector2(0,0);
        localScaleParent.x *= -1;
        foreach (Transform child in transform)
        {
            localScaleChild = child.transform.localScale;
        }
        transform.localScale = localScaleParent;
        foreach (Transform child in transform)
        {
            child.transform.localScale= localScaleChild;
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * JumpPower;
    }

    private void cutscene_Controller(string currentSceneName)
    {
        if (SceneManager.GetActiveScene().name == "LVL1 - Home")
        {
            if (label)
            {
                StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText("1518. Strasbourg, France", false, true));
                label = false;
            }
            float stopPosition = 2.18f;
            if (gameObject.transform.position.x < stopPosition && cutScene)
            {
                moveX = 0.8f;
                if (facingRight == false)
                    FlipPlayer();

                animator.SetBool("IsRunning", true);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerspeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            } 
            else {
                moveX = 0.0f;
                animator.SetBool("IsRunning", false);

                string newText;
                if (cutSceneNumber == 1)
                {
                    gameObject.GetComponent<PlayerInter>().pressETxt.text = "Press E to Interact";
                    newText = "mHello dear, did you sleep well?\npYes...\npNights are getting colder.\npI am kinda hungry.. last night the meals were just some herbs.\npAre there some breadcrumbs leftovers?\nmI think so, have a look around dear.";
                }
                else
                    newText = "pI can't seem to find it.\nsDad..\nsI was so hungry that I ate them.\nsPlease don't be mad at me. I'm sorry.\nmIt's okay. We know it's hard sweety.\nmCan we afford to get some more, dear?\npI think so...\npI will go check out the market. Be right back.";

                FlipPlayer();
                StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true,false));
                //End of CutScene
                cutScene = false;
                gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "LVL3 - Market")
        {
            //cavalo aparece detras da subida e diz para noS
            if (cutSceneMarket)
            {
                cutSceneMarket = false;
                gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = false;
                animator.SetBool("IsRunning", false);
                MarketToVillageDoor.SetActive(false);
                StartCoroutine(CutSceneMarket());
            }
        }
        else if (SceneManager.GetActiveScene().name == "LVL2 - Big Forest")
        {
            string newText = "pWh...han?..?..!?\npWait... Did I really just see that?\npNo way that was real.. I must be hallucinating!\npIs this the reason everyone is acting like that?\npHope my family is okay.. I must get back home.";
            StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true, false));
            cutScene = false;
        }
    }

    private IEnumerator CutSceneMarket()
    {
        FlipPlayer();
        cavalo.GetComponent<Animator>().SetTrigger("HorseWalking");
        //yield return new WaitUntil(() => (dialogueText.text == ""));
        yield return new WaitForSeconds(2.0f);
        string newText = "-What is this filthy plesant doing in here?\n-Think you can just come and steal our food?\npI am not filthy and I just bought this.\n-GOSH! HOW DARE A MERE PLEBE ANSWER ME BACK!\n-PREPARE FOR YOUR PUNISHMENT!";
        StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true, false));

        // pessoas do mercado comeca a aproximar enquanto dançam
        
        yield return new WaitUntil(() => (dialogueText.text == ""));
        newText = "-...What? ..What is going on? Why are you all dancing?\n-GET AWAY FROM MY HORSE!";
        StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true, false));
        
        //horse runs back behind the staircase
        
        yield return new WaitUntil(() => (dialogueText.text == ""));
        newText = "I just spoke with these people... What on devils name is going on?";
        StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, false, false));

        //jogador agora pode se mexer
        gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
        cutScene = false;
        //dançarinos continuam se a aproximar ate o jogador ir para o lado esquerdo , se ele for contra eles perde vida e morre. 

    }

    private IEnumerator monsterAppear()
    {
        demon.GetComponent<Animator>().SetTrigger("IsAppearing");
        yield return new WaitUntil(() => enterMonster);
        gameObject.GetComponent<BarsController>().morteMonstro();
    }
}