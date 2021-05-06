using UnityEngine;

public class HealthManager : MonoBehaviour
{
    /// <summary>
    /// ������� �������� ������ � ��������� ���������
    /// </summary>
    /// <param name="isAlive"></param>
    public delegate void ControlDel(bool isAlive);
    /// <summary>
    /// ������� ��������� ����������� ����������
    /// </summary>
    public event ControlDel ControlEnableEvent;
    /// <summary>
    /// ������������ ���������� �������� ���������
    /// </summary>
    [SerializeField] float maxHealth;
    /// <summary>
    /// ������� �������� �������� ���������
    /// </summary>
    float currentHealth;
    /// <summary>
    /// ��������� ���������
    /// </summary>
    bool isAlive;
    /// <summary>
    /// ��������� �������� ���������
    /// </summary>
    Animator characterAnimator;
    [SerializeField, Header("Character Destroy Time")] float destroyTime;
    /// <summary>
    /// ���� ������ ���������� �� �����
    /// </summary>
    [SerializeField] Output output;

    [SerializeField] bool godMode;

    private void Awake()
    {
        isAlive = true; // ���������� ��������� ���������
        //output = GetComponent<Output>(); // ��������� ���������� ������
        currentHealth = maxHealth; // ��������� �������� �������� ��������
        characterAnimator = GetComponent<Animator>(); // ��������� �������� ���������
    }

    private void Update()
    {
        if (output)
        {
            currentHealth = currentHealth >= 0 ? currentHealth : 0; // ���� ������� �������� ������ ���� ����, �� ����� ��������� "0"

            output.OutputHealthCount($"{currentHealth}"); // ����� ������ ������ �������� ���������
        }
    }

    /// <summary>
    /// ����� ����������� �����
    /// </summary>
    /// <param name="totaldamage"></param>
    /// <param name="damage"></param>
    public void TakeDamage(bool totaldamage, float damage = 0)
    {
        if (godMode)
            return;

        // ������� ��� ������� ���������� ��� ��������� �������� ���������
        if (totaldamage)
            currentHealth -= currentHealth;

        // ��������� �������� ��������� � ������������ � ������
        currentHealth -= damage;

        // ��������� ��������� ���������
        isAlive = CheckCharacterHealth();

        // �������� ��� "������" ���������
        if (!isAlive)
        {
            ControlEnableEvent?.Invoke(isAlive); // ����� ������� ��������� ����������� ���������� � ������������ � ���������� ���������

            // ���������� ���� ��������
            DeactivatedAnimations();

            // ����� �������� "������" ���������
            characterAnimator.SetBool("Dead", !isAlive);

            // ��������� ���������� ���������� � ����������� �� ��������� ���������
            GetComponent<Collider2D>().enabled = isAlive;
            // ����������� ������� ���������
            Destroy(gameObject, destroyTime);
        }
    }

    /// <summary>
    /// ����� ���������� ���� ��������
    /// </summary>
    void DeactivatedAnimations()
    {
        foreach (var a in characterAnimator.parameters)
        {
            characterAnimator.SetBool(a.name, false);
        }
    }

    /// <summary>
    /// ����� �������� ��������� ���������
    /// </summary>
    /// <returns></returns>
    bool CheckCharacterHealth()
    {
        // ��������� ����� ������ �������� ���������
        if (output)
            output.ChangeTextColor(output.healthCount, CheckHealthColor());

        return currentHealth > 0;
    }

    /// <summary>
    /// �������� �������� �������� ��� ���������� ����� ������
    /// </summary>
    /// <returns></returns>
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
