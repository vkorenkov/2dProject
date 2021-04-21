using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombScript : MonoBehaviour
{
    /// <summary>
    /// Твердое тело объекта юомбы
    /// </summary>
    Rigidbody2D bombRb;
    /// <summary>
    /// Система частиц эффекта взрыва
    /// </summary>
    ParticleSystem explosionPs;
    /// <summary>
    /// Эффектор имитации взрыва
    /// </summary>
    PointEffector2D bombEffector;
    /// <summary>
    /// Изображение бомбы
    /// </summary>
    SpriteRenderer bombSprite;
    /// <summary>
    /// Коллайдер эффектора взрыва
    /// </summary>
    CircleCollider2D bombPointEffector;

    private void Awake()
    {
        bombRb = GetComponent<Rigidbody2D>(); // Получение твердого тела объекта бомбы
        explosionPs = GetComponentInChildren<ParticleSystem>(); // получение частиц имитации взрыва
        bombEffector = GetComponent<PointEffector2D>(); // Получение эффектора бомбы
        bombSprite = GetComponent<SpriteRenderer>(); // Получение изображение бомбы
        bombPointEffector = GetComponent<CircleCollider2D>(); // Получение коллайдера бомбы

        bombRb.AddForce(transform.up * Random.Range(2, 5), ForceMode2D.Impulse); // Небольшой импульс запуска бомбы
        bombRb.AddTorque(Random.Range(-2, 2), ForceMode2D.Impulse); // Придание вращения бомбе
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bombSprite.enabled)
        {
            Destroy(gameObject, explosionPs.main.startLifetime.constantMax); // Запуск уничтожения объекта бомбы по времени работы частиц
            explosionPs.Play(); // Запуск эффекта взрыва
            bombSprite.enabled = false; // Отключение изображения бомбы
            bombPointEffector.enabled = true; // включение колайдера эффектора имитации взрыва
            bombEffector.enabled = true; // // включение эффектора имитации взрыва
            bombRb.constraints = RigidbodyConstraints2D.FreezePositionX; // "Заморозка" движения бомбы по оси X
        }
    }
}
