using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // VARIAVEIS PRIVADAS
    private Rigidbody2D rb;
    private float moveX;
    private Animator anim;

    // VARIAVEIS PUBLICAS       
    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        Attack();
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
        if (moveX > 0)
        {
            anim.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f); // Direita
        }
        if (moveX < 0)
        {
            anim.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // Esquerda
        } 
        if (moveX == 0)
         {
            anim.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        anim.SetBool("isJumping", true);    
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
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

