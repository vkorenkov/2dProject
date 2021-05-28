using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SliderScript : MonoBehaviour
{
    /// <summary>
    /// Объект активации анимации текста
    /// </summary>
    AnimationActivator animationActivator;
    /// <summary>
    /// Объкут motor текущего объекта
    /// </summary>
    private JointMotor2D sliderMotor;
    /// <summary>
    /// Указание, что motor работает в обе стороны
    /// </summary>
    [SerializeField] bool isFullMotor;
    /// <summary>
    /// Указание, что этот объект является движущеяся платформой
    /// </summary>
    [SerializeField] bool isMovePlatform;
    /// <summary>
    /// Указание что этот объект является двенрью для перехода на другой уровень
    /// </summary>
    [SerializeField] bool isDoor;
    /// <summary>
    /// Указание, что эта дверь открывается при необходимом количестве собранных предметов
    /// </summary>
    [SerializeField] bool isCollectDoor;
    /// <summary>
    /// Указание, что эта дверь открывается при необходимом количестве убитых врагов
    /// </summary>
    [SerializeField] bool isKilleEnemiesDoor;
    /// <summary>
    /// Поле соединения
    /// </summary>
    [SerializeField] SliderJoint2D slider;
    /// <summary>
    /// Сила мотора
    /// </summary>
    [SerializeField] float motorForce = 5;
    /// <summary>
    /// Необходимое количество собранных объекто
    /// </summary>
    [SerializeField] int collectedObjectCount;
    /// <summary>
    /// Необходимое количество убитых врагов
    /// </summary>
    [SerializeField] int killedEnemiesCount;
    /// <summary>
    /// Текст сообщения
    /// </summary>
    [SerializeField] TextMeshPro message;

    private void Awake()
    {
        animationActivator = new AnimationActivator();

        if (isMovePlatform) isFullMotor = true; // Условие, при котором если объект является плаформой, включается движение в обе стороны

        // Включение motor если у объекта включено движение в оба направления
        if (slider && isFullMotor)
        {
            sliderMotor = slider.motor;
            slider.useMotor = true;
        }
    }

    private void FixedUpdate()
    {
        if(isMovePlatform)
        {
            // Включение обратного движения при достижении лимита
            bool isLimit = slider.jointTranslation <= slider.limits.min || slider.jointTranslation >= slider.limits.max;

            if (isLimit)
            {
                sliderMotor.motorSpeed = sliderMotor.motorSpeed * -1;
                slider.motor = sliderMotor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDoor)
        {
            if (!isMovePlatform && collision.CompareTag("Player") || collision.CompareTag("Movable"))
            {
                if (!isFullMotor)
                    slider.useMotor = true;
                else
                {
                    sliderMotor.motorSpeed = motorForce * -1;
                    slider.motor = sliderMotor;
                }
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                if (isCollectDoor)
                {
                    if (collision.GetComponent<CollectObjects>().collected.CollectedObjectsCount >= collectedObjectCount)
                    {
                        slider.useMotor = false;
                        message.gameObject.SetActive(false);
                    }
                    else
                    {
                        message.text = $"You must collect {collectedObjectCount} beer to get through! \n {CollectObjects.hints[SceneManager.GetActiveScene().buildIndex]}";
                        animationActivator.AnimationPlayback(message, true);
                    }
                }
                if (isKilleEnemiesDoor)
                {
                    if (collision.GetComponent<CollectObjects>().collected.KilledEnemiesCount >= killedEnemiesCount)
                    {
                        slider.useMotor = false;
                        message.gameObject.SetActive(false);
                    }
                    else
                    {
                        message.text = $"You must killed {killedEnemiesCount} enemies to get through!";
                        animationActivator.AnimationPlayback(message, true);
                    }
                }
                if (isCollectDoor && isKilleEnemiesDoor)
                {
                    if (collision.GetComponent<CollectObjects>().collected.CollectedObjectsCount >= collectedObjectCount
                        && collision.GetComponent<CollectObjects>().collected.KilledEnemiesCount >= killedEnemiesCount)
                    {
                        slider.useMotor = false;
                        message.gameObject.SetActive(false);
                    }
                    else
                    {
                        message.text = $"You must collect {collectedObjectCount} beer and killed {killedEnemiesCount} enemies to get through! \n {CollectObjects.hints[SceneManager.GetActiveScene().buildIndex]}";
                        animationActivator.AnimationPlayback(message, true);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isDoor)
        {
            if (!isMovePlatform && collision.CompareTag("Player") || collision.CompareTag("Movable"))
            {
                if (!isFullMotor)
                    slider.useMotor = false;
                else
                {
                    sliderMotor.motorSpeed = motorForce;
                    slider.motor = sliderMotor;
                }
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                animationActivator.AnimationPlayback(message, false);
            }
        }
    }
}
