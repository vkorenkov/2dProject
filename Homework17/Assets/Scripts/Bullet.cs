using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float flightTime;
    float flightTimer;
    Rigidbody2D bulletRb;
    [SerializeField] ParticleSystem hitEffect;

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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.TryGetComponent(out Rigidbody2D collisionRb))
    //        collisionRb.velocity = Vector2.zero;

    //    if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
    //    {
    //        collisionObjHealth.TakeDamage(false, damage);
    //    }

    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
        {
            bulletRb.velocity = new Vector2();

            if (hitEffect)
                hitEffect.Play();

            collisionObjHealth.TakeDamage(false, damage);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            Destroy(gameObject, hitEffect.main.duration);

            return;
        }

        Destroy(gameObject);
    }
}
