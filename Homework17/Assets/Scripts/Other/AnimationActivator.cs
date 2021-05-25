using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationActivator
{
    /// <summary>
    /// ����� ������� ��������� �������� ������
    /// </summary>
    /// <param name="text"></param>
    /// <param name="side"></param>
    public void AnimationPlayback(TextMeshPro text, bool side)
    {
        // ��������� ��������
        var DescriptionAnimation = text.GetComponent<Animation>();
        int speed = 1;

        // ��������� ����������� ��������
        if (!side)
        {
            speed *= -1;
            DescriptionAnimation["DescriptionAnimation"].time = DescriptionAnimation["DescriptionAnimation"].length;
        }

        // ������ ��������
        DescriptionAnimation["DescriptionAnimation"].speed = speed;
        DescriptionAnimation.Play();
    }
}
