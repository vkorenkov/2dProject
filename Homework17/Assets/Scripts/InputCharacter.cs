using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCharacter : MonoBehaviour
{
    MoveCharacter move;
    float horisontalAxis;
    Vector2 jump;

    private void Start()
    {
        move = GetComponent<MoveCharacter>();
    }

    void Update()
    {
        horisontalAxis = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
            jump = Vector2.up;
    }

    void FixedUpdate()
    {
        move.Move(horisontalAxis);

        move.Jump(jump);

        jump = new Vector2();
    }
}
