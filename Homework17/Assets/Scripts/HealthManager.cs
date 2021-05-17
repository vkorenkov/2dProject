using Cinemachine;
using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    /// <summary>
    /// Делегат передачи данных о состоянии персонажа
    /// </summary>
    /// <param name="isAlive"></param>
    public delegate void ControlDel(bool isAlive);
    /// <summary>
    /// Событие изменения доступности управления
    /// </summary>
    public event ControlDel ControlEnableEvent;
    public event ControlDel DeathEvent;
    /// <summary>
    /// Максимальное количество здоровья персонажа
    /// </summary>
    [SerializeField] float maxHealth;
    /// <summary>
    /// Текущее значение здоровья персонажа
    /// </summary>
    public float currentHealth;
    /// <summary>
    /// Состояние персонажа
    /// </summary>
    bool isAlive;
    /// <summary>
    /// Компонент аниматор персонажа
    /// </summary>
    [SerializeField] Animator characterAnimator;

    [SerializeField] float damageCoolDown;

    [SerializeField, Header("Character Destroy Time")] float destroyTime;
    /// <summary>
    /// Поле вывода информации на экран
    /// </summary>
    [SerializeField] Output output;

    [SerializeField] bool godMode;

    bool isDamaged;

    private void Awake()
    {
        isAlive = true; // Назначение состояния персонажа
        currentHealth = maxHealth; // Установка значения текущего здоровья
    }

    private void Update()
    {
        if (output)
        {
            currentHealth = currentHealth >= 0 ? currentHealth : 0; // Если уровень здоровья упадет ниже нуля, то будет выводится "0"

            output.OutputHealthCount($"{currentHealth}"); // Вызов метода вывода здоровья персонажа
        }
    }

    /// <summary>
    /// Метод регистрации урона
    /// </summary>
    /// <param name="totaldamage"></param>
    /// <param name="damage"></param>
    public void TakeDamage(bool totaldamage, float damage = 0)
    {
        if (isDamaged)
            return;

        if (godMode)
            return;

        // Условие при котором отнимается все доступное здоровье персонажа
        if (totaldamage)
        {
            currentHealth -= currentHealth;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Вычитание здоровья персонажа в соответствии с уроном
        currentHealth -= damage;

        isDamaged = true;

        // получение состояния персонажа
        isAlive = CheckCharacterHealth();

        // Действия при "смерти" персонажа
        if (!isAlive)
        {
            if (isPlayer)
            {
                DeathEvent?.Invoke(isAlive); // Вызов события изменения доступности управления в соответствии с состоянием персонажа
                ControlEnableEvent.Invoke(isAlive);
                Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            }

            // Отключение всех анимаций
            DeactivatedAnimations();

            // Вызов анимации "смерти" персонажа
            characterAnimator.SetTrigger("DeathT");

            // Изменение активности коллайдера в зависимости от состояния персонажа
            foreach(var c in GetComponents<Collider2D>())
            {
                c.enabled = isAlive;
            }

            // Уничтожение объекта персонажа
            Destroy(gameObject, destroyTime);
        }
        else
            StartCoroutine(DamageCoolDown());
    }

    IEnumerator DamageCoolDown()
    {
        characterAnimator.SetTrigger("HurtT");

        if (isPlayer) ControlEnableEvent.Invoke(false);

        yield return new WaitForSeconds(damageCoolDown);

        if (isPlayer) ControlEnableEvent.Invoke(true);

        isDamaged = false;

        StopCoroutine(DamageCoolDown());
    }

    /// <summary>
    /// Метод отключения всех анимаций
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
    /// Метод проверки состояния персонажа
    /// </summary>
    /// <returns></returns>
    bool CheckCharacterHealth()
    {
        // Изменение цвета вывода здоровья персонажа
        if (output)
            output.ChangeTextColor(output.healthCount, CheckHealthColor());

        return currentHealth > 0;
    }

    /// <summary>
    /// Проверка значения здоровья для назначения цвета текста
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
