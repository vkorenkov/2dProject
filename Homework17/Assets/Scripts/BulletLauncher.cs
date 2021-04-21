using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] Transform launcher;
    [SerializeField, Range(1, 10)] float bulletSpeed;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletTorque;

    public void Shoot()
    {
        Rigidbody2D newBullet = Instantiate(bullet, launcher.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        bulletTorque = transform.rotation.y < 0 ? Mathf.Abs(bulletTorque) : -Mathf.Abs(bulletTorque);

        newBullet.AddTorque(bulletTorque, ForceMode2D.Impulse);

        newBullet.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
