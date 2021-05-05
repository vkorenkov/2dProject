using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtack : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] float damage;
    [SerializeField] float attackCoolDown;
    [SerializeField] Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Attack()
    {

    }
}
