using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    [SerializeField] SliderJoint2D slider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        slider.useMotor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        slider.useMotor = false;
    }
}
