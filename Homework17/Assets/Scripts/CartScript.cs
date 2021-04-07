using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CartScript : MonoBehaviour
{
    List<WheelJoint2D> wheels;

    private void Start()
    {
        wheels = GetComponentsInChildren<WheelJoint2D>().ToList();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag.ToLower() == "player")
                RotateWheels(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.ToLower() == "player")
            RotateWheels(false);
    }

    private void RotateWheels(bool isRotate)
    {
        foreach (var w in wheels)
        {
            w.useMotor = isRotate;
            w.GetComponent<Rigidbody2D>().freezeRotation = !isRotate;
        }
    }
}
