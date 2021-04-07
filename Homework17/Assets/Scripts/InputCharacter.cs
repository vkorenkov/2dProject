using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCharacter : MonoBehaviour
{
    MoveCharacter move;
    float horisontalAxis;
    Vector2 jump;
    SpriteRenderer characterSprite;

    private void Start()
    {
        move = GetComponent<MoveCharacter>();
        characterSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horisontalAxis = Input.GetAxis("Horizontal");

        if (horisontalAxis > 0)
            characterSprite.flipX = false;
        if(horisontalAxis < 0)
            characterSprite.flipX = true;

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
