using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CartScript : MonoBehaviour
{
    // Коллекция колес
    List<WheelJoint2D> wheels;

    private void Start()
    {
        // Получение соединений колес
        wheels = GetComponentsInChildren<WheelJoint2D>().ToList();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Запуск motor при коллизии с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            RotateWheels(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Отключение motor при коллизии с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            RotateWheels(false);
        }
    }

    /// <summary>
    /// Метод запуска вращения колес
    /// </summary>
    /// <param name="isRotate"></param>
    private void RotateWheels(bool isRotate)
    {
        foreach (var w in wheels)
        {
            w.useMotor = isRotate;
            w.GetComponent<Rigidbody2D>().freezeRotation = !isRotate;
        }
    }
}
