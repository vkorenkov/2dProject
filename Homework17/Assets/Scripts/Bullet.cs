using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float flightTime;
    float flightTimer;
    Rigidbody2D bulletRb;

    private void Awake()
    {
        flightTimer = flightTime;
        bulletRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        flightTimer -= Time.deltaTime;

        if (flightTimer <= 0 && bulletRb.gravityScale == 0)
            bulletRb.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D collisionRb))
            collisionRb.velocity = Vector2.zero;

        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
        {
            collisionObjHealth.TakeDamage(false, damage);
        }

        Destroy(gameObject);
    }
}
