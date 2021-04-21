using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    /// <summary>
    /// Текст вывода здоровья
    /// </summary>
    [SerializeField] public TextMeshPro healthCount;
    /// <summary>
    /// Свойство поворота персонажа
    /// </summary>
    Quaternion RotationY
    {
        // Воздващает поворот текста
        get => new Quaternion(healthCount.transform.rotation.x, 0, healthCount.transform.rotation.z, healthCount.transform.rotation.w);
    }

    private void Update()
    {
        // Положение и поворот тектста в разивисимости от поворота персонажа
        healthCount.transform.rotation = transform.rotation.y < 0 ?
            RotationY : new Quaternion();
    }

    /// <summary>
    /// Метод вывода здоровья на экран
    /// </summary>
    /// <param name="count"></param>
    public void OutputHealthCount(string count)
    {
        healthCount.text = count;
    }

    /// <summary>
    /// Метод изменения цвета текста
    /// </summary>
    /// <param name="textObj"></param>
    /// <param name="color"></param>
    public void ChangeTextColor(TextMeshPro textObj, Color color)
    {
        textObj.color = color;
    }
}
