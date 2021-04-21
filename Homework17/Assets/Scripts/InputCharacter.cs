using System;
using UnityEngine;

public class InputCharacter : MonoBehaviour
{
    /// <summary>
    /// Поле компонента Animator главного героя
    /// </summary>
    Animator animator;

    /// <summary>
    /// Поле класса передвижения персонажа
    /// </summary>
    MoveCharacter move;
    /// <summary>
    /// поле направления движения
    /// </summary>
    float horisontalAxis;
    /// <summary>
    /// Поле вектора прыжка
    /// </summary>
    Vector2 jump;
    /// <summary>
    /// Интервал между "выстрелами"
    /// </summary>
    [SerializeField, Header("Shot interval")] float shotTime;
    /// <summary>
    /// Таймер выстрела
    /// </summary>
    float shotTimer;

    [SerializeField, Header("Control enable")] bool isControlEnable;

    /// <summary>
    /// Компонент запуска снаряда
    /// </summary>
    BulletLauncher bulletLauncher;

    private void Start()
    {
        // Получение анимаций
        animator = GetComponent<Animator>();

        // Инициализация эеземпляра класса передвижения персонажа
        move = GetComponent<MoveCharacter>();

        // Получение компонента BulletLauncher
        bulletLauncher = GetComponent<BulletLauncher>();

        shotTimer = shotTime; // Назначение таймера выстрела

        // Подписка на событие изменения доступности управления
        GetComponent<HealthManager>().ControlEnableEvent += HealthManager_ControlEnableEvent;
    }

    /// <summary>
    /// Обработчик события изменения доступности управления
    /// </summary>
    /// <param name="isAlive"></param>
    private void HealthManager_ControlEnableEvent(bool isAlive)
    {
        isControlEnable = isAlive;
    }

    void Update()
    {       
        if (isControlEnable)
        {
            Controls();

            shotTimer += Time.deltaTime; // Изменение таймера

            if (shotTimer > shotTime)
            {
                shotTimer = 0;

                ShotControl();
            }

            // Запуск\отключение анимации движения персонажа
            var go = Mathf.Abs(horisontalAxis) > 0;

            animator.SetBool("Run", go); // Запуск анимации бега

            animator.SetBool("Dash", !move.IsGrounded); // Запуск анимации прыжка
        }
        else
            horisontalAxis = 0;
    }

    void FixedUpdate()
    {
        animator.SetBool("Attack", false); // Завершение анимации атаки

        // Вызов метода передвижения персонажа
        move.Move(horisontalAxis);
        // Вызов метода прыжка
        move.Jump(jump);
        // Переопределение вектора прыжка
        jump = new Vector2();
    }

    /// <summary>
    /// Метод передвижения персонажа
    /// </summary>
    void Controls()
    {
        // Считывание нажатия кнопок направления
        horisontalAxis = Input.GetAxis("Horizontal");

        // считывание нажатия кнопки прыжка
        if (Input.GetButtonDown("Jump"))
        {
            jump = Vector2.up;
        }
    }

    /// <summary>
    /// Метод управления выстрелом
    /// </summary>
    void ShotControl()
    {
        if (Input.GetButton("Fire1"))
        {
            bulletLauncher.Shot(); // Вызов метода выстрела

            animator.SetBool("Attack", true); // Запуск анимации атаки
        }
        else
            shotTimer = shotTime + 1; // Прибаление единицы к таймеру для срабатывания первого выстрела при окончании таймера
    }
}
