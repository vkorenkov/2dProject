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
    Output output;

    private void Awake()
    {
        isAlive = true;
        output = GetComponent<Output>();
        currentHealth = maxHealth;
        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (output)
        {
            currentHealth = currentHealth >= 0 ? currentHealth : 0;

            output.OutputHealthCount($"{currentHealth}");
        }
    }

    public void TakeDamage(bool totaldamage, float damage = 0)
    {
        if (totaldamage)
            currentHealth -= currentHealth;

        currentHealth -= damage;

        isAlive = CheckCharacterHealth();

        if (!isAlive)
        {
            ControlEnableEvent?.Invoke(isAlive);

            DeactivatedAnimations();

            characterAnimator.SetBool("Dead", !isAlive);

            GetComponent<Collider2D>().enabled = isAlive;

            Destroy(gameObject, destroyTime);
        }
    }

    void DeactivatedAnimations()
    {
        foreach (var a in characterAnimator.parameters)
        {
            characterAnimator.SetBool(a.name, false);
        }
    }

    bool CheckCharacterHealth()
    {
        if (output)
            output.ChangeTextColor(output.healthCount, CheckHealthColor());

        return currentHealth > 0;
    }

    Color CheckHealthColor()
    {
        Color color = new Color();

        if (currentHealth > 75) color = Color.green;
        if (currentHealth < 75) color = Color.yellow;
        if (currentHealth < 50) color = new Color(205, 87, 0);
        if (currentHealth < 25) color = Color.red;

        return color;
    }
}
