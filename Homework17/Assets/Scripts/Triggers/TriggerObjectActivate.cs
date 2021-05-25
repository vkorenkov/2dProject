using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectActivate : MonoBehaviour
{
    /// <summary>
    /// Активируемый объект
    /// </summary>
    [SerializeField, Space] GameObject activateObject;
    /// <summary>
    /// Указание, что этот объект необходимо запускать совместно с корутиной SetObjectActiveCoroutine
    /// </summary>
    [SerializeField, Header("Is Coroutine parameters")] bool isCoroutine;
    /// <summary>
    /// Таймер активации объекта
    /// </summary>
    [SerializeField] float activeteTime;
    /// <summary>
    /// Время деактивации объекта
    /// </summary>
    [SerializeField] float deactivationTime;
    /// <summary>
    /// Указание активирован ли объект
    /// </summary>
    bool isActivate;

    /// <summary>
    /// Активирует объект через промежуток времени activeteTime и деактивирует через deactivationTime
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
