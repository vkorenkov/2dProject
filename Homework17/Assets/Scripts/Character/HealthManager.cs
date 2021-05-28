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

    [SerializeField] bool godMode;

    [SerializeField] public PlayerPrefs playerPrefs;
    /// <summary>
    /// Максимальное количество здоровья персонажа
    /// </summary>
    public float maxHealth;
    /// <summary>
    /// Текущее значение здоровья персонажа
    /// </summary>
    public float currentHealth;

    float CurrentHealthPercent
    {
        get
        {
            if (playerPrefs)
                return Mathf.Round(playerPrefs.CurrentHealth / playerPrefs.MaxHealth * 100);
            else
                return Mathf.Round(currentHealth / maxHealth * 100);
        }
    }
    /// <summary>
    /// Состояние персонажа
    /// </summary>
    public bool isAlive;
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
    Color textColor;

    bool isDamaged;

    private void Awake()
    {
        isAlive = true; // Назначение состояния персонажа
        if (!playerPrefs) currentHealth = maxHealth; // Установка значения текущего здоровья
    }

    private void Update()
    {
        if (output)
        {
            if (playerPrefs)
                playerPrefs.CurrentHealth = playerPrefs.CurrentHealth >= 0 ? playerPrefs.CurrentHealth : 0; // Если уровень здоровья упадет ниже нуля, то будет выводится "0"
            else
                currentHealth = currentHealth >= 0 ? currentHealth : 0;

            textColor = CheckHealthColor(); // Установка цвета текта здоровья

            output.ChangeTextColor(output.healthCount, textColor);

            output.OutputHealthCount($"{CurrentHealthPercent}"); // Вызов метода вывода здоровья персонажа в процентах
            //output.OutputHealthCount($"{currentHealth}"); // Вызов метода вывода здоровья персонажа в очках
        }
    }

    /// <summary>
    /// Метод регистрации урона
    /// </summary>
    /// <param name="totaldamage"></param>
    /// <param name="damage"></param>
    public void TakeDamage(bool totaldamage, float damage = 0)
    {
        // Условие при котором отнимается все доступное здоровье персонажа
        if (totaldamage)
        {
            isDamaged = false;
            if (playerPrefs) playerPrefs.CurrentHealth -= playerPrefs.CurrentHealth;
            else currentHealth -= currentHealth;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (isDamaged)
            return;

        if (godMode)
            return;

        // Вычитание здоровья персонажа в соответствии с уроном
        if (playerPrefs) playerPrefs.CurrentHealth -= damage;
        else currentHealth -= damage;

        if (isPlayer) isDamaged = true;

        // получение состояния персонажа
        isAlive = CheckCharacterHealth();

        // Действия при "смерти" персонажа
        if (!isAlive)
        {
            // Отключение всех анимаций
            DeactivatedAnimations();

            // Вызов анимации "смерти" персонажа
            characterAnimator.SetTrigger("DeathT");

            if (isPlayer)
            {
                DeathEvent?.Invoke(isAlive);         // Вызов события изменения состояния персонажа
                ControlEnableEvent?.Invoke(isAlive); // Вызов события изменения доступности управления в соответствии с состоянием персонажа
                Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            }

            // Изменение активности коллайдера в зависимости от состояния персонажа
            foreach (var c in GetComponents<Collider2D>())
                c.enabled = isAlive;

            // Уничтожение объекта персонажа
            Destroy(gameObject, destroyTime);
        }
        else
        {
            StartCoroutine(DamageCoolDown());
        }
    }

    /// <summary>
    /// Отключает получение урона, если урон был ранее получен
    /// </summary>
    /// <returns></returns>
    IEnumerator DamageCoolDown()
    {
        characterAnimator.SetTrigger("HurtT"); // Запуск анимации получения урона

        ControlEnableEvent.Invoke(false); // Оключение управления

        yield return new WaitForSeconds(damageCoolDown);

        ControlEnableEvent.Invoke(true);// Включение управления

        if (isPlayer) isDamaged = false;

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
        if (playerPrefs)
            return playerPrefs.CurrentHealth > 0;
        else
            return currentHealth > 0;
    }

    /// <summary>
    /// Проверка значения здоровья для назначения цвета текста
    /// </summary>
    /// <returns></returns>
    Color CheckHealthColor()
    {
        if (playerPrefs)
            return new Color((playerPrefs.MaxHealth - playerPrefs.CurrentHealth) / 100 + 0.5f, CurrentHealthPercent / 100, 0);
        else
            return new Color((maxHealth - currentHealth) / 100 + 0.5f, CurrentHealthPercent / 100, 0);
    }
}
