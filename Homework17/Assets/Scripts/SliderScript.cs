using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    /// <summary>
    /// Поле соединения
    /// </summary>
    [SerializeField] SliderJoint2D slider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.ToLower() == "player" || collision.tag.ToLower() == "movable")
            // Зпуск движения
            slider.useMotor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.ToLower() == "player" || collision.tag.ToLower() == "movable")
            // Остановка движения
            slider.useMotor = false;
    }
}
