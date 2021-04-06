using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMotorScript : MonoBehaviour
{
    HingeJoint2D hinge;
    float timer;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        hinge.useMotor = true;
        

        timer = Random.Range(0.1f, 2);
    }

    private void FixedUpdate()
    {
        bool isLimits = hinge.jointAngle <= hinge.limits.max && timer <= 0 || hinge.jointAngle >= hinge.limits.min && timer <= 0;

        timer -= Time.deltaTime;

        if (isLimits)
        {
            var motorSpeed = hinge.motor;
            motorSpeed.motorSpeed *= -1;
            hinge.motor = motorSpeed;
            timer = Random.Range(0.1f, 2);
        }
    }
}
