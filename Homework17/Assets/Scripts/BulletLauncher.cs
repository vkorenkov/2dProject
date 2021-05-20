using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    /// <summary>
    /// Объект, откуда будет производиться запуск снаряда
    /// </summary>
    [SerializeField, Header("Projectile launcher")] Transform launcher;
    /// <summary>
    /// Скорость полета снаряда
    /// </summary>
    [SerializeField, Range(1, 10), Header("Projectile speed")] 
    float bulletSpeed;

    [SerializeField] Output output;

    [Header("Projectiles count")] public int bulletCont;
    /// <summary>
    /// Объект снаряда
    /// </summary>
    [SerializeField, Header("Projectile Object")] GameObject bullet;
    /// <summary>
    /// Значение вращения снаряда
    /// </summary>
    [SerializeField, Header("Projectile torque")] float bulletTorque;

    private void Update()
    {
        output.OutputProjectilesCount($"{bulletCont}");
    }

    /// <summary>
    /// Метод создания экземпляра снаряда
    /// </summary>
    public void Shot()
    {
        if (bulletCont == 0)
            return;

        // Создание экзкмпляра объекта снаряда
        Rigidbody2D newBullet = Instantiate(bullet, launcher.position, Quaternion.identity, null).GetComponent<Rigidbody2D>();
        // Присвоение значения вращения снаряда в зависимости от стороны движения
        bulletTorque = transform.rotation.y > 0 ? -Mathf.Abs(bulletTorque) : Mathf.Abs(bulletTorque);
        // Придание вращения снаряду
        newBullet.AddTorque(bulletTorque, ForceMode2D.Impulse);
        // Придание скорости сраряду
        newBullet.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
