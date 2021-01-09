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

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool isCrouching;
    private bool isCoveringEars;

    public bool movePlayer;
    public bool cutScene;
    public int cutSceneNumber = 1;
    public TextMeshProUGUI EText;
    public TextMeshProUGUI dialogueText;

    private string currentSceneName;
    private bool label;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSceneName = SceneManager.GetActiveScene().name;
        sanityPenalty = 0;
        if (currentSceneName == "LVL1 - Home")
        {
            movePlayer = false;
            cutScene = true;
            label = true;
        }
        else if(currentSceneName== "LVL1 - BackHome")
        {
            gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
            movePlayer = true;
            cutScene = false;
            FlipPlayer();
            StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText("I'm going to buy some bread.", false, false));
        }
        else// if (currentSceneName == "LVL1 - Village")
        {
            gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
            movePlayer = true;
            cutScene = false;
        }

        FlipPlayer();
        FlipPlayer();

    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        //Player_Movement();
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

    void Player_Movement()
    {
        //Controls 
        moveX = Input.GetAxis("Horizontal");
        if (moveX < 0)
        {
            if(moveX+sanityPenalty<0)
                moveX += sanityPenalty;
        }
        else if (moveX > 0)
        {
            if (moveX - sanityPenalty > 0)
                moveX -= sanityPenalty;
        }
        rb.velocity = new Vector2(moveX * playerspeed, rb.velocity.y);
        
        if (moveX == 0)
        {
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsRunning", true);
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
        if(Input.GetButtonDown("Crouch") && isGrounded == true)
        {
            isCrouching = true;
            animator.SetBool("IsCrouching", true);
            standCollider.enabled = false;
        }

        if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", false);
            standCollider.enabled = true;
        }

        if(isCrouching == true)
        {
            rb.velocity = new Vector2(moveX * 0, rb.velocity.y);
        }

        //COVER EARS
        if(Input.GetButtonDown("Ears"))
        {
            isCoveringEars = true;
            animator.SetBool("IsCoveringEars", true);
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
            rb.velocity = new Vector2(moveX * 0, rb.velocity.y);
        }

        //Player Direction
        if (moveX < 0.0f && facingRight == true)
            FlipPlayer();
        else if (moveX > 0.0f && facingRight == false)
            FlipPlayer();
    }

    void FlipPlayer()
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
        if (currentSceneName == "LVL1 - Home")
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
                    newText = "mHello dear, did you sleep well?\npYes. Nights are getting colder.\nmYes, I know...\npCan you pass me the bread?";
                else
                    newText = "pI can't seem to find it.\nsDad, I was so hungry and ate it, I'm sorry.\nm It's okay. I guess we can afford to buy another one, dear?\npYes, maybe one or two more. I will go to the market. Be right back";

                FlipPlayer();
                StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true,false));
                //End of CutScene
                cutScene = false;
                gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
            }
        }
    }

}