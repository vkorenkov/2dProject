using UnityEngine;

public class StaticdamageObj : MonoBehaviour
{
    /// <summary>
    /// ���������� ��, ��� ��������� ������� �������� ���������
    /// </summary>
    [SerializeField, Header("Trigger")] bool isTriggerObj;
    /// <summary>
    /// ���� ��������� ��������
    /// </summary>
    [SerializeField] float damage;
    /// <summary>
    /// ���� ������������
    /// </summary>
    [SerializeField] float pushForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggerObj && collision.gameObject.CompareTag("Player"))
        {
            var playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.AddForce((playerRb.transform.position - transform.position).normalized * pushForce, ForceMode2D.Impulse);
            Damage(collision, damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggerObj && collision.CompareTag("Player"))
            Damage(collision, damage);
    }

    /// <summary>
    /// ����� ��������� ����� ��� ������������ � ��������, ������� ����� ��������� HealthManager
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="damage"></param>
    void Damage(Collision2D collision, float damage)
    {
        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
            collisionObjHealth.TakeDamage(false, damage);
    }

    /// <summary>
    /// ����� ��������� ����� ��� ������������ � ��������, ������� ����� ��������� HealthManager
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="damage"></param>
    void Damage(Collider2D collision, float damage)
    {
        if (collision.gameObject.TryGetComponent(out HealthManager collisionObjHealth))
            collisionObjHealth.TakeDamage(false, damage);
    }
}
