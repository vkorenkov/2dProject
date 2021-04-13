using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCharacter : MonoBehaviour
{
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
    /// Поле спрайта персонажа
    /// </summary>
    SpriteRenderer characterSprite;

    private void Start()
    {
        // Инициализация эеземпляра класса передвижения персонажа
        move = GetComponent<MoveCharacter>();
        // Получение ссылки на спрайт персонажа
        characterSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Считывание нажатия кнопок направления
        horisontalAxis = Input.GetAxisRaw("Horizontal");

        // Разворот спрайта в сторону движения
        if (horisontalAxis > 0)
            characterSprite.flipX = false;
        if(horisontalAxis < 0)
            characterSprite.flipX = true;

        // считывание нажатия кнопки прыжка
        if (Input.GetButtonDown("Jump"))
            jump = Vector2.up;
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
}
