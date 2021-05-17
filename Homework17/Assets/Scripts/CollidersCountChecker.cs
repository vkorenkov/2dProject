using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersCountChecker : MonoBehaviour
{
    List<Collider2D> colliders;

    private void Awake()
    {
        colliders = new List<Collider2D>();
    }

    private void Update()
    {
        Debug.Log(colliders.Count);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            colliders.Add(collision);

            if (colliders.Count > 5)
            {
                StartStopPlatform(0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            colliders.Remove(collision);

            if (colliders.Count < 4)
            {
                StartStopPlatform(1);
            }
        }
    }

    void StartStopPlatform(float speed)
    {
        var slider = GetComponent<SliderJoint2D>();
        var motor = slider.motor;
        motor.motorSpeed = speed;
        slider.motor = motor;
    }
}
