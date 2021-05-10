using System;
using System.Collections;
using UnityEngine;

public class InputCharacter : MonoBehaviour
{
    bool run;

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

    [SerializeField, Header("Melee interval")] float meleeTime;
    /// <summary>
    /// Таймер выстрела
    /// </summary>
    float shotTimer;

    float meleeTimer;

    [SerializeField, Header("Control enable")] bool isControlEnable;

    MeleeAtack meleeAtack;

    /// <summary>
    /// Компонент запуска снаряда
    /// </summary>
    BulletLauncher bulletLauncher;

    private void Awake()
    {
        // Получение анимаций
        animator = GetComponentInChildren<Animator>();

        // Инициализация эеземпляра класса передвижения персонажа
        move = GetComponent<MoveCharacter>();

        meleeAtack = GetComponentInChildren<MeleeAtack>();

        // Получение компонента BulletLauncher
        bulletLauncher = GetComponentInChildren<BulletLauncher>();

        meleeTimer = meleeTime;

        shotTimer = shotTime; // Назначение таймера выстрела

        // Подписка на событие изменения доступности управления
        GetComponentInChildren<HealthManager>().ControlEnableEvent += HealthManager_ControlEnableEvent;
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
            shotTimer += Time.deltaTime; // Изменение таймера

            meleeTimer += Time.deltaTime;

            MeleeActivate();

            ShootActivate();

            Controls();

            // Запуск\отключение анимации движения персонажа
            run = Mathf.Abs(horisontalAxis) > 0;

            animator.SetBool("JumpB", !move.IsGrounded); // Запуск анимации прыжка
        }
        else
            horisontalAxis = 0;
    }

    void FixedUpdate()
    {
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
        animator.speed = 1f;

        animator.SetBool("Run", run); // Запуск анимации бега

        // Считывание нажатия кнопок направления
        horisontalAxis = Input.GetAxis("Horizontal");

        // считывание нажатия кнопки прыжка
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("JumpT");
            jump = Vector2.up;
        }

        if (meleeTimer < meleeTime)
        {
            animator.speed = 2f;

            horisontalAxis = 0;
        }
    }

    /// <summary>
    /// Метод управления выстрелом
    /// </summary>
    void ShotControl()
    {
        bulletLauncher.Shot(); // Вызов метода выстрела

        animator.SetTrigger("Attack"); // Запуск анимации атаки
    }

    void MeleeControl()
    {
        meleeAtack.Attack();

        animator.SetTrigger("Attack");
    }

    void MeleeActivate()
    {
        if (meleeTimer > meleeTime)
        {
            if (Input.GetButtonDown("Fire1") && move.IsGrounded)
            {
                meleeTimer = 0;

                MeleeControl();
            }
            else
                meleeTimer = meleeTime + 1;
        }
    }

    void ShootActivate()
    {
        if (shotTimer > shotTime && bulletLauncher.bulletCont > 0)
        {
            if (Input.GetButton("Fire2"))
            {
                shotTimer = 0;

                ShotControl();

                bulletLauncher.bulletCont -= 1;
            }
            else
                shotTimer = shotTime + 1; // Прибаление единицы к таймеру для срабатывания первого выстрела при окончании таймера
        }
    }
}
