using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private string menuName;

    public void goToMenu()
    {
        SceneManager.LoadScene(menuName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
