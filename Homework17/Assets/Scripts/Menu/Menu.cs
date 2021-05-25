using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// Запускает игру
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    /// <summary>
    /// Закрывает приложение игры
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
