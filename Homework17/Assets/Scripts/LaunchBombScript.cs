using UnityEngine;

public class LaunchBombScript : MonoBehaviour
{
    /// <summary>
    /// ������ ������� �����
    /// </summary>
    float bombLaunchTimer;
    /// <summary>
    /// ������ �����
    /// </summary>
    [SerializeField, Header("Bomb game object")] GameObject bombObj;
    /// <summary>
    /// ������������ �������� ������� ������� �����
    /// </summary>
    [SerializeField, Range(1, 3), Header("Bomb max timer value")] 
    float bombLaunchTimerMaxValue = 1;
    /// <summary>
    /// ����������� ������� �����
    /// </summary>
    bool canLaunch;

    private void Start()
    {
        // ���������� ���������� ������� ������� ������ �����
        bombLaunchTimer = Random.Range(0.5f, bombLaunchTimerMaxValue); 
    }

    private void Update()
    {
        if (canLaunch)
        {
            bombLaunchTimer -= Time.deltaTime; // �������� ������� 

            if (bombLaunchTimer <= 0)
            {
                // �������� ���������� ������� �����
                Instantiate(bombObj, transform.position, transform.rotation);
                // ���������� ���������� ������� �����
                canLaunch = false;
                // ���������� ���������� �������� ������� �����
                bombLaunchTimer = Random.Range(0, bombLaunchTimerMaxValue);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ ����� ��� ������������ ��������
        if (collision.tag.ToLower() == "player" || collision.tag.ToLower() == "movable")
            canLaunch = true;
    }
}
