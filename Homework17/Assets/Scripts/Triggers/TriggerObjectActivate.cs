using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectActivate : MonoBehaviour
{
    /// <summary>
    /// ������������ ������
    /// </summary>
    [SerializeField, Space] GameObject activateObject;
    /// <summary>
    /// ��������, ��� ���� ������ ���������� ��������� ��������� � ��������� SetObjectActiveCoroutine
    /// </summary>
    [SerializeField, Header("Is Coroutine parameters")] bool isCoroutine;
    /// <summary>
    /// ������ ��������� �������
    /// </summary>
    [SerializeField] float activeteTime;
    /// <summary>
    /// ����� ����������� �������
    /// </summary>
    [SerializeField] float deactivationTime;
    /// <summary>
    /// �������� ����������� �� ������
    /// </summary>
    bool isActivate;

    /// <summary>
    /// ���������� ������ ����� ���������� ������� activeteTime � ������������ ����� deactivationTime
    /// </summary>
    /// <returns></returns>
    IEnumerator SetObjectActiveCoroutine()
    {
        while (isActivate)
        {
            yield return new WaitForSeconds(activeteTime);

            activateObject.SetActive(true);

            yield return new WaitForSeconds(deactivationTime);

            activateObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCoroutine)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
            {
                isActivate = true;
                StartCoroutine(SetObjectActiveCoroutine());
            }
        }
        else
            activateObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isCoroutine)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
            {
                isActivate = false;
                StopCoroutine(SetObjectActiveCoroutine());
            }
        }
        else
            activateObject.SetActive(false);
    }
}
