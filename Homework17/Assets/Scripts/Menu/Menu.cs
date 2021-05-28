using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] PlayerPrefs playerPrefs;
    [SerializeField] Collected collected;

    /// <summary>
    /// ��������� ����
    /// </summary>
    public void StartGame()
    {
        collected.KilledEnemiesCount = 0;
        collected.CollectedObjectsCount = 0;
        collected.allBonuses = 0;
        playerPrefs.CurrentHealth = playerPrefs.MaxHealth;
        playerPrefs.BulletCount = 0;

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
