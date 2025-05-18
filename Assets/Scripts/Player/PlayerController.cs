using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // VARIAVEIS PRIVADAS
    private Rigidbody2D rb;
    private float moveX;
    private Animator anim;
    private bool isAttacking = false;
    private PlayerLife playerLife;
    private bool isTakingDamage = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int score = 0;
    public TMP_Text scoreText;
    public bool lastJumpWasDouble = false;


    // VARIAVEIS PUBLICAS       
    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float damageCooldown = 1f;
    public float jumpForce;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveX = Input.GetAxisRaw("Horizontal");
        playerLife = GetComponent<PlayerLife>();
        UpdateScoreUI();
    }

    void Update()
    {   
        moveX = Input.GetAxis("Horizontal");
        Attack();

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
        if (isAttacking)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isRunning", false);
            return;
        }

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

        if (!isGrounded)
            lastJumpWasDouble = true;
    else
        lastJumpWasDouble = false;
    }

    void Attack()
    {
        if (!isAttacking && Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
            StartCoroutine(AttackRoutine());

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
                score++;
                UpdateScoreUI();
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.6f);
        isAttacking = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
            isGrounded = true;
            addJumps = 1;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TryTakeDamage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TryTakeDamage();
        }
    }

    void TryTakeDamage()
    {
        if (!isTakingDamage && !isAttacking)
        {
            playerLife.LoseLife();
            StartCoroutine(DamageCooldown());
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    IEnumerator DamageCooldown()
    {
        isTakingDamage = true;
        yield return new WaitForSeconds(damageCooldown);
        isTakingDamage = false;
    }
}       

