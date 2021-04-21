using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombScript : MonoBehaviour
{
    /// <summary>
    /// ������� ���� ������� �����
    /// </summary>
    Rigidbody2D bombRb;
    /// <summary>
    /// ������� ������ ������� ������
    /// </summary>
    ParticleSystem explosionPs;
    /// <summary>
    /// �������� �������� ������
    /// </summary>
    PointEffector2D bombEffector;
    /// <summary>
    /// ����������� �����
    /// </summary>
    SpriteRenderer bombSprite;
    /// <summary>
    /// ��������� ��������� ������
    /// </summary>
    CircleCollider2D bombPointEffector;

    private void Awake()
    {
        bombRb = GetComponent<Rigidbody2D>(); // ��������� �������� ���� ������� �����
        explosionPs = GetComponentInChildren<ParticleSystem>(); // ��������� ������ �������� ������
        bombEffector = GetComponent<PointEffector2D>(); // ��������� ��������� �����
        bombSprite = GetComponent<SpriteRenderer>(); // ��������� ����������� �����
        bombPointEffector = GetComponent<CircleCollider2D>(); // ��������� ���������� �����

        bombRb.AddForce(transform.up * Random.Range(2, 5), ForceMode2D.Impulse); // ��������� ������� ������� �����
        bombRb.AddTorque(Random.Range(-2, 2), ForceMode2D.Impulse); // �������� �������� �����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bombSprite.enabled)
        {
            Destroy(gameObject, explosionPs.main.startLifetime.constantMax); // ������ ����������� ������� ����� �� ������� ������ ������
            explosionPs.Play(); // ������ ������� ������
            bombSprite.enabled = false; // ���������� ����������� �����
            bombPointEffector.enabled = true; // ��������� ��������� ��������� �������� ������
            bombEffector.enabled = true; // // ��������� ��������� �������� ������
            bombRb.constraints = RigidbodyConstraints2D.FreezePositionX; // "���������" �������� ����� �� ��� X
        }
    }
}
