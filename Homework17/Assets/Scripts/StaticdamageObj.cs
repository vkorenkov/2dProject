using UnityEngine;

public class StaticdamageObj : MonoBehaviour
{
    /// <summary>
    /// Обозначает то, что коллайдер объекта является триггером
    /// </summary>
    [SerializeField, Header("Trigger")] bool isTriggerObj;
    /// <summary>
    /// Урон наносимый объектом
    /// </summary>
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

    /// <summary>
    /// Метод нанесения урона при столкглвении с объектом, котоырй имеет компонент HealthManager
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="damage"></param>
    void Damage(Collision2D collision, float damage)
    {
        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
            collisionObjHealth.TakeDamage(false, damage);
    }

    /// <summary>
    /// Метод нанесения урона при столкглвении с объектом, котоырй имеет компонент HealthManager
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="damage"></param>
    void Damage(Collider2D collision, float damage)
    {
        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
            collisionObjHealth.TakeDamage(false, damage);
    }
}
