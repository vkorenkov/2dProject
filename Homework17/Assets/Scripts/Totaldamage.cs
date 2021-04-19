using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totaldamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
        {
            collisionObjHealth.TakeDamage(true);
        }
    }
}
