using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private PlayerStats stats;

    public int fallBoundary = -20;

    public float jumpDelay = 0.7f;

    [SerializeField]
    private LayerMask whatIsGround;
    private bool grounded = false;

    private bool facingRight = true;
    private bool jump;
    //Double Jump
    private bool doubleJump = false;
    public static bool canDoubleJump = false;

    private float previousY;
    public bool fallingCheck = true;

    //References
    Rigidbody2D rb;
    Rigidbody2D rbAux;
    SpriteRenderer playerGraphics;
    Animator animator;

    AudioManager audioManager;

    // Use this for initialization
    void Start () {

        stats = PlayerStats.instance;

        rb = GetComponent<Rigidbody2D>();
        playerGraphics = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audiomanager found");
        }

        previousY = transform.position.y;
    }

    void Update()
    {
        if (!jump)
        {
            jump = Input.GetButtonDown("Jump");
        }

        CheckIfFallingAnimation();
    }


    void FixedUpdate () {

        if (animator.GetBool("Dead"))
        {
            return;
        }

        //FALL DOWN
        if (transform.position.y <= fallBoundary )
        {
            animator.SetBool("Dead", true);
            StartCoroutine(GameMaster.instance.PlayerDown(this));
        }

        CheckGrounded();

        //MOVING
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * PlayerStats.movementSpeed, rb.velocity.y);
        //Set the animator speed
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        //FLIP
        if ((rb.velocity.x > 0 && !facingRight) || (rb.velocity.x < 0 && facingRight))
        {
            Flip();
        }

        //JUMP & DOUBLE JUMP
        if (jump)
            if (grounded)
                Jump();
            else if (doubleJump)
            {
                doubleJump = false;
                Jump();
            }

        jump = false;
    }

    private void Jump()
    {
        transform.parent = null;
        rb.velocity = new Vector2(rb.velocity.x,0f);
        rb.AddForce(new Vector2(0f, stats.jumpForce));
        AudioManager.instance.PlaySound("Jump");
    }

    private void Flip()
    {
        facingRight = !facingRight;
        playerGraphics.flipX = !playerGraphics.flipX;
    }

    private void CheckGrounded()
    {
        /*Get the playerBack to calculate the jump from there
        float playerBack;
        if (facingRight)
        {
            playerBack = transform.position.x - jumpDelay;
        }
        else
        {
            playerBack = transform.position.x + jumpDelay;
        }
        
        //Cast a short (0.53f) ray to the ground to check if there are collisions
        grounded = Physics2D.Raycast(new Vector3(playerBack,transform.position.y,0f),Vector2.down, 0.53f, whatIsGround);*/

        grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.53f, whatIsGround);

        if (grounded)
        {
            animator.SetBool("Grounded", true);
            animator.SetBool("Falling", false);

            if (canDoubleJump) doubleJump = true;
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }

    private void CheckIfFallingAnimation()
    {
        if (previousY > transform.position.y && fallingCheck)
        {
            animator.SetBool("Falling", true);
        }
        previousY = transform.position.y;
    }

    //Check collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //STOMP
            float pointBetweenPlayerAndEnemy = (transform.position.y + collision.transform.position.y) /2;
            //TODO: Player Position - 0.2 is a bad way to calculate the player's feet position in order to stomp an enemy properly
            if ((transform.position.y -0.2) > pointBetweenPlayerAndEnemy)
            {
                //Delete previous forces
                rb.Sleep();
                rb.WakeUp();

                //Add a new jump
                animator.SetBool("Falling",false);
                rb.AddForce(new Vector2(0f, stats.jumpForce));
                AudioManager.instance.PlaySound("Jump");
                //Activate doubleJump after stomp
                if (canDoubleJump) doubleJump = true;
                jump = false;
            }
            
            //DEAD
            else
            {
                StartCoroutine(GameMaster.instance.KillPlayer(this));
            }  
        }

        if (collision.gameObject.tag == "Spike")
        {
            //DEAD
            StartCoroutine(GameMaster.instance.KillPlayer(this));
        }

        //Make the movement above a moving platform smoother and realistic
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
            //Disable the checking of falling state
            fallingCheck = false;
            rb.interpolation = RigidbodyInterpolation2D.None;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //It worked with only that condition, but IDKW it stops working and I have to do the trick below
        if (collision.gameObject.tag == "MovingPlatform")
        {
            if(!Physics2D.Raycast(transform.position, Vector2.down, 0.53f, whatIsGround))
            { 
                transform.parent = null;
                fallingCheck = true;
                rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
            }

        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            //DEAD

            StartCoroutine(GameMaster.instance.KillPlayer(this));
        }
    }


    public void ActiveDoubleJump()
    {
        canDoubleJump = true;
    }
}
