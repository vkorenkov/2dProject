using System.Collections;
using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public bool isBonus; // Указание, что объект это бонус
    public bool isProjectile; // Указание, что объект это снаряд
    public bool isHealth; // Указание, что объект это бонус здоровья

    private bool canPickUp; // Указание, что объект можно подобрать

    HealthManager healthManager; // Объект компонента управления здоровьем
    CollectObjects collectObjects; // Объект сбора бонусов
    BulletLauncher bulletLauncher; // Объект запуска снаряда

    [SerializeField] float healthRecovery; // Значение восстановления здоровья
    [SerializeField] int projectileRecovery; // Значение восстановления снарядов
    [SerializeField] ParticleSystem getBonusEffect; // Эффекта подбора бонуса
    [SerializeField] ParticleSystem bonusEffect; // Эффекта бонуса
    [SerializeField] Animation DescriptionAnimation; // Анимаци описания

    [SerializeField] TextMeshPro bonusDescription; // Объект текста описания

    private Collider2D player;

    private void Awake()
    {
        var player = GameObject.Find("MainCharacter");

        // Определение типа бонуса
        if (isHealth)
            healthManager = player.GetComponent<HealthManager>();
        if (isProjectile)
            bulletLauncher = player.GetComponentInChildren<BulletLauncher>();
        if (isBonus)
            collectObjects = player.GetComponent<CollectObjects>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickUp)
        {
            if (isBonus) collectObjects.collectedObjectsCount += 1; // Прибавление собранного бонуса

            // Определение количества здоровья, чтобы не получить более 100% здоровья
            if (isHealth)
            {
                if(healthManager.currentHealth == healthManager.maxHealth)
                {
                    var outPut = player.GetComponentInChildren<Output>();
                    outPut.timer = 5;
                    outPut.goTimer = true;
                    outPut.OutputDialog("I am completely healthy, but I always like to drink!");
                }

                // Восстановление здоровья
                var current = healthManager.maxHealth - healthManager.currentHealth;
                healthManager.currentHealth += current > healthRecovery ? healthRecovery : current;
            }
            else if (!isBonus)
                return;

            getBonusEffect.Play(); // Запуск эффекта получения бонуса

            StartCoroutine(ParticlesPlaybackTime());

            canPickUp = false;
        }
    }

    /// <summary>
    /// Корутина уничтожения объета бонуса
    /// </summary>
    /// <returns></returns>
    IEnumerator ParticlesPlaybackTime()
    {
        bonusEffect.Stop(); // остановка эффекта бонуса
        GetComponent<BoxCollider2D>().enabled = false; // Отключение колайдера бонуса
        GetComponent<SpriteRenderer>().enabled = false; // Отключение спрайта бонуса

        while (getBonusEffect.isPlaying)
            yield return null;

        Destroy(gameObject.transform.parent.gameObject); // Уничтожение объекта бонуса

        StopCoroutine(ParticlesPlaybackTime()); // Остановка корутины
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision;

            canPickUp = true;

            if (isBonus)
                ShowDescription();

            if (isHealth)
                ShowDescription();

            if (isProjectile)
            {
                bulletLauncher.bulletCount += projectileRecovery;
                getBonusEffect.Play();
                StartCoroutine(ParticlesPlaybackTime());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;

            canPickUp = false;

            // Закрытие анимации 
            if (bonusDescription)
            {
                DescriptionAnimation["DescriptionAnimation"].time = DescriptionAnimation["DescriptionAnimation"].length;
                DescriptionAnimation["DescriptionAnimation"].speed = -1;
                DescriptionAnimation.Play();
            }
        }
    }

    /// <summary>
    /// Метод запуска анимации появления описания
    /// </summary>
    void ShowDescription()
    {
        DescriptionAnimation["DescriptionAnimation"].speed = 1;
        DescriptionAnimation.Play();
    }
}
