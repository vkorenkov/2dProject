using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public delegate void ControlDel(bool isAlive);
    public event ControlDel ControlEnableEvent;

    [SerializeField] float maxHealth;
    float currentHealth;
    bool isAlive;
    Animator characterAnimator;
    [SerializeField] float destroyTime;

    private void Awake()
    {
        isAlive = true;
        currentHealth = maxHealth;
        characterAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(bool totaldamage, float damage = 0)
    {
        if (totaldamage)
            currentHealth -= currentHealth;

        currentHealth -= damage;
        Debug.Log($"Здоровья осталось {currentHealth}");
        isAlive = CheckCharacterHealth();

        if (!isAlive)
        {
            characterAnimator.SetBool("Dead", !isAlive);

            GetComponent<Collider2D>().enabled = isAlive;

            ControlEnableEvent?.Invoke(isAlive);

            Destroy(gameObject, destroyTime);
        }
    }

    bool CheckCharacterHealth()
    {
        return currentHealth <= 0 ? false : true;
    }
}
