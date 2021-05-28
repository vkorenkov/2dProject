using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] PlayerPrefs playerPrefs;
    [SerializeField] Collected collected;

    /// <summary>
    /// Запускает игру
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
    /// Закрывает приложение игры
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
