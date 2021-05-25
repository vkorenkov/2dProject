using UnityEngine;

public class SpawnObjectScript : MonoBehaviour
{
    /// <summary>
    /// ������ ������� �����
    /// </summary>
    float bombLaunchTimer;

    [SerializeField] Transform launcher;
    /// <summary>
    /// ������ ��� ����������
    /// </summary>
    [SerializeField, Header("Spawn game object")] GameObject spawnObj;

    [SerializeField] bool isRandom;
    /// <summary>
    /// ������������ �������� ������� ������� �����
    /// </summary>
    [SerializeField, Range(0, 3), Header("Spawn max timer value")]
    float bombLaunchTimerMaxValue = 1;

    [SerializeField] float minLaunchForce = 2;
    [SerializeField] float maxLaunchForce = 5;
    /// <summary>
    /// ����������� ������� �����
    /// </summary>
    bool canLaunch;

    [HideInInspector] public bool isDestroy; 

    private void Start()
    {
        // ���������� ���������� ������� ������� ������ �����
        bombLaunchTimer = Random.Range(0, bombLaunchTimerMaxValue);
    }

    private void Update()
    {
        if (canLaunch)
        {
            bombLaunchTimer -= Time.deltaTime; // �������� ������� 

            if (bombLaunchTimer <= 0)
            {
                float spawnPosition = 0;

                if (isRandom)
                    spawnPosition = Random.Range(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.max.x);
                else
                    spawnPosition = launcher.position.x;

                // �������� ���������� ������� �����
                var InstantiateObj = Instantiate(spawnObj, new Vector3(spawnPosition, launcher.position.y), Quaternion.identity);

                if (InstantiateObj.TryGetComponent(out Rigidbody2D rb))
                    rb.AddForce(transform.up * Random.Range(minLaunchForce, maxLaunchForce), ForceMode2D.Impulse); // ��������� ������� ������� �����

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
        if (collision.CompareTag("Player") || collision.CompareTag("Movable"))
            canLaunch = true;
    }
}