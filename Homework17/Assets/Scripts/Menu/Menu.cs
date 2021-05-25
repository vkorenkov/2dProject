using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// ��������� ����
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    /// <summary>
    /// ��������� ���������� ����
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
