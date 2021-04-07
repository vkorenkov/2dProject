using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeakScript : MonoBehaviour
{
    SliderJoint2D slider;

    private void Start()
    {
        slider = GetComponentInChildren<SliderJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        slider.useMotor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        slider.useMotor = false;
    }
}