using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerInverterDirecao : MonoBehaviour
{
    public float velocidade;
    private Vector2 direcao = Vector2.right; // Começa indo para a direita

    // Tag do objeto com o qual a colisão inverte o movimento
    public string tagInversora = "Inversora";

    void Update()
    {
        // Move o player constantemente na direção atual
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se colidir com objeto com a tag correta, inverte a direção
        if (collision.gameObject.CompareTag(tagInversora))
        {
            direcao *= -1;
            Flip();// Inverte a direção
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}

