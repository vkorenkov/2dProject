using UnityEngine;

public class Drag : MonoBehaviour/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    /// <summary>
    /// Поле положения курсора при нажатии
    /// </summary>
    Vector3 offset;
    /// <summary>
    /// Поле RigitBody героя
    /// </summary>
    Rigidbody2D rb;
    /// <summary>
    /// Поле главной камеры
    /// </summary>
    new Camera camera;

    void Awake()
    {
        // Получение компонента RigitBody
        rb = GetComponent<Rigidbody2D>();
        // Получение камеры
        camera = Camera.main;
    }

    #region interface realization
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    offset = transform.position -
    //        Camera.main.ScreenToWorldPoint(eventData.position);
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    Vector3 newPosition = eventData.position;
    //    newPosition.z = 0;
    //    transform.position = newPosition + offset;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Vector3 newPosition = eventData.position;
    //    transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    //}
    #endregion

    private void OnMouseDown()
    {
        // расчет положения курсора при захвате модели персонажа
        offset = transform.position -
            camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
    }

    void OnMouseDrag()
    {
        // Сброс скорости модели персонажа
        rb.velocity = new Vector2();
        // Изменение позиции модели персонажа при перетаскивании
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        // Присвоение новой позиции
        transform.position = camera.ScreenToWorldPoint(newPosition) + offset;
    }
}
