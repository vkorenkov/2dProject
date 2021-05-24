using UnityEngine;

public class MenuCharactermove : MonoBehaviour
{
    [SerializeField] Rigidbody2D characterRb;
    Animator animator;

    /// <summary>
    /// �������� ������� �������� �������� �����
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
    ///// ����� ������������ �������� �����
    ///// </summary>
    ///// <param name="side"></param>
    //public void Move()
    //{
    //    transform.Translate(Vector2.right * 1 * characterSpeed * Time.deltaTime);

    //    // ����������� �������� ������������ ���������
    //    characterRb.velocity = new Vector2(Mathf.Clamp(CharacterRbVelocity.x, -maxSpeed, maxSpeed), CharacterRbVelocity.y);
    //}
}
