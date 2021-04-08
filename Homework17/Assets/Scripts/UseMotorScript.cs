using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMotorScript : MonoBehaviour
{
    /// <summary>
    /// ���� ����������
    /// </summary>
    HingeJoint2D hinge;
    /// <summary>
    /// ������ ������� motor
    /// </summary>
    float timer;

    void Start()
    {
        // ��������� ����������
        hinge = GetComponent<HingeJoint2D>();
        // ������ motor
        hinge.useMotor = true;
        
        // ��������� ���������� �������� �������
        timer = Random.Range(0.1f, 2);
    }

    private void FixedUpdate()
    {
        // ������� ���������� �������� motor
        bool isLimits = hinge.jointAngle <= hinge.limits.max && timer <= 0 || hinge.jointAngle >= hinge.limits.min && timer <= 0;

        // ��������� �������
        timer -= Time.deltaTime;

        if (isLimits)
        {
            // ������ �� motor
            var motorSpeed = hinge.motor;
            // ��������� ����������� motor
            motorSpeed.motorSpeed *= -1;
            // ���������� �������� motor
            hinge.motor = motorSpeed;
            // ��������� ���������� �������� �������
            timer = Random.Range(0.1f, 2);
        }
    }
}
