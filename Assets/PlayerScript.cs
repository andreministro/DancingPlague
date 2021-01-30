using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public Collider2D standCollider;
    public Collider2D crouchCollider;
    public GameObject checkCeilingCollider;
    public float playerspeed= 4.2f;
    private bool facingRight = true;
    public int JumpPower;
    private float moveX;
    public float sanityPenalty;
    public GameObject cavalo, demon, inventario, demonCollider, MarketToVillageDoor;
    public GameObject luzTocha;
    public GameObject dancerMove;

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
    public static bool isLit = false;
    private bool isLitWalking;
    public bool canJump = true;
    private bool canUncrouch = true;

    public bool movePlayer;
    public bool cutScene;
    public int cutSceneNumber = 1;
    public TextMeshProUGUI EText;
    public TextMeshProUGUI dialogueText;

    private string currentSceneName;
    private bool label;

    public bool enterMonster = false;

    public bool cutSceneMarket = true;
    public bool cutSceneBackHome = true;
    private bool firstLitBackHome=true;
    private static bool firstBigForest = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSceneName = SceneManager.GetActiveScene().name;
        sanityPenalty = 0;
        if (SceneManager.GetActiveScene().name == "LVL1 - Home" && gameObject.GetComponent<BarsController>().isDead()==false)
        {
            movePlayer = false;
            cutScene = true;
            label = true;
        }
        else if (SceneManager.GetActiveScene().name == "LVL2 - Big Forest" && inventario.GetComponent<InventoryDisplay>().checkItemList("Balde") && firstBigForest)
        {
            movePlayer = false;
            cutScene = true;
        }
        else if (SceneManager.GetActiveScene().name == "LVL4 - BackHome")
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

        if ((SceneManager.GetActiveScene().name == "LVL2 - Big Forest"))
        {
            FlipPlayer();
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (cutScene)
        {
                animator.SetBool("IsCoveringEW", false);
                animator.SetBool("IsCoveringEars", false);
                animator.SetBool("IsCrawling", false);
                animator.SetBool("IsCrouching", false);
                animator.SetBool("IsLitWalking", false);
                isLitWalking = false;
                isCrawling = false;
                isCoveringEars = false;
                isCoveringEW = false;
                isCrouching = false;
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

    public void SetMonster()
    {
        firstMonster = true;
    }

    void Player_Movement()
    {

        //Controls 
        if (gameObject.transform.position.x > 8.3 && (SceneManager.GetActiveScene().name == "LVL2 - Forest") && firstMonster==true && inventario.GetComponent<InventoryDisplay>().checkItemList("Balde"))
        {
            demonCollider.SetActive(true);
            StartCoroutine(monsterAppear());
            firstMonster = false;
        }
        moveX = Input.GetAxis("Horizontal");
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

        if (moveX == 0 || isCrouching == true || isCoveringEars == true)
        {
            animator.SetBool("IsRunning", false);
            isWalking = false;
        }
        else
        {
            animator.SetBool("IsCoveringEW", false);
            animator.SetBool("IsCrawling", false);

            if (isCrouching != true)
            {
                animator.SetBool("IsRunning", true);
                isWalking = true;
            }
        }

        //JUMP
        if (Input.GetButtonDown("Jump") && isGrounded == true && canJump == true)
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
        if(Input.GetButtonDown("Crouch") && isGrounded == true)
        {
            isCrouching = true;
            animator.SetBool("IsCrouching", true);
            standCollider.enabled = false;
        } 
        else if (Input.GetButtonUp("Crouch") && canUncrouch == true)
        {
            Debug.Log("1");
            isCrouching = false;
            animator.SetBool("IsCrouching", false);
            standCollider.enabled = true;
        }
        else if (canUncrouch == false && isCrouching == true)
        {
            Debug.Log("2");
            isCrouching = true;
            //Debug.Log(isCrouching);
            standCollider.enabled = false;
            animator.SetBool("IsCrouching", true);

        }


        if (moveX != 0 && isCrouching == true)
        {
            isCrawling = true;
            animator.SetBool("IsCrawling", true);
        }
        else if (moveX == 0)
        {
            isCrawling = false;
            animator.SetBool("IsCrawling", false);
           /* if (isCrawling == false && canUncrouch ==true)
                animator.SetBool("IsCrouching", false);*/
        }

        
         if (canUncrouch == true && isCrouching == true && offTrigger == true)
         {
             isCrouching = false;
             animator.SetBool("IsCrouching", false);
             standCollider.enabled = true;
             offTrigger = false;
             Debug.Log("3");
         }
         

        //COVER EARS
        if (Input.GetButton("Ears") && isGrounded == true)
        {
            canJump = false;
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
            canJump = true;
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

        if (Input.GetButtonDown("Torch") && isGrounded == true && isLit == false && gameObject.GetComponent<PlayerInter>().inventory.GetComponent<InventoryDisplay>().checkItemList("Torcha"))
        {
            canJump = false;
            animator.SetBool("IsLit", true);
            isLit = true;
            luzTocha.SetActive(true);
            SoundManager.PlaySound("tochastart");
            if (firstLitBackHome && SceneManager.GetActiveScene().name == "LVL4 - BackHome")
            {
                firstLitBackHome = false;
                movePlayer = false;
                cutScene = true;
            }
        }
        else if (Input.GetButtonDown("Torch") && isGrounded == true && isLit == true)
        {
            canJump = true;
            animator.SetBool("IsLit", false);
            isLit = false;
            luzTocha.SetActive(false);
        }

        if (moveX != 0 && isLit == true && isGrounded == true)
        {
            isLitWalking = true;
            animator.SetBool("IsLitWalking", true);
        }
        else
        {
            isLitWalking = false;
            animator.SetBool("IsLitWalking", false);
        }

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
    public bool getLit()
    {
        return isLit;
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
            FlipPlayer();
            string newText = "pWh...han?..?..!?\npWait... Did I really just see that?\npNo way that was real.. I must be hallucinating!\npThis isn't something I should be dealing...\npIs this the reason everyone is acting like that?\npHope my family is doing fine.. I must get back home.";
            StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true, false));
            cutScene = false;
            firstBigForest = false;
        }
        else if (SceneManager.GetActiveScene().name == "LVL4 - BackHome" && isLit==false)
        {
            if (cutSceneBackHome)
            {
                cutScene = false;
                cutSceneBackHome = false;
                StartCoroutine(CutSceneBackHome());
            }
        }
        else if (SceneManager.GetActiveScene().name == "LVL4 - BackHome" && isLit == true)
        {
            string newText = "pWhere is everyone?!\npI need to find them.";
            StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true, false));
            cutScene = false;
        }
    }
    private IEnumerator CutSceneBackHome()
    {
        FlipPlayer();
        string newText = "pHey dear, you have no idea what just happened today.\npHello?\npI can't see a damm thing. I need some light.";
        StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true, false));
        yield return new WaitUntil(() => (dialogueText.text == ""));
        if (inventario.GetComponent<InventoryDisplay>().checkItemList("Torcha")){
            gameObject.GetComponent<PlayerInter>().pressETxt.text = "Light the torch (Press 1)";
        }
        else
        {
            newText = "Maybe I need to craft something to give me light.";
            StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, false, false));
        }
    }
    private IEnumerator CutSceneMarket()
    {
        FlipPlayer();
        cavalo.GetComponent<Animator>().SetTrigger("HorseWalking");
        //yield return new WaitUntil(() => (dialogueText.text == ""));
        yield return new WaitForSeconds(2.0f);
        string newText = "-What is this filthy plesant doing in here?\n-Think you can just come and steal our food?\npI  am not filthy I just want to buy something for my family to eat.\n-GOSH! HOW DARE A MERE PLEBE ANSWER ME BACK!\n-PREPARE FOR YOUR PUNISHMENT!";
        StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true, false));

        // pessoas do mercado comeca a aproximar enquanto dançam
        yield return new WaitUntil(() => (dialogueText.text == ""));
        yield return new WaitForSeconds(1.8f);
        dancerMove.GetComponent<Collider2D>().enabled = false;
        dancerMove.SetActive(true);
        dancerMove.GetComponent<Animator>().SetTrigger("DancersMove");
        newText = "-...What? ..What is going on? Why are you all dancing?\n-GET AWAY FROM MY HORSE!";
        StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true, false));

        //horse runs back behind the staircase
        yield return new WaitUntil(() => (dialogueText.text == ""));
        cavalo.GetComponent<Animator>().SetTrigger("HorseBaza");

        //jogador agora pode se mexer
        dancerMove.GetComponent<Collider2D>().enabled = true;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ceiling1")
        {
            canUncrouch = false;
        }
        
    }
    private bool offTrigger = false;
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag== "Ceiling1")
        {
            canUncrouch = true;
            offTrigger = true;
        }
    }
    public void clearStaticVariables()
    {
        isLit = false;
        firstBigForest = true;
        firstMonster = true;
    }
}