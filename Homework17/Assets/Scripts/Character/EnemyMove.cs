using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    /// <summary>
    /// Объект игрока
    /// </summary>
    GameObject player;
    /// <summary>
    /// Объект здоровя
    /// </summary>
    HealthManager health;
    /// <summary>
    /// обозначения, что враг может двигаться
    /// </summary>
    [SerializeField] bool thisMustMove;
    /// <summary>
    /// Скорость передвижения
    /// </summary>
    [SerializeField, Range(0.1f, 1)] float speed;
    /// <summary>
    /// Анимация движения
    /// </summary>
    Animator moveAnimation;
    /// <summary>
    /// Указание, что объект должен двигаться в текущий момент
    /// </summary>
    bool isMovable;
    /// <summary>
    /// Расстояние на котором враг начинает подходить
    /// </summary>
    [SerializeField] float moveDistance = 1;

    /// <summary>
    /// Свойство дистанции между ГГ и врагом
    /// </summary>
    float distance
    {
        get => Vector2.Distance(player.transform.position, transform.position);
    }

    /// <summary>
    /// Свойство текущей позиции игрока
    /// </summary>
    Vector2 playerPosition
    {
        get => player.transform.position;
    }

    private void Awake()
    {
        moveAnimation = GetComponent<Animator>();
        health = GetComponent<HealthManager>();
        health.ControlEnableEvent += Health_ControlEnableEvent;
        player = GameObject.FindGameObjectWithTag("Player");
        isMovable = true;
    }

    /// <summary>
    /// Обработчик события включения\отключения если враг погиб
    /// </summary>
    /// <param name="isAlive"></param>
    private void Health_ControlEnableEvent(bool isAlive)
    {
        isMovable = isAlive;
        moveAnimation.SetBool("Walk", false);
    }

    private void FixedUpdate()
    {
        if (isMovable && thisMustMove)
        {
            // Перемещение врага в сторону ГГ
            if (distance < moveDistance && health.isAlive)
            {
                moveAnimation.SetBool("Walk", isMovable);

                var newPosition = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

                transform.position = new Vector2(newPosition.x, transform.position.y);

                transform.rotation = EnemySight();
            }
            else
                moveAnimation.SetBool("Walk", false);
        }
    }

    /// <summary>
    /// Поворот спрайта врага
    /// </summary>
    /// <returns></returns>
    Quaternion EnemySight()
    {
        return playerPosition.x < transform.position.x ? Quaternion.identity : new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
    }
}
