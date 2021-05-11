using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectActivate : MonoBehaviour
{
    [SerializeField, Space] GameObject activateObject;
    [SerializeField, Header("Is Coroutine parameters")] bool isCoroutine;
    [SerializeField] float activeteTime;
    [SerializeField] float deactivationTime;
    bool isActivate;

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
            if (collision.tag.ToLower() == "player" || collision.tag.ToLower() == "enemy")
            {
                isActivate = !isActivate;
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
            if (collision.tag.ToLower() == "player" || collision.tag.ToLower() == "enemy")
            {
                isActivate = !isActivate;
                StopCoroutine(SetObjectActiveCoroutine());
            }
        }
        else
            activateObject.SetActive(false);
    }
}
