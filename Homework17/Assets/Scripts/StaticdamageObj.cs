using UnityEngine;

public class StaticdamageObj : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage(collision, damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision, damage);
    }

    void Damage(Collision2D collision, float damage)
    {
        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
            collisionObjHealth.TakeDamage(false, damage);
    }

    void Damage(Collider2D collision, float damage)
    {
        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
            collisionObjHealth.TakeDamage(false, damage);
    }
}
