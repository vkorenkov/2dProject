using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationActivator
{
    /// <summary>
    /// Метод запуска отдельной анимации текста
    /// </summary>
    /// <param name="text"></param>
    /// <param name="side"></param>
    public void AnimationPlayback(TextMeshPro text, bool side)
    {
        // Получение анимации
        var DescriptionAnimation = text.GetComponent<Animation>();
        int speed = 1;

        // Изменение направления анимации
        if (!side)
        {
            speed *= -1;
            DescriptionAnimation["DescriptionAnimation"].time = DescriptionAnimation["DescriptionAnimation"].length;
        }

        // Запуск анимации
        DescriptionAnimation["DescriptionAnimation"].speed = speed;
        DescriptionAnimation.Play();
    }
}
