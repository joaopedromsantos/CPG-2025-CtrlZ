using UnityEngine;

public class DamageInObstacles : MonoBehaviour
{
    public string waterTag = "Water"; // Defina a tag do seu Tilemap de água
    public float damageInterval = 1f; // Tempo entre danos em segundos

    private PlayerLife playerLife;
    private bool isInWater = false;
    private float damageTimer = 0f;

    void Start()
    {
        playerLife = GetComponent<PlayerLife>();
    }

    void Update()
    {
        if (isInWater)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                playerLife.LoseLife();
                damageTimer = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(waterTag))
        {
            isInWater = true;
            damageTimer = damageInterval; // Dano imediato ao entrar
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(waterTag))
        {
            isInWater = false;
            damageTimer = 0f;
        }
    }
}