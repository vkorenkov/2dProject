using System;
using UnityEngine;

public class InputCharacter : MonoBehaviour
{
    HealthManager healthManager;

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

    [SerializeField] bool isControlEnable;

    BulletLauncher bulletLauncher;

    private void Start()
    {
        animator = GetComponent<Animator>();

        isControlEnable = true;

        healthManager = GetComponent<HealthManager>();

        healthManager.ControlEnableEvent += HealthManager_ControlEnableEvent;

        // Инициализация эеземпляра класса передвижения персонажа
        move = GetComponent<MoveCharacter>();

        bulletLauncher = GetComponent<BulletLauncher>();
    }

    private void HealthManager_ControlEnableEvent(bool isAlive)
    {
        isControlEnable = isAlive;
    }

    void Update()
    {
        if (isControlEnable)
        {
            Controls();

            // Запуск\отключение анимации движения персонажа
            var go = Mathf.Abs(horisontalAxis) > 0 ? true : false;

            animator.SetBool("Run", go);
        }
    }

    void FixedUpdate()
    {
        animator.SetBool("Attack", false);

        // Вызов метода передвижения персонажа
        move.Move(horisontalAxis);
        // Вызов метода прыжка
        move.Jump(jump);
        // Переопределение вектора прыжка
        jump = new Vector2();
    }

    void Controls()
    {
        // Считывание нажатия кнопок направления
        horisontalAxis = Input.GetAxis("Horizontal");

        // считывание нажатия кнопки прыжка
        if (Input.GetButtonDown("Jump"))
            jump = Vector2.up;

        if (Input.GetButtonDown("Fire1"))
        {
            bulletLauncher.Shoot();

            animator.SetBool("Attack", true);
        }
    }
}
