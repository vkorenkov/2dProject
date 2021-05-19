using TMPro;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    private JointMotor2D sliderMotor;
    [SerializeField] bool isFullMotor;
    [SerializeField] bool isMovePlatform;
    [SerializeField] bool isDoor;
    [SerializeField] bool isCollectDoor;
    [SerializeField] bool isKilleEnemiesDoor;
    /// <summary>
    /// Поле соединения
    /// </summary>
    [SerializeField] SliderJoint2D slider;
    [SerializeField] float motorForce = 5;
    [SerializeField] int collectedObjectCount;
    [SerializeField] int killedEnemiesCount;
    [SerializeField] TextMeshPro message;

    private void Awake()
    {
        if (isMovePlatform) isFullMotor = true;

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
                    // Зпуск движения
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
                    if (collision.GetComponent<CollectObjects>().collectedObjectsCount >= collectedObjectCount)
                    {
                        collision.GetComponent<CollectObjects>().collectedObjectsCount = 0;
                        slider.useMotor = false;
                        message.gameObject.SetActive(false);
                    }
                    else
                    {
                        message.text = $"You must collect {collectedObjectCount} beer to get through!";
                        AnimationPlayback(true);
                    }
                }
                if (isKilleEnemiesDoor)
                {
                    if (collision.GetComponent<CollectObjects>().killedEnemies >= killedEnemiesCount)
                    {
                        collision.GetComponent<CollectObjects>().killedEnemies = 0;
                        slider.useMotor = false;
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
                    // Остановка движения
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
                AnimationPlayback(false);
            }
        }
    }

    void AnimationPlayback(bool side)
    {
        var DescriptionAnimation = message.GetComponent<Animation>();
        int speed = 1;

        if (!side)
        {
            speed *= -1;
            DescriptionAnimation["DescriptionAnimation"].time = DescriptionAnimation["DescriptionAnimation"].length;
        }

        DescriptionAnimation["DescriptionAnimation"].speed = speed;
        DescriptionAnimation.Play();
    }
}
