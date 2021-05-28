using UnityEngine;

public class MeleeAtack : MonoBehaviour
{
    /// <summary>
    /// Точка ближней атаки
    /// </summary>
    [SerializeField] Transform attackPoint;
    /// <summary>
    /// Радиус атаки
    /// </summary>
    [SerializeField] float attackRange;
    /// <summary>
    /// Урон
    /// </summary>
    [SerializeField] float damage;
    /// <summary>
    /// Маска объекта получения урона
    /// </summary>
    [SerializeField] LayerMask layerMask;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Attack()
    {
        // Получение всех объектов попавших в радиус поражения
        var damageObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layerMask);

        // Нанесение урона
        foreach (var d in damageObjects)
        {
            var damageObject = d.GetComponent<HealthManager>();
            damageObject.TakeDamage(false, damage);

            if (!damageObject.isAlive && damageObject.CompareTag("Enemy"))
                GetComponentInParent<CollectObjects>().collected.KilledEnemiesCount += 1;
        }
    }
}
