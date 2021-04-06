using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //Vector2 destination;
    //bool isMove;
    GameObject warrior;

    private void Awake()
    {
        warrior = GameObject.Find("Warrior");
    }

    private void Update()
    {
        transform.position = new Vector2(warrior.transform.position.x, warrior.transform.position.y + 0.5f);

        #region variant
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    isMove = true;
        //    destination = new Vector2(transform.position.x + 2, transform.position.y);
        //}

        //if (isMove)
        //{
        //    cameraPosition.position = Vector2.Lerp(transform.position, destination, 1);

        //    if (Vector2.Distance(transform.position, destination) > 0.1f)
        //        isMove = false;
        //}
        #endregion
    }
}
