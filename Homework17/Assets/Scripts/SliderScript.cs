using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    private JointMotor2D sliderMotor;
    [SerializeField] bool isFullMotor;
    [SerializeField] bool isMovePlatform;
    /// <summary>
    /// Поле соединения
    /// </summary>
    [SerializeField] SliderJoint2D slider;
    [SerializeField] float motorForce = 5;
    Transform parent;

    private void Awake()
    {
        if (isMovePlatform) isFullMotor = true;

        if (slider && isFullMotor)
        {
            sliderMotor = slider.motor;
            slider.useMotor = true;
        }
    }

    private void FixedUpdate()
    {
        if(isMovePlatform)
        {
            bool isLimit = slider.jointTranslation <= slider.limits.min || slider.jointTranslation >= slider.limits.max;

            if (isLimit)
            {
                sliderMotor.motorSpeed = sliderMotor.motorSpeed * -1;
                slider.motor = sliderMotor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isMovePlatform && collision.tag.ToLower() == "player" || collision.tag.ToLower() == "movable")
        {
            if (!isFullMotor)
                // Зпуск движения
                slider.useMotor = true;
            else
            {
                sliderMotor.motorSpeed = motorForce * -1;
                slider.motor = sliderMotor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isMovePlatform && collision.tag.ToLower() == "player" || collision.tag.ToLower() == "movable")
        {
            if (!isFullMotor)
                // Остановка движения
                slider.useMotor = false;
            else
            {               
                sliderMotor.motorSpeed = motorForce;
                slider.motor = sliderMotor;
            }
        }
    }
}
