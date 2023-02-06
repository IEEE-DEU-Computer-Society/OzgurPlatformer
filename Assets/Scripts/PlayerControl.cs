using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
public class PlayerControl : MonoBehaviour
{   
    //known bugs:
    //wall climbing - coyote time for double jump

    //includes:
    //horizontal movement and jump
    //double jump, wall jump
    //coyote time, variable jump and jump buffering
    
    //TODO: wall stick, bug fix

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
    
    //double jump variable
    public int extraJumpCounter = 1;
    
    //wall jump variables
    public bool isWalled;
    public GameObject face;
    public GameObject face2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = GameObject.Find("CharacterFeet");
        face = GameObject.Find("CharacterFace");
        face2 = GameObject.Find("CharacterFace2");
    }
    void Update()
    {
        //horizontal move
        moveInput = Input.GetAxisRaw("Horizontal");
        if (isGrounded || !isWalled)
        {
            if (moveInput != 0)
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        //horizontal move
        
        //
        isGrounded = Physics2D.OverlapBox(feet.transform.position, feet.transform.localScale, 0,
            LayerMask.GetMask("Ground"));
        //
        
        //
        isWalled = Physics2D.OverlapBox(face.transform.position, face.transform.localScale, 0,
            LayerMask.GetMask("Wall")) || Physics2D.OverlapBox(face2.transform.position, face2.transform.localScale, 0,
            LayerMask.GetMask("Wall"));
        //

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
        if (coyoteTimer > 0f && jumpBufferTimer > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        //jump
        
        //double jump
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && extraJumpCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed*1.25f);
            extraJumpCounter--;
        }
        //double jump
        
        //variable jump height
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y >0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            coyoteTimer = 0f;
            jumpBufferTimer = 0f;
        }
        //variable jump height
        
        //wall jump
        if (coyoteTimer > 0f && jumpBufferTimer > 0f && isWalled)
        {
            rb.velocity = new Vector2(moveInput * jumpSpeed, jumpSpeed);
        }
        //wall jump
    }
}