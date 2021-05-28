using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterLevelTransitions : MonoBehaviour
{
    /// <summary>
    /// Главная камера
    /// </summary>
    Camera mainCamera;

    /// <summary>
    /// Обазначение левой стороны эерана
    /// </summary>
    [SerializeField] bool isBack;

    /// <summary>
    /// Координаты леавой границы камеры
    /// </summary>
    Vector2 leftCameraLine;
    /// <summary>
    /// Координаты правой границы камеры
    /// </summary>
    Vector2 rightCameraLine;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        SetChangers();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Установка текущего уровня
            if (isBack)
            {
                StartCoroutine(ChangeCharacterPositionCoroutine(false, collision));

                collision.GetComponent<MoveCharacter>().savePosition.UsePosition = true;
            }
            else
            {
                StartCoroutine(ChangeCharacterPositionCoroutine(true, collision));
            }
        }
    }

    /// <summary>
    /// Меняет позицию игрока в зависимости от того с какой стороны игрок прошел границу камеры
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeCharacterPositionCoroutine(bool isForward, Collider2D collision)
    {
        Animation blackoutAnimation = GameObject.Find("StartCanvas").GetComponent<Animation>();

        // Запуск анимации перехода из катсцены на первый уровень
        blackoutAnimation[blackoutAnimation.clip.name].time = blackoutAnimation[blackoutAnimation.clip.name].length;
        blackoutAnimation[blackoutAnimation.clip.name].speed *= -1;
        blackoutAnimation.Play();

        collision.GetComponent<CollectObjects>().collected.CollectedObjectsCount = 0;

        yield return new WaitForSeconds(.5f);

        int loadScene = isForward ? SceneManager.GetActiveScene().buildIndex + 1 : SceneManager.GetActiveScene().buildIndex - 1;

        SceneManager.LoadScene(loadScene);
    }

    /// <summary>
    /// Устанавливает триггеры смены уровня на границы камеры
    /// </summary>
    public void SetChangers()
    {
        leftCameraLine = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.5f)); // Получение левой границы экрана
        rightCameraLine = mainCamera.ViewportToWorldPoint(new Vector2(1, 0.5f)); // Получение правой границы экрана

        transform.position = isBack ? leftCameraLine : rightCameraLine;
    }
}
