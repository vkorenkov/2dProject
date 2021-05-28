using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SliderScript : MonoBehaviour
{
    /// <summary>
    /// ������ ��������� �������� ������
    /// </summary>
    AnimationActivator animationActivator;
    /// <summary>
    /// ������ motor �������� �������
    /// </summary>
    private JointMotor2D sliderMotor;
    /// <summary>
    /// ��������, ��� motor �������� � ��� �������
    /// </summary>
    [SerializeField] bool isFullMotor;
    /// <summary>
    /// ��������, ��� ���� ������ �������� ���������� ����������
    /// </summary>
    [SerializeField] bool isMovePlatform;
    /// <summary>
    /// �������� ��� ���� ������ �������� ������� ��� �������� �� ������ �������
    /// </summary>
    [SerializeField] bool isDoor;
    /// <summary>
    /// ��������, ��� ��� ����� ����������� ��� ����������� ���������� ��������� ���������
    /// </summary>
    [SerializeField] bool isCollectDoor;
    /// <summary>
    /// ��������, ��� ��� ����� ����������� ��� ����������� ���������� ������ ������
    /// </summary>
    [SerializeField] bool isKilleEnemiesDoor;
    /// <summary>
    /// ���� ����������
    /// </summary>
    [SerializeField] SliderJoint2D slider;
    /// <summary>
    /// ���� ������
    /// </summary>
    [SerializeField] float motorForce = 5;
    /// <summary>
    /// ����������� ���������� ��������� �������
    /// </summary>
    [SerializeField] int collectedObjectCount;
    /// <summary>
    /// ����������� ���������� ������ ������
    /// </summary>
    [SerializeField] int killedEnemiesCount;
    /// <summary>
    /// ����� ���������
    /// </summary>
    [SerializeField] TextMeshPro message;

    private void Awake()
    {
        animationActivator = new AnimationActivator();

        if (isMovePlatform) isFullMotor = true; // �������, ��� ������� ���� ������ �������� ���������, ���������� �������� � ��� �������

        // ��������� motor ���� � ������� �������� �������� � ��� �����������
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
            // ��������� ��������� �������� ��� ���������� ������
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
