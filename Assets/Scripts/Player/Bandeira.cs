using UnityEngine;
using UnityEngine.SceneManagement;


public class Bandeira : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bandeira"))
        {
            SceneManager.LoadScene("History_2"); 
        }
    }
}