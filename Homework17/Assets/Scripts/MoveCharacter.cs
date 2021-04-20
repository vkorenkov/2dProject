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
    [SerializeField, Range(0.1f, 3f)] float jumpForce;

    [SerializeField] float jumpOffset;

    [SerializeField] Transform groundColliderTransform;

    [SerializeField] LayerMask groundMask;

    [SerializeField] bool isPhysicsMove;

    /// <summary>
    /// Свойство текущей скорости главного героя
    /// </summary>
    Vector2 CharacterRbVelocity
    {
        get => characterRb.velocity;
    }

    public bool IsGrounded
    {
        get => Physics2D.OverlapCapsule(groundColliderTransform.position, new Vector2(jumpOffset, jumpOffset), CapsuleDirection2D.Horizontal, 0, groundMask);
    }

    private void Start()
    {
        // Получение компонента RigitBody героя
        characterRb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Метод передвижения главного героя
    /// </summary>
    /// <param name="side"></param>
    public void Move(float side)
    {
        var absSide = Mathf.Abs(side);

        if (side != 0)
            transform.rotation = RotateSide(side);

        // Перемещение героя по горизонтали
        if (!isPhysicsMove)
            transform.Translate(Vector2.right * absSide * characterSpeed * Time.deltaTime);
        else
            characterRb.AddForce(Vector2.right * absSide * characterSpeed);

        // Ограничение скорости передвижения персонажа
        characterRb.velocity = new Vector2(Mathf.Clamp(CharacterRbVelocity.x, -maxSpeed, maxSpeed), CharacterRbVelocity.y);
    }

    Quaternion RotateSide(float side)
    {
        var rotateY = side > 0 ? Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z) :
                 Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);

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
