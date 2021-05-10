using UnityEngine;

public class LaunchBombScript : MonoBehaviour
{
    /// <summary>
    /// Таймер запуска бомбы
    /// </summary>
    float bombLaunchTimer;
    /// <summary>
    /// Объект бомбы
    /// </summary>
    [SerializeField, Header("Bomb game object")] GameObject bombObj;
    /// <summary>
    /// Максимальное значение таймера запуска бомбы
    /// </summary>
    [SerializeField, Range(1, 3), Header("Bomb max timer value")] 
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
        bombLaunchTimer = Random.Range(0.5f, bombLaunchTimerMaxValue); 
    }

    private void Update()
    {
        if (canLaunch)
        {
            bombLaunchTimer -= Time.deltaTime; // Изменние таймера 

            if (bombLaunchTimer <= 0)
            {
                // Создание жкземпляра объекта бомбы
                Rigidbody2D bomb = Instantiate(bombObj, transform.position, transform.rotation).GetComponent<Rigidbody2D>();

                bomb.AddForce(transform.up * Random.Range(minLaunchForce, maxLaunchForce), ForceMode2D.Impulse); // Небольшой импульс запуска бомбы

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
