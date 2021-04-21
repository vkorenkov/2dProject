using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Количество урона
    /// </summary>
    [SerializeField, Header("Damage")] float damage;
    /// <summary>
    /// Время полета снаряда
    /// </summary>
    [SerializeField, Header("Projectile time"), Tooltip("projectile flight time ")] 
    float flightTime;
    /// <summary>
    /// Таймер полета снаряда
    /// </summary>
    float flightTimer;
    /// <summary>
    /// Твердое тело снаряда
    /// </summary>
    Rigidbody2D bulletRb;

    [SerializeField, Header("Effect of the hit")] ParticleSystem hitEffect;

    private void Awake()
    {
        flightTimer = flightTime; // Назначение таймера
        bulletRb = GetComponent<Rigidbody2D>(); // Получение твердого тела снаряда
    }

    private void Update()
    {
        flightTimer -= Time.deltaTime; // Уменьшение таймера

        if (flightTimer <= 0 && bulletRb.gravityScale == 0)
            bulletRb.gravityScale = 1; // Включение гравитации для снаряда
    }

    #region collision
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
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Попытка получения компонента HealthManager при столкновении с препятствием
        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
        {
            bulletRb.velocity = new Vector2(); // Сброс скорости полета

            if (hitEffect)
                hitEffect.Play(); // Запуск эффекта попадания

            collisionObjHealth.TakeDamage(false, damage); // Нанесение урона

            GetComponent<SpriteRenderer>().enabled = false; // Отключение изображения снаряда
            GetComponent<Collider2D>().enabled = false; // Отключение коллайдера снаряда

            Destroy(gameObject, hitEffect.main.duration); // Запуск уничтожения объекта снаряда при столкновении с объектом, который может получить урон

            return;
        }

        Destroy(gameObject); // Запуск уничтожения объекта снаряда
    }
}
