using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCharacter : MonoBehaviour
{
    MoveCharacter move;
    float horisontalAxis;

    private void Start()
    {
        move = GetComponent<MoveCharacter>();
    }

    void Update()
    {
        horisontalAxis = Input.GetAxis("Horizontal");        
    }

    void FixedUpdate()
    {
        move.Move(horisontalAxis);
    }
}
