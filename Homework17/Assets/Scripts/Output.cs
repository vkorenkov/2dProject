using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    [SerializeField] Transform hudPosition;
    /// <summary>
    /// Текст вывода здоровья
    /// </summary>
    [SerializeField] public TextMeshPro healthCount;
    /// <summary>
    /// Текст вывода количетва снарядов
    /// </summary>
    [SerializeField] public TextMeshPro projectileCount;

    [SerializeField] public TextMeshPro bonusCount;

    private void Update()
    {
        if (hudPosition) transform.position = hudPosition.position;

        // Положение и поворот тектста в разивисимости от поворота персонажа
        if (healthCount) healthCount.transform.rotation = TextRotation();
        if (projectileCount) projectileCount.transform.rotation = TextRotation();
        if (bonusCount) bonusCount.transform.rotation = TextRotation();
    }

    Quaternion TextRotation()
    {
        return transform.rotation.y < 0 || transform.rotation.y > 0 ? Quaternion.identity : new Quaternion();
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
    /// Метод вывода здоровья на экран
    /// </summary>
    /// <param name="count"></param>
    public void OutputProjectilesCount(string count)
    {
        projectileCount.text = count;
    }

    public void OutputBonusCount(string count)
    {
        bonusCount.text = count;
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
