using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public int maxLives = 7;
    public int lives;

    public Image[] heart;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
    }

    void Update()
    {
        HealhtLogic();
    }

    void HealhtLogic()
    {
        if (lives > maxLives)
        {
            lives = maxLives;
        }

        for (int i = 0; i < heart.Length; i++)
        {
            if (i < lives)
            {
                heart[i].sprite = fullHeart;
            }
            else
            {
                heart[i].sprite = emptyHeart;
            }

            if (i < maxLives)
            {
                heart[i].enabled = true;
            }
            else
            {
                heart[i].enabled = false;
            }
        }
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
    }


}