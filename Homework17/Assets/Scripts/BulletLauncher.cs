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

    [SerializeField] Output output;

    [Header("Projectiles count")] public int bulletCont;
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
        output.OutputProjectilesCount($"{bulletCont}");
    }

    /// <summary>
    /// ����� �������� ���������� �������
    /// </summary>
    public void Shot()
    {
        if (bulletCont == 0)
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
