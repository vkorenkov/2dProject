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
    /// Поле высоты прыжка главного героя
    /// </summary>
    [SerializeField, Range(0.1f, 3f), Header("Force of jump")] 
    float jumpForce;

    [SerializeField] Transform WarriorTransform;

    /// <summary>
    /// Корректировка фиксации прыжка
    /// </summary>
    [SerializeField, Header("Jump offset")] float jumpOffset;
    /// <summary>
    /// Положение коллайдера проверки нахождения на земле
    /// </summary>
    [SerializeField, Header("Ground Checker")] Transform groundColliderTransform;
    /// <summary>
    /// Маска поверхности
    /// </summary>
    [SerializeField] LayerMask groundMask;
    /// <summary>
    /// включение\выключение движения по физической модели
    /// </summary>
    [SerializeField] bool isPhysicsMove;

    /// <summary>
    /// Свойство текущей скорости главного героя
    /// </summary>
    Vector2 CharacterRbVelocity
    {
        get => characterRb.velocity;
    }

    /// <summary>
    /// Свойство проверки положения персонажа на земле
    /// </summary>
    public bool IsGrounded
    {
        get => Physics2D.OverlapCapsule(groundColliderTransform.position, new Vector2(jumpOffset, jumpOffset), CapsuleDirection2D.Horizontal, 0, groundMask);
    }

    private void Start()
    {
        // Получение компонента RigitBody героя
        //characterRb = GetComponentInChildren<Rigidbody2D>();
        characterRb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Метод передвижения главного героя
    /// </summary>
    /// <param name="side"></param>
    public void Move(float side)
    {
        if (side != 0)
            WarriorTransform.rotation = RotateSide(side);

        #region remember_this
        //var absSide = Mathf.Abs(side);
        // Перемещение героя по горизонтали
        //if (!isPhysicsMove)
        //    transform.Translate(Vector2.right * absSide * characterSpeed * Time.deltaTime);
        //else
        //    characterRb.AddForce(Vector2.right * absSide * characterSpeed);
        #endregion

        // Перемещение героя по горизонтали
        if (!isPhysicsMove)
            transform.Translate(Vector2.right * side * characterSpeed * Time.deltaTime);
        else
            characterRb.AddForce(Vector2.right * side * characterSpeed);

        // Ограничение скорости передвижения персонажа
        characterRb.velocity = new Vector2(Mathf.Clamp(CharacterRbVelocity.x, -maxSpeed, maxSpeed), CharacterRbVelocity.y);
    }

    Quaternion RotateSide(float side)
    {
        var rotateY = side > 0 ? Quaternion.Euler(WarriorTransform.rotation.x, 0, WarriorTransform.rotation.z) :
                 Quaternion.Euler(WarriorTransform.rotation.x, 180, WarriorTransform.rotation.z);

        return rotateY;
    }

    /// <summary>
    /// Метод прыжка главного героя
    /// </summary>
    /// <param name="jumpDirection"></param>
    public void Jump(Vector2 jumpDirection)
    {
        if (IsGrounded)
            // Физическое воздействие на главного героя
            characterRb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Установка родителем главного героя движущегося предмета
        // для передвижения вместе с ним
        if (collision.transform.tag.ToLower() == "movable")
            transform.SetParent(collision.transform);
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
