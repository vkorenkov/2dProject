using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Animation LoseAnimation;
    [SerializeField] PlayerPrefs playerPrefs;
    [SerializeField] Collected playerCollected;

    private void Awake()
    {
        // Подписка на событие изменения доступности управления
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>().DeathEvent += MenuScript_ControlEnableEvent;
    }

    private void MenuScript_ControlEnableEvent(bool isAlive)
    {
        // Запуск анимации меню поражения
        LoseAnimation.Play();
    }

    public void RestartGame()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerCollected.KilledEnemiesCount = 0;
        playerCollected.CollectedObjectsCount = 0;
        playerCollected.allBonuses = playerCollected.loadCount;
        playerPrefs.CurrentHealth = player.GetComponent<HealthManager>().playerPrefs.MaxHealth;
        // Загрузка сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
