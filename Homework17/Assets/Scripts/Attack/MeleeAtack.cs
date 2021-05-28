using UnityEngine;

public class MeleeAtack : MonoBehaviour
{
    /// <summary>
    /// ����� ������� �����
    /// </summary>
    [SerializeField] Transform attackPoint;
    /// <summary>
    /// ������ �����
    /// </summary>
    [SerializeField] float attackRange;
    /// <summary>
    /// ����
    /// </summary>
    [SerializeField] float damage;
    /// <summary>
    /// ����� ������� ��������� �����
    /// </summary>
    [SerializeField] LayerMask layerMask;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Attack()
    {
        // ��������� ���� �������� �������� � ������ ���������
        var damageObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layerMask);

        // ��������� �����
        foreach (var d in damageObjects)
        {
            var damageObject = d.GetComponent<HealthManager>();
            damageObject.TakeDamage(false, damage);

            if (!damageObject.isAlive && damageObject.CompareTag("Enemy"))
                GetComponentInParent<CollectObjects>().collected.KilledEnemiesCount += 1;
        }
    }
}
