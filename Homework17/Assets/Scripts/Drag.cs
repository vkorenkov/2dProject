using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    Vector3 offset;
    Rigidbody2D rb;
    new Camera camera;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        offset = transform.position -
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
    }

    void OnMouseDrag()
    {
        rb.velocity = new Vector2();
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }
}
