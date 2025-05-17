using UnityEngine;
using System.Collections;

public class human : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    private bool isPaused = false;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
    }

    void Update()
    {
        if (isPaused || isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
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