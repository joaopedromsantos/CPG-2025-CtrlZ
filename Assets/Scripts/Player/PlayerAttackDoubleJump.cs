using UnityEngine;

public class DoubleJumpAttack : MonoBehaviour
{
    public float areaRange = 1.5f;
    public LayerMask enemyLayers;

    private PlayerController playerController;
    private PlayerLife playerLife;
    private Rigidbody2D rb;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        playerLife = GetComponent<PlayerLife>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Caiu em cima do inimigo!");
            if (rb.linearVelocity.y < 0 && playerController.lastJumpWasDouble)
            {
                Debug.Log("Ataque em area!");
                playerController.lastJumpWasDouble = false;
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, areaRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("Inimigo atingido!");
                    enemy.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
                    playerController.score++;
                    playerLife.lives++;
                    playerLife.HealhtLogic();
                    playerController.UpdateScoreUI();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, areaRange);
    }
}