using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TextMeshProUGUI EText;
    public TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movePlayer = false;
        cutScene = true; 
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        //Player_Movement();
        if (cutScene)
        {
            float stopPosition = 2.18f;
            moveX = 0.8f;
            if (gameObject.transform.position.x < stopPosition)
            {
                if (moveX == 0)
                {
                    animator.SetBool("IsRunning", false);
                }
                else
                {
                    animator.SetBool("IsRunning", true);
                }
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerspeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                moveX = 0.0f;
                if (moveX == 0)
                {
                    animator.SetBool("IsRunning", false);
                }
                else
                {
                    animator.SetBool("IsRunning", true);
                }
                string newText = "mBom dia homem da minha vida!!<3\nsPai, comi o pao:(\npcabrao de merda.";
                FlipPlayer();
                StartCoroutine(gameObject.GetComponent<PlayerInter>().displayDialogueText(newText, true));
                //End of CutScene
                cutScene = false;
                gameObject.GetComponent<PlayerInter>().playerInteractionsEnabled = true;
            }
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
            isCoveringEars = false;
            animator.SetBool("IsCoveringEars", false);
        }

        if (isCoveringEars == true)
        {
            rb.velocity = new Vector2(moveX * 0, rb.velocity.y);
        }

        //Player Direction
        if (moveX < 0.0f && facingRight == true)
            FlipPlayer();
        else if (moveX > 0.0f && facingRight == false)
            FlipPlayer();

        EText.transform.position = new Vector2(transform.position.x+3.0f,transform.position.y+11.2f);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Jump()
    {
        rb.velocity = Vector2.up * JumpPower;
    }

}