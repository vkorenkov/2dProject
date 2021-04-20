using UnityEngine;

public class StaticdamageObj : MonoBehaviour
{
    [SerializeField] bool isTriggerObj;

    [SerializeField] float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggerObj && collision.gameObject.tag.ToLower() == "player")
            Damage(collision, damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggerObj && collision.gameObject.tag.ToLower() == "player")
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
