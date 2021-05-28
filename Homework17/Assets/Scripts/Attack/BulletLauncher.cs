using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    /// <summary>
    /// ������, ������ ����� ������������� ������ �������
    /// </summary>
    [SerializeField, Header("Projectile launcher")] Transform launcher;
    /// <summary>
    /// �������� ������ �������
    /// </summary>
    [SerializeField, Range(1, 10), Header("Projectile speed")] 
    float bulletSpeed;
    /// <summary>
    /// ������ ������ ���������� �� �����
    /// </summary>
    [SerializeField] Output output;

    public PlayerPrefs playerPrefs;
    /// <summary>
    /// ����� ��������� ��������
    /// </summary>
    [Header("Projectiles count")] public int bulletCount;
    /// <summary>
    /// ������ �������
    /// </summary>
    [SerializeField, Header("Projectile Object")] GameObject bullet;
    /// <summary>
    /// �������� �������� �������
    /// </summary>
    [SerializeField, Header("Projectile torque")] float bulletTorque;

    private void Update()
    {
        output.OutputProjectilesCount($"{playerPrefs.BulletCount}");
    }

    /// <summary>
    /// ����� �������� ���������� �������
    /// </summary>
    public void Shot()
    {
        if (playerPrefs.BulletCount <= 0)
            return;

        // �������� ���������� ������� �������
        Rigidbody2D newBullet = Instantiate(bullet, launcher.position, Quaternion.identity, null).GetComponent<Rigidbody2D>();
        // ���������� �������� �������� ������� � ����������� �� ������� ��������
        bulletTorque = transform.rotation.y > 0 ? -Mathf.Abs(bulletTorque) : Mathf.Abs(bulletTorque);
        // �������� �������� �������
        newBullet.AddTorque(bulletTorque, ForceMode2D.Impulse);
        // �������� �������� �������
        newBullet.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
