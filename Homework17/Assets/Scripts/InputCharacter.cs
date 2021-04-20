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

    [SerializeField] float shootTime;

    float shootTimer;

    [SerializeField] bool isControlEnable;

    BulletLauncher bulletLauncher;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Инициализация эеземпляра класса передвижения персонажа
        move = GetComponent<MoveCharacter>();

        bulletLauncher = GetComponent<BulletLauncher>();

        shootTimer = shootTime;

        GetComponent<HealthManager>().ControlEnableEvent += HealthManager_ControlEnableEvent;
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

            shootTimer += Time.deltaTime;

            if (shootTimer > shootTime)
            {
                shootTimer = 0;

                ShootControl();
            }

            // Запуск\отключение анимации движения персонажа
            var go = Mathf.Abs(horisontalAxis) > 0;

            animator.SetBool("Run", go);

            animator.SetBool("Dash", !move.IsGrounded);
        }
        else
            horisontalAxis = 0;
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
        {
            jump = Vector2.up;
        }
    }

    void ShootControl()
    {
        if (Input.GetButton("Fire1"))
        {
            bulletLauncher.Shoot();

            animator.SetBool("Attack", true);
        }
        else
            shootTimer = shootTime + 1;
    }
}
