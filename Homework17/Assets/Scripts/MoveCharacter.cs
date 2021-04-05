using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    Rigidbody2D characterRb;
    float characterSpeed = 2;
    [SerializeField] float maxSpeed = 0.1f;
    public float height;
    Animator animator;
    [SerializeField, Range(0.1f, 1)] float jumpForce;

    Vector2 characterRbVelocity
    {
        get => characterRb.velocity;
    }

    private void Start()
    {
        characterRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(float side)
    {
        transform.Translate(Vector2.right * side * characterSpeed * Time.deltaTime);
        characterRb.velocity = new Vector2(Mathf.Clamp(characterRbVelocity.x, -maxSpeed, maxSpeed), characterRbVelocity.y);

        if (side > 0 || side < 0)
            animator.SetBool("Run", true);
        else
            animator.SetBool("Run", false);
    }

    public void Jump(Vector2 jumpDirection)
    {
        characterRb.AddForce(jumpDirection * jumpForce * 3, ForceMode2D.Impulse);
    }
}
