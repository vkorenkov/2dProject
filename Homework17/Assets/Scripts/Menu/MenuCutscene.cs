using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuCutscene : MonoBehaviour
{
    /// <summary>
    /// Коллекция объектов земли
    /// </summary>
    [SerializeField] List<GameObject> grounds;
    /// <summary>
    /// Первый объект земли
    /// </summary>
    [SerializeField] GameObject startPart;
    /// <summary>
    /// Скорость движения объектов земли
    /// </summary>
    [SerializeField] float moveSpeed = 2;
    /// <summary>
    /// Длина объекта земли
    /// </summary>
    float partLenght;
    /// <summary>
    /// Максимальное количество объектов земли на экране
    /// </summary>
    [SerializeField] int partsCount = 3;
    /// <summary>
    /// Коллекция текущих объектов земли
    /// </summary>
    public static List<GameObject> currentParts;

    private void Start()
    {
        partLenght = startPart.GetComponent<BoxCollider2D>().bounds.size.x; // Получение длины объекта
        currentParts = new List<GameObject>();
        currentParts.Add(startPart);

        for (var i = 0; i < partsCount; i++)
        {
            SetParts();
        }
    }

    private void Update()
    {
        Move();

        if (currentParts.Count < partsCount)
        {
            SetParts();
        }
    }

    /// <summary>
    /// Перемещение объектов земли для создания иллюзии движения
    /// </summary>
    void Move()
    {
        transform.Translate(Vector2.right * -1 * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Установка объекта земли в конец предыдущего
    /// </summary>
    void SetParts()
    {
        GameObject part = Instantiate(grounds[Random.Range(0, grounds.Count)], transform); // Установка объекта земли
        var temp = currentParts.Last().transform.position.x; // Получение конечной точки коллайдера предыдущего объекта
        temp += partLenght - 0.02f; // Получение точки установки следующего объекта
        part.transform.position = new Vector3(temp, 0, 0); // Установка объекта
        currentParts.Add(part);
    }
}
