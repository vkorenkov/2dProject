using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuCutscene : MonoBehaviour
{
    /// <summary>
    /// ��������� �������� �����
    /// </summary>
    [SerializeField] List<GameObject> grounds;
    /// <summary>
    /// ������ ������ �����
    /// </summary>
    [SerializeField] GameObject startPart;
    /// <summary>
    /// �������� �������� �������� �����
    /// </summary>
    [SerializeField] float moveSpeed = 2;
    /// <summary>
    /// ����� ������� �����
    /// </summary>
    float partLenght;
    /// <summary>
    /// ������������ ���������� �������� ����� �� ������
    /// </summary>
    [SerializeField] int partsCount = 3;
    /// <summary>
    /// ��������� ������� �������� �����
    /// </summary>
    public static List<GameObject> currentParts;

    private void Start()
    {
        partLenght = startPart.GetComponent<BoxCollider2D>().bounds.size.x; // ��������� ����� �������
        currentParts = new List<GameObject>();
        currentParts.Add(startPart);

        for (var i = 0; i < partsCount; i++)
        {
            SetParts();
        }
    }

    private void Update()
    {
        Move();

        if (currentParts.Count < partsCount)
        {
            SetParts();
        }
    }

    /// <summary>
    /// ����������� �������� ����� ��� �������� ������� ��������
    /// </summary>
    void Move()
    {
        transform.Translate(Vector2.right * -1 * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// ��������� ������� ����� � ����� �����������
    /// </summary>
    void SetParts()
    {
        GameObject part = Instantiate(grounds[Random.Range(0, grounds.Count)], transform); // ��������� ������� �����
        var temp = currentParts.Last().transform.position.x; // ��������� �������� ����� ���������� ����������� �������
        temp += partLenght - 0.02f; // ��������� ����� ��������� ���������� �������
        part.transform.position = new Vector3(temp, 0, 0); // ��������� �������
        currentParts.Add(part);
    }
}
