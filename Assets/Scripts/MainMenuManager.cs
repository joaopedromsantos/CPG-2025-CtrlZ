using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameLevelName;

    public void Play()
    {
        SceneManager.LoadScene(gameLevelName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
