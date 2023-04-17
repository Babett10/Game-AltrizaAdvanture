using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f; // Kecepatan karakter

    private Rigidbody2D rb;
    private float moveInput;
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, 10, 0);
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);  
    }
}
