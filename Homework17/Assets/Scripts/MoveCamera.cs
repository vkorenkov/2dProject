using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //Vector2 destination;
    //bool isMove;
    /// <summary>
    /// Поле главного героя
    /// </summary>
    GameObject warrior;

    [SerializeField] bool movecamera;

    private void Awake()
    {
        movecamera = true;        

        // Иницализация поля главного героя
        warrior = GameObject.Find("Warrior");

        warrior.GetComponent<HealthManager>().ControlEnableEvent += MoveCamera_ControlEnableEvent;
    }

    private void MoveCamera_ControlEnableEvent(bool isAlive)
    {
        movecamera = isAlive;
    }

    private void Update()
    {
        if (movecamera)
        {
            if (warrior.transform.position.x > -1.15f)
                // Изменение положения камеры в след за героем
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
}
