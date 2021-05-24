using UnityEngine;

public class MenuCharactermove : MonoBehaviour
{
    [SerializeField] Rigidbody2D characterRb;
    Animator animator;

    /// <summary>
    /// Свойство текущей скорости главного героя
    /// </summary>
    Vector2 CharacterRbVelocity
    {
        get => characterRb.velocity;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("Run", true);
    }

    //private void Update()
    //{
    //    Move();
    //}

    ///// <summary>
    ///// Метод передвижения главного героя
    ///// </summary>
    ///// <param name="side"></param>
    //public void Move()
    //{
    //    transform.Translate(Vector2.right * 1 * characterSpeed * Time.deltaTime);

    //    // Ограничение скорости передвижения персонажа
    //    characterRb.velocity = new Vector2(Mathf.Clamp(CharacterRbVelocity.x, -maxSpeed, maxSpeed), CharacterRbVelocity.y);
    //}
}
