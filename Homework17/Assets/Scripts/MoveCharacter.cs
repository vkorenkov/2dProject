using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    /// <summary>
    /// Поле компонента RigitBody героя
    /// </summary>
    Rigidbody2D characterRb;
    /// <summary>
    /// Поле множителя скорости героя
    /// </summary>
    [SerializeField, Range(1, 10)] float characterSpeed = 1;
    /// <summary>
    /// Поле множителя максимальной скорости героя
    /// </summary>
    [SerializeField, Range(0.1f, 5f)] float maxSpeed = 0.1f;
    /// <summary>
    /// Поле компонента Animator главного героя
    /// </summary>
    Animator animator;
    /// <summary>
    /// Поле высоты прыжка главного героя
    /// </summary>
    [SerializeField, Range(0.1f, 3f)] float jumpForce;

    [SerializeField] bool isPhisicsMove;
    /// <summary>
    /// Свойство текущей скорости главного героя
    /// </summary>
    Vector2 characterRbVelocity
    {
        get => characterRb.velocity;
    }

    private void Start()
    {
        // Получение компонента RigitBody героя
        characterRb = GetComponent<Rigidbody2D>();
        // Получениекомпонента Animator главного героя
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Метод передвижения главного героя
    /// </summary>
    /// <param name="side"></param>
    public void Move(float side)
    {
        // Перемещение героя по горизонтали
        if(!isPhisicsMove)
            transform.Translate(Vector2.right * side * characterSpeed * Time.deltaTime);
        else 
            characterRb.AddForce(Vector2.right * side * characterSpeed);

        // Ограничение скорости передвижения персонажа
        characterRb.velocity = new Vector2(Mathf.Clamp(characterRbVelocity.x, -maxSpeed, maxSpeed), characterRbVelocity.y);

        // Запуск\отключение анимации движения персонажа
        if (side > 0 || side < 0)
            animator.SetBool("Run", true);
        else
            animator.SetBool("Run", false);
    }

    /// <summary>
    /// Метод прыжка главного героя
    /// </summary>
    /// <param name="jumpDirection"></param>
    public void Jump(Vector2 jumpDirection)
    {
        // Физическое воздействие на главного героя
        characterRb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Установка родителем главного героя движущегося предмета
        // для передвижения вместе с ним
        if (collision.transform.tag.ToLower() == "movable")
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.ToLower() == "movable")
        {
            // Сброс родителя главного героя
            transform.parent = null;
        }
    }
}
