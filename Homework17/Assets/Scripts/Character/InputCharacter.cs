using System;
using System.Collections;
using UnityEngine;

public class InputCharacter : MonoBehaviour
{
    /// <summary>
    /// Указание для включения анимации бега
    /// </summary>
    bool run;
    /// <summary>
    /// Главная камера
    /// </summary>
    Camera mainCamera;
    /// <summary>
    /// Текущий уровень
    /// </summary>
    public static int currentLevel;
    /// <summary>
    /// Праввый триггер смены камеры
    /// </summary>
    [SerializeField] Transform rightLevelChanger;
    /// <summary>
    /// Левый триггер смены камеры
    /// </summary>
    [SerializeField] Transform leftLevelChanger;
    ///// <summary>
    ///// Координаты леавой границы камеры
    ///// </summary>
    //public static Vector2 leftCameraLine;
    ///// <summary>
    ///// Координаты правой границы камеры
    ///// </summary>
    //public static Vector2 rightCameraLine;

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
    /// <summary>
    /// Таймер отката ближнего боя
    /// </summary>
    float meleeTimer;
    /// <summary>
    /// Указание доступности управления
    /// </summary>
    [SerializeField, Header("Control enable")] bool isControlEnable;
    /// <summary>
    /// Объект атаки ближнего боя
    /// </summary>
    MeleeAtack meleeAtack;

    /// <summary>
    /// Компонент запуска снаряда
    /// </summary>
    BulletLauncher bulletLauncher;

    private void Awake()
    {
        mainCamera = Camera.main;

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
    private void HealthManager_ControlEnableEvent(bool isControlEnable)
    {
        this.isControlEnable = isControlEnable;
        run = isControlEnable;
        animator.SetBool("JumpB", false);
    }

    void Update()
    {
        //if (leftLevelChanger && rightLevelChanger)
        //    SetChangers();

        if (isControlEnable)
        {
            shotTimer += Time.deltaTime; // Изменение таймера

            meleeTimer += Time.deltaTime;

            MeleeActivate();

            ShootActivate();

            Controls();

            run = Mathf.Abs(horisontalAxis) > 0;

            animator.SetBool("JumpB", !move.IsGrounded); // Запуск анимации прыжка
        }
        else
            horisontalAxis = 0;

        animator.SetBool("Run", run); // Запуск анимации бега
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

        // Считывание нажатия кнопок направления
        horisontalAxis = Input.GetAxis("Horizontal");

        animator.SetBool("Run", run); // Запуск анимации бега

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

    /// <summary>
    /// Управляет атакой ближнего боя
    /// </summary>
    void MeleeControl()
    {
        meleeAtack.Attack();

        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// Активация атаки ближнего боя
    /// </summary>
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

    /// <summary>
    /// Активация выстрела
    /// </summary>
    void ShootActivate()
    {
        if (shotTimer > shotTime && bulletLauncher.playerPrefs.BulletCount > 0)
        {
            if (Input.GetButton("Fire2"))
            {
                shotTimer = 0;

                ShotControl();

                bulletLauncher.playerPrefs.BulletCount -= 1;
            }
            else
                shotTimer = shotTime + 1; // Прибаление единицы к таймеру для срабатывания первого выстрела при окончании таймера
        }
    }

    ///// <summary>
    ///// Устанавливает триггеры смены уровня на границы камеры
    ///// </summary>
    //public void SetChangers()
    //{
    //    leftCameraLine = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.5f)); // Получение левой границы экрана
    //    rightCameraLine = mainCamera.ViewportToWorldPoint(new Vector2(1, 0.5f)); // Получение правой границы экрана

    //    leftLevelChanger.position = leftCameraLine;
    //    rightLevelChanger.position = rightCameraLine;
    //}

    /// <summary>
    /// Включает\отключает управление
    /// </summary>
    /// <param name="control"></param>
    public void ControlEnableChange(bool control)
    {
        run = control;
        isControlEnable = control;
    }
}
