using UnityEngine;
using UnityEngine.SceneManagement;


public class Bandeira : MonoBehaviour
{
    public string sceneToLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bandeira"))
        {
            SceneManager.LoadScene(sceneToLoad); 
        }
    }
}