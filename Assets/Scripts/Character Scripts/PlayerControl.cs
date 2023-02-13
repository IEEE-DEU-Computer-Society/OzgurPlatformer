using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
public class PlayerControl : MonoBehaviour
{   
    //includes:
    //horizontal movement and jump
    //double jump, wall stick, wall jump
    //coyote time, variable jump and jump buffering
    //hook movements

    //assign
    public GrappleHook grappleHook;
    public GameObject feet;
    public GameObject face;
    public GameObject face2;
    public LayerMask ground;
    public LayerMask wall;
    public Rigidbody2D rb;

    //basic variables
    public float speed = 10f;
    public float jumpSpeed = 15f;
    private float moveInput;
    
    //isGrounded variables
    public bool isGrounded;
    
    //coyote time variables
    public float coyoteTimer;
    public float coyoteLimit = 0.1f;
    
    //jump buffer variables
    public float jumpBufferTimer;
    public float jumpBufferLimit = 0.1f;
    
    //double jump variable
    public int extraJumpCounter = 1;
    
    //wall jump variables
    public bool isWalled;
    public bool isRightWalled;
    public bool isLeftWalled;
    public bool isSticked;
    public bool isWallJumping;
    public float jumpDirection;

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        //horizontal move
        if (!grappleHook.isGrappled && !isWallJumping)
        {
            if (isGrounded || !isWalled)
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }
        }
        //horizontal move
        
        //hook move
        else if(grappleHook.isGrappled)
        {
            rb.AddForce(new Vector2(moveInput * 2f,0));
        }
        //hook move

        //ground check
        isGrounded = Physics2D.OverlapBox(feet.transform.position, feet.transform.localScale, 0, ground);
        //ground check
        
        //wall check
        isLeftWalled = Physics2D.OverlapBox(face.transform.position, face.transform.localScale, 0, wall);
        isRightWalled = Physics2D.OverlapBox(face2.transform.position, face2.transform.localScale, 0, wall);
        isWalled = isLeftWalled || isRightWalled;
        //wall check

        //coyote time
        if (isGrounded || isWalled)
        {
            coyoteTimer = coyoteLimit;
            extraJumpCounter = 1;
        }
        
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
        //coyote time
        
        //jump buffer
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
        if (coyoteTimer > 0 && jumpBufferTimer > 0f && !isSticked && !isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        //jump
        
        //variable jump height
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y >0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            coyoteTimer = 0f;
            jumpBufferTimer = 0f;
        }
        //variable jump height
        
        //double jump
        if (Input.GetKeyDown(KeyCode.Space) && !isWallJumping &&!isSticked && !isGrounded && extraJumpCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed*1.25f);
            if (coyoteTimer < 0)
            {
                extraJumpCounter--;
            }
        }
        //double jump

        //wall stick
        if (isWalled && !isGrounded && !isSticked)
        {
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0f;
            isSticked = true;
        }

        if (!isWalled)
        {
            isSticked = false;
        }
        //wall stick
        
        //wall jump
        if (isLeftWalled)
        {
            jumpDirection = 1f;
        }

        else if (isRightWalled)
        {
            jumpDirection = -1f;
        }

        if (isSticked)
        {
            isWallJumping = true;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(jumpDirection * jumpSpeed, jumpSpeed);
            }
        }
        
        if (!isSticked)
        {
            rb.gravityScale = 4f;
        }

        if (isGrounded)
        {
            isWallJumping = false;
        }
        //wall jump
    }
}