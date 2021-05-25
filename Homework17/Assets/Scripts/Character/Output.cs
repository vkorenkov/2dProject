using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    [HideInInspector] public AnimationActivator animationActivator = new AnimationActivator();

    [SerializeField] Transform hudPosition;
    /// <summary>
    /// Текст вывода здоровья
    /// </summary>
    [SerializeField] public TextMeshPro healthCount;
    /// <summary>
    /// Текст вывода количетва снарядов
    /// </summary>
    [SerializeField] private TextMeshPro projectileCount;
    /// <summary>
    /// Текст вывода количетва убитых врагов
    /// </summary>
    [SerializeField] private TextMeshPro killedCount;
    /// <summary>
    /// Текст вывода количетва поднятых предметов
    /// </summary>
    [SerializeField] private TextMeshPro bonusCount;
    /// <summary>
    /// Текст вывода фраз персонажа
    /// </summary>
    [SerializeField] public TextMeshPro dialogText;
    /// <summary>
    /// Таймер отображения фразы
    /// </summary>
    [HideInInspector] public float timer;
    /// <summary>
    /// Запуск таймера
    /// </summary>
    [HideInInspector] public bool goTimer;

    private void Update()
    {
        if (hudPosition) transform.position = hudPosition.position;

        // Положение и поворот тектста в разивисимости от поворота персонажа
        if (healthCount) healthCount.transform.rotation = TextRotation();
        if (projectileCount) projectileCount.transform.rotation = TextRotation();
        if (bonusCount) bonusCount.transform.rotation = TextRotation();

        if (goTimer)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                goTimer = false;
                animationActivator.AnimationPlayback(dialogText, false);
            }
        }
    }

    /// <summary>
    /// Поворачивает текст в зависимости от взгляда персонажа
    /// </summary>
    /// <returns></returns>
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
        healthCount.text = $"{count} %";
    }

    /// <summary>
    /// Выводи количество здоровья на экран
    /// </summary>
    /// <param name="count"></param>
    public void OutputProjectilesCount(string count)
    {
        projectileCount.text = count;
    }

    /// <summary>
    /// Выводит количество собраных предметов на экран
    /// </summary>
    /// <param name="count"></param>
    public void OutputBonusCount(string count)
    {
        bonusCount.text = count;
    }

    /// <summary>
    /// Выводит количество убитых противников на экран
    /// </summary>
    /// <param name="count"></param>
    public void OutputKillsCount(string count)
    {
        if(killedCount) killedCount.text = count;
    }

    /// <summary>
    /// Выводит фразы ГГ на экран
    /// </summary>
    /// <param name="count"></param>
    public void OutputDialog(string dialog)
    {
        dialogText.text = dialog;
        animationActivator.AnimationPlayback(dialogText, true);
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
