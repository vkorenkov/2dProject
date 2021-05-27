using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    /// <summary>
    /// ������ ������
    /// </summary>
    GameObject player;
    /// <summary>
    /// ������ �������
    /// </summary>
    HealthManager health;
    /// <summary>
    /// �����������, ��� ���� ����� ���������
    /// </summary>
    [SerializeField] bool thisMustMove;
    /// <summary>
    /// �������� ������������
    /// </summary>
    [SerializeField, Range(0.1f, 1)] float speed;
    /// <summary>
    /// �������� ��������
    /// </summary>
    Animator moveAnimation;
    /// <summary>
    /// ��������, ��� ������ ������ ��������� � ������� ������
    /// </summary>
    bool isMovable;
    /// <summary>
    /// ���������� �� ������� ���� �������� ���������
    /// </summary>
    [SerializeField] float moveDistance = 1;

    /// <summary>
    /// �������� ��������� ����� �� � ������
    /// </summary>
    float distance
    {
        get => Vector2.Distance(player.transform.position, transform.position);
    }

    /// <summary>
    /// �������� ������� ������� ������
    /// </summary>
    Vector2 playerPosition
    {
        get => player.transform.position;
    }

    private void Awake()
    {
        moveAnimation = GetComponent<Animator>();
        health = GetComponent<HealthManager>();
        health.ControlEnableEvent += Health_ControlEnableEvent;
        player = GameObject.FindGameObjectWithTag("Player");
        isMovable = true;
    }

    /// <summary>
    /// ���������� ������� ���������\���������� ���� ���� �����
    /// </summary>
    /// <param name="isAlive"></param>
    private void Health_ControlEnableEvent(bool isAlive)
    {
        isMovable = isAlive;
        moveAnimation.SetBool("Walk", false);
    }

    private void FixedUpdate()
    {
        if (isMovable && thisMustMove)
        {
            // ����������� ����� � ������� ��
            if (distance < moveDistance && health.isAlive)
            {
                moveAnimation.SetBool("Walk", isMovable);

                var newPosition = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

                transform.position = new Vector2(newPosition.x, transform.position.y);

                transform.rotation = EnemySight();
            }
            else
                moveAnimation.SetBool("Walk", false);
        }
    }

    /// <summary>
    /// ������� ������� �����
    /// </summary>
    /// <returns></returns>
    Quaternion EnemySight()
    {
        return playerPosition.x < transform.position.x ? Quaternion.identity : new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
    }
}
