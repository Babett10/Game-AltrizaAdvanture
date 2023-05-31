using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f; // Kecepatan karakter
    public float jumpForce = 12f; //Tinggi lompat
    private int maxJumps = 2;
    private int _jumpleft;
    private float moveInput;
    private bool isGrounded;

    private enum MovementState { idle, running, jumping, falling, doubleJump }

    [SerializeField] private LayerMask JumpableGround;
   
    
    private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
         coll = GetComponent<BoxCollider2D>();
         sprite = GetComponent<SpriteRenderer>();
         anim = GetComponent<Animator>();
         _jumpleft = maxJumps;
    }


    void Update()
    {
        if (IsGrounded() && rb.velocity.y <= 0)
        {
            _jumpleft = maxJumps;
        }

        if (Input.GetButtonDown("Jump") && _jumpleft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _jumpleft -= 1;
            Debug.Log("lompat =" +  _jumpleft);
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);  
        
        UpdateAnimationState();
        
    }

    private void UpdateAnimationState() //Animasi
    {
        MovementState state;

        if (moveInput > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false; //hadap kanan
        }
        else if (moveInput < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true; //hadap kiri
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }


}
