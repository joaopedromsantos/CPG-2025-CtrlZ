using UnityEngine;

public class DoubleJumpAttack : MonoBehaviour
{
    public float areaRange = 1.5f; // Raio do ataque em �rea
    public LayerMask enemyLayers;   // Defina igual ao do PlayerController

    private PlayerController playerController;
    private Rigidbody2D rb;
    private bool lastJumpWasDouble = false;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detecta se foi um double jump (n�o est� no ch�o e usou o pulo extra)
        if (Input.GetButtonDown("Jump"))
        {
            if (!playerController.isGrounded && playerController.addJumps > 0)
            {
                lastJumpWasDouble = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset ao tocar o ch�o
        if (collision.gameObject.CompareTag("Ground"))
        {
            lastJumpWasDouble = false;
        }

        // Se cair em cima do inimigo ap�s double jump
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (rb.linearVelocity.y < 0 && lastJumpWasDouble)
            {
                // Ataque em �rea
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, areaRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
                    playerController.score++;
                    playerController.UpdateScoreUI();
                }
                lastJumpWasDouble = false; // Evita m�ltiplos ataques
            }
        }
    }

    // Visualiza��o do raio de ataque no editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, areaRange);
    }
}