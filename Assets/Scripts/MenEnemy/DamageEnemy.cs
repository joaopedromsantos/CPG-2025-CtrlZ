using UnityEngine;
using System.Collections;

public class human : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;

    private bool isPaused = false;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPaused || isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PauseMovement());
        }
    }

    IEnumerator PauseMovement()
    {
        isPaused = true;
        yield return new WaitForSeconds(0.5f);
        isPaused = false;
    }

    // Método público para ser chamado pelo PlayerController
    public void Die()
    {
        Debug.Log("Método Die chamado em: " + gameObject.name);
        if (!isDead)
            StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        transform.eulerAngles = new Vector3(0, 0, 90);
        // anim.SetTrigger("Die"); // Se tiver animação de morte
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}