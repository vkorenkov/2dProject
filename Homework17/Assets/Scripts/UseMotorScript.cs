using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMotorScript : MonoBehaviour
{
    /// <summary>
    /// Поле соединения
    /// </summary>
    HingeJoint2D hinge;
    /// <summary>
    /// Таймер запуска motor
    /// </summary>
    float timer;

    void Start()
    {
        // Получение соединения
        hinge = GetComponent<HingeJoint2D>();
        // запуск motor
        hinge.useMotor = true;
        
        // Установка случайного значения таймера
        timer = Random.Range(0.1f, 2);
    }

    private void FixedUpdate()
    {
        // Условия выполнения действий motor
        bool isLimits = hinge.jointAngle <= hinge.limits.max && timer <= 0 || hinge.jointAngle >= hinge.limits.min && timer <= 0;

        // Изменение таймера
        timer -= Time.deltaTime;

        if (isLimits)
        {
            // Ссылка на motor
            var motorSpeed = hinge.motor;
            // Изменение направления motor
            motorSpeed.motorSpeed *= -1;
            // присвоение значений motor
            hinge.motor = motorSpeed;
            // Установка случайного значения таймера
            timer = Random.Range(0.1f, 2);
        }
    }
}
