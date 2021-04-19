using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] Transform launcher;
    [SerializeField, Range(1, 10)] float bulletSpeed;
    [SerializeField] GameObject bullet;
    [SerializeField] float buletTorque;

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, launcher.position, Quaternion.identity);

        var bulletRb = newBullet.GetComponent<Rigidbody2D>();

        bulletRb.AddTorque(buletTorque, ForceMode2D.Impulse);

        bulletRb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
