using Cinemachine;
using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    /// <summary>
    /// ������� �������� ������ � ��������� ���������
    /// </summary>
    /// <param name="isAlive"></param>
    public delegate void ControlDel(bool isAlive);
    /// <summary>
    /// ������� ��������� ����������� ����������
    /// </summary>
    public event ControlDel ControlEnableEvent;
    public event ControlDel DeathEvent;

    [SerializeField] int livesCount;

    [SerializeField] bool godMode;
    /// <summary>
    /// ������������ ���������� �������� ���������
    /// </summary>
    [SerializeField] float maxHealth;
    /// <summary>
    /// ������� �������� �������� ���������
    /// </summary>
    public float currentHealth;
    /// <summary>
    /// ��������� ���������
    /// </summary>
    public bool isAlive;
    /// <summary>
    /// ��������� �������� ���������
    /// </summary>
    [SerializeField] Animator characterAnimator;

    [SerializeField] float damageCoolDown;

    [SerializeField, Header("Character Destroy Time")] float destroyTime;
    /// <summary>
    /// ���� ������ ���������� �� �����
    /// </summary>
    [SerializeField] Output output;

    bool isDamaged;

    private void Awake()
    {
        isAlive = true; // ���������� ��������� ���������
        currentHealth = maxHealth; // ��������� �������� �������� ��������
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
        if (isDamaged)
            return;

        if (godMode)
            return;

        // ������� ��� ������� ���������� ��� ��������� �������� ���������
        if (totaldamage)
        {
            currentHealth -= currentHealth;
            if (livesCount == 0) GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // ��������� �������� ��������� � ������������ � ������
        currentHealth -= damage;

        if (isPlayer) isDamaged = true;

        // ��������� ��������� ���������
        isAlive = CheckCharacterHealth();

        // �������� ��� "������" ���������
        if (!isAlive)
        {
            // ���������� ���� ��������
            DeactivatedAnimations();

            // ����� �������� "������" ���������
            characterAnimator.SetTrigger("DeathT");

            if (isPlayer)
            {
                DeathEvent?.Invoke(isAlive); // ����� ������� ��������� ����������� ���������� � ������������ � ���������� ���������
                ControlEnableEvent?.Invoke(isAlive);
                Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            }

            // ��������� ���������� ���������� � ����������� �� ��������� ���������
            foreach (var c in GetComponents<Collider2D>())
                c.enabled = isAlive;

            // ����������� ������� ���������
            Destroy(gameObject, destroyTime);
        }
        else
        {
            StartCoroutine(DamageCoolDown());
        }
    }

    IEnumerator DamageCoolDown()
    {
        characterAnimator.SetTrigger("HurtT");

        ControlEnableEvent.Invoke(false);

        yield return new WaitForSeconds(damageCoolDown);

        ControlEnableEvent.Invoke(true);

        if (isPlayer) isDamaged = false;

        StopCoroutine(DamageCoolDown());
    }


    /// <summary>
    /// ����� ���������� ���� ��������
    /// </summary>
    void DeactivatedAnimations()
    {
        foreach (var a in characterAnimator.parameters)
        {
            if (a.type == AnimatorControllerParameterType.Float)
                characterAnimator.SetFloat(a.name, 0);
            if (a.type == AnimatorControllerParameterType.Int)
                characterAnimator.SetFloat(a.name, 0);
            if (a.type == AnimatorControllerParameterType.Bool)
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
