using UnityEngine;

public class LaunchBombScript : MonoBehaviour
{
    /// <summary>
    /// Таймер запуска бомбы
    /// </summary>
    float bombLaunchTimer;

    [SerializeField] Transform launcher;
    /// <summary>
    /// Объект бомбы
    /// </summary>
    [SerializeField, Header("Spawn game object")] GameObject bombObj;

    [SerializeField] bool isRandom;
    /// <summary>
    /// Максимальное значение таймера запуска бомбы
    /// </summary>
    [SerializeField, Range(0, 3), Header("Spawn max timer value")]
    float bombLaunchTimerMaxValue = 1;

    [SerializeField] float minLaunchForce = 2;
    [SerializeField] float maxLaunchForce = 5;
    /// <summary>
    /// Доступность запуска бомбы
    /// </summary>
    bool canLaunch;

    private void Start()
    {
        // Назначение случайного времени таймера запука бомбы
        bombLaunchTimer = Random.Range(0, bombLaunchTimerMaxValue);
    }

    private void Update()
    {
        if (canLaunch)
        {
            bombLaunchTimer -= Time.deltaTime; // Изменние таймера 

            if (bombLaunchTimer <= 0)
            {
                float spawnPosition = 0;

                if (isRandom)
                    spawnPosition = Random.Range(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.max.x);
                else
                    spawnPosition = launcher.position.x;

                // Создание жкземпляра объекта бомбы
                Rigidbody2D swpawnObj = Instantiate(bombObj, new Vector3(spawnPosition, launcher.position.y), Quaternion.identity).GetComponent<Rigidbody2D>();

                swpawnObj.AddForce(transform.up * Random.Range(minLaunchForce, maxLaunchForce), ForceMode2D.Impulse); // Небольшой импульс запуска бомбы

                // ОТключение возможноти запуска бомбы
                canLaunch = false;
                // Назначение случайного значения таймера бомбы
                bombLaunchTimer = Random.Range(0, bombLaunchTimerMaxValue);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Запуск бомбы при срабатывании триггера
        if (collision.tag.ToLower() == "player" || collision.tag.ToLower() == "movable")
            canLaunch = true;
    }
}
