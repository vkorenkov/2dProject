using UnityEngine;

public class SpawnObjectScript : MonoBehaviour
{
    /// <summary>
    /// Таймер запуска бомбы
    /// </summary>
    float bombLaunchTimer;

    [SerializeField] Transform launcher;
    /// <summary>
    /// Объект для размещения
    /// </summary>
    [SerializeField, Header("Spawn game object")] GameObject spawnObj;

    [SerializeField] bool isRandom;

    [SerializeField] bool isBomb;
    /// <summary>
    /// Максимальное значение таймера запуска бомбы
    /// </summary>
    [SerializeField, Range(0, 3), Header("Spawn max timer value")]
    float bombLaunchTimerValue;

    [SerializeField] float minLaunchForce = 2;
    [SerializeField] float maxLaunchForce = 5;
    /// <summary>
    /// Доступность запуска бомбы
    /// </summary>
    bool canLaunch;

    [HideInInspector] public bool isDestroy;

    private void Update()
    {
        if (canLaunch && isBomb)
        {
            bombLaunchTimer -= Time.deltaTime; // Изменние таймера 

            if (bombLaunchTimer <= 0)
            {
                Launch();
            }
        }
    }

    void Launch()
    {
        float spawnPosition = 0;

        if (isRandom)
            spawnPosition = Random.Range(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.max.x);
        else
            spawnPosition = launcher.position.x;

        // Создание жкземпляра объекта бомбы
        var InstantiateObj = Instantiate(spawnObj, new Vector3(spawnPosition, launcher.position.y), Quaternion.identity);

        if (InstantiateObj.TryGetComponent(out Rigidbody2D rb))
            rb.AddForce(transform.up * Random.Range(minLaunchForce, maxLaunchForce), ForceMode2D.Impulse); // Небольшой импульс запуска бомбы

        // Отключение возможноти запуска бомбы
        //canLaunch = false;
        // Назначение случайного значения таймера бомбы
        bombLaunchTimer = bombLaunchTimerValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Запуск бомбы при срабатывании триггера
        if (collision.CompareTag("Player") || collision.CompareTag("Movable"))
        {
            if (isBomb)
            {
                Launch();
                canLaunch = true;
            }
            else
                Launch();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Запуск бомбы при срабатывании триггера
        if (collision.CompareTag("Player") || collision.CompareTag("Movable"))
            canLaunch = false;
    }
}
