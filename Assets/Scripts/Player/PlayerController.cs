using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // VARIAVEIS PRIVADAS
    private Rigidbody2D rb;
    private float moveX;

    // VARIAVEIS PUBLICAS       
    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveX = Input.GetAxisRaw("Horizontal");
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            addJumps = 1;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else if (addJumps > 0 && Input.GetButtonDown("Jump"))
        {
            Jump();
            addJumps--;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f); // Direita
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // Esquerda
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            addJumps = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}       

