using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtack : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] float damage;
    [SerializeField] LayerMask layerMask;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Attack()
    {
        var damageObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layerMask);

        foreach (var d in damageObjects)
        {
            var damageObject = d.GetComponent<HealthManager>();
            damageObject.TakeDamage(false, damage);
            if (!damageObject.isAlive)
                GetComponentInParent<CollectObjects>().killedEnemies += 1;
        }
    }
}
