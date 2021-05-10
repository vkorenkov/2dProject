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

    [SerializeField] float minLaunchForce = 2;
    [SerializeField] float maxLaunchForce = 5;
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
                Rigidbody2D bomb = Instantiate(bombObj, transform.position, transform.rotation).GetComponent<Rigidbody2D>();

                bomb.AddForce(transform.up * Random.Range(minLaunchForce, maxLaunchForce), ForceMode2D.Impulse); // ��������� ������� ������� �����

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
