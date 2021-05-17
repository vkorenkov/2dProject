using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Animation LoseAnimation;

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
        // Загрузка сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
