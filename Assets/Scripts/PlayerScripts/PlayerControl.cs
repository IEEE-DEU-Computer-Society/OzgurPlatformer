using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //assign
    public PlayerState state;
    public GameObject feet;
    public GameObject right;
    public GameObject left;
    public Rigidbody2D rb;
    
    //jump selection - SELECT ONE AND ONLY - make it true to select
    public bool variableJumpHeight = true;
    public bool longJump;
    
    //wall jump selection - SELECT ONE AND ONLY - make it true to select
    public bool wallJumpV1;
    public bool wallJumpV2 = true;

    //movement variables
    public float speed = 10f;
    public float jumpSpeed = 15f;
    private float moveInput;
    
    //check variables
    public RaycastHit2D groundCheck;
    public RaycastHit2D wallCheckRight;
    public RaycastHit2D wallCheckLeft;

    //coyote time variables
    public float coyoteTimer;
    public float coyoteLimit = 0.1f;
    
    //jump buffer variables
    public float jumpBufferTimer;
    public float jumpBufferLimit = 0.1f;
    
    //long jump variable
    public Vector2 longJumpCheck;
    
    //double jump variable
    public int extraJumpCounter = 1;
    
    //wall jump variable
    public float jumpDirection;

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        state.isIdle = state.isGrounded && moveInput == 0;
        state.isMoving = moveInput != 0;

        //horizontal move
        if (!state.isGrappled && !state.isWallJumping)
        {
            if (state.isGrounded || !state.isWalled)
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }
        }
        //horizontal move
        
        //hook move
        else if(state.isGrappled)
        {
            rb.AddForce(new Vector2(moveInput * 2f,0));
        }
        //hook move

        //ground check
        groundCheck = Physics2D.Raycast(feet.transform.position, Vector2.down, 0.1f);
        if (groundCheck.collider != null)
        {
            state.isGrounded = groundCheck.collider.CompareTag("Ground");
        }
        
        else
        {
            state.isGrounded = false;
        }
        //ground check
        
        //wall check
        wallCheckRight = Physics2D.Raycast(right.transform.position, Vector2.right, 0.1f);
        wallCheckLeft = Physics2D.Raycast(left.transform.position, Vector2.left, 0.1f);

        if (wallCheckRight.collider != null)
        {
            state.isRightWalled = wallCheckRight.collider.CompareTag("Wall");
        }
        
        else
        {
            state.isRightWalled = false;
        }

        if (wallCheckLeft.collider != null)
        {
            state.isLeftWalled = wallCheckLeft.collider.CompareTag("Wall");
        }

        else
        {
            state.isLeftWalled = false;
        } 
        state.isWalled = state.isLeftWalled || state.isRightWalled;
        //wall check

        //coyote time +
        if (state.isGrounded)
        {
            coyoteTimer = coyoteLimit;
            
            extraJumpCounter = 1;
            state.isJumping = false;
            state.isWallJumping = false;
        }
        
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
        //coyote time  +

        //jump and wall jump buffer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = jumpBufferLimit;
        }

        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }
        //jump buffer

        //jump
        if (coyoteTimer > 0 && jumpBufferTimer > 0f && !state.isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            state.isJumping = true;
        }
        //jump and wall jump
        
        //long jump
        if (longJump)
        {
            if (state.isGrounded)
            {
                longJumpCheck = feet.transform.position;
                state.isLongJumping = false;
            }

            if (Math.Abs(feet.transform.position.y - (longJumpCheck.y + 2.5f)) < 0.25f && rb.velocity.y > 0f)
            {
                if (!Input.GetKey(KeyCode.Space))
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    coyoteTimer = 0f;
                    jumpBufferTimer = 0f;
                }
            
                else
                {
                    state.isLongJumping = true;
                }
            }
        }
        //long jump

        //variable jump height
        if (variableJumpHeight)
        {
            if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                coyoteTimer = 0f;
                jumpBufferTimer = 0f;
            }
        }
        //variable jump height
        
        //double jump for long jump
        if (longJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !state.isGrounded && extraJumpCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed*1.25f);
                if (coyoteTimer < 0)
                {
                    extraJumpCounter--;
                }

                longJumpCheck = feet.transform.position;
                if (Math.Abs(feet.transform.position.y - (longJumpCheck.y + 2.5f)) < 0.25f && rb.velocity.y > 0f)
                {
                    if (!Input.GetKey(KeyCode.Space))
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0f);
                        coyoteTimer = 0f;
                        jumpBufferTimer = 0f;
                    }
                }
            }
        }
        //double jump for long jump

        //double jump for variable jump height
        if (variableJumpHeight)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !state.isGrounded && extraJumpCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed*1.25f);
                if (coyoteTimer < 0)
                {
                    extraJumpCounter--;
                }
            }
        }
        //double jump for variable jump height

        //wall jump
        if (state.isLeftWalled)
        {
            jumpDirection = 1f;
        }

        else if (state.isRightWalled)
        {
            jumpDirection = -1f;
        }

        if (!state.isSticked && state.isWalled && !state.isGrounded)
        {
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0f;
        }

        state.isSticked = state.isWalled && !state.isGrounded;

        if (state.isSticked)
        {
            state.isWallJumping = true;
            state.isJumping = false;

            if (moveInput == jumpDirection * -1 && jumpBufferTimer > 0f)
            {
                rb.gravityScale = 4f;
                rb.velocity = new Vector2(jumpDirection * jumpSpeed * 0.3f, jumpSpeed);
                state.isWallJumping = false;
            }
            
            else if (jumpBufferTimer > 0f)
            {
                rb.velocity = new Vector2(jumpDirection * jumpSpeed, jumpSpeed);
                rb.gravityScale = 4f;
            }
        }
        //wall jump
    }
}