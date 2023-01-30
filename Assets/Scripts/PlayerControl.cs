using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
public class PlayerControl : MonoBehaviour
{   
    //TODO edge detection and bugfix
    //TODO double jump, wall jump
    //known bugs: sticking to the wall
    
    //includes:
    //horizontal movement and jump
    //coyote time, variable jump and jump buffering

    //basic variables
    public Rigidbody2D rb;
    public float speed = 10f;
    public float jumpSpeed = 15f;
    private float moveInput;
    
    //isGrounded variables
    public bool isGrounded;
    public GameObject feet;
    
    //coyote time variables
    public float coyoteTimer;
    public float coyoteLimit = 0.1f;
    
    //jump buffer variables
    public float jumpBufferTimer;
    public float jumpBufferLimit = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = GameObject.Find("CharacterFeet");
    }
    void Update()
    {
        //horizontal move
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        //horizontal move
        
        //
        isGrounded = Physics2D.OverlapBox(feet.transform.position, feet.transform.localScale, 0,
            LayerMask.GetMask("Ground"));
        //
        
        //coyote time
        if (isGrounded)
        {
            coyoteTimer = coyoteLimit;
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
        if (coyoteTimer > 0f && jumpBufferTimer > 0f)
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
    }
}