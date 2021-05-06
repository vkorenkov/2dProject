using UnityEngine;

public class HealthManager : MonoBehaviour
{
    /// <summary>
    /// Делегат передачи данных о состоянии персонажа
    /// </summary>
    /// <param name="isAlive"></param>
    public delegate void ControlDel(bool isAlive);
    /// <summary>
    /// Событие изменения доступности управления
    /// </summary>
    public event ControlDel ControlEnableEvent;
    /// <summary>
    /// Максимальное количество здоровья персонажа
    /// </summary>
    [SerializeField] float maxHealth;
    /// <summary>
    /// Текущее значение здоровья персонажа
    /// </summary>
    float currentHealth;
    /// <summary>
    /// Состояние персонажа
    /// </summary>
    bool isAlive;
    /// <summary>
    /// Компонент аниматор персонажа
    /// </summary>
    Animator characterAnimator;
    [SerializeField, Header("Character Destroy Time")] float destroyTime;
    /// <summary>
    /// Поле вывода информации на экран
    /// </summary>
    [SerializeField] Output output;

    [SerializeField] bool godMode;

    private void Awake()
    {
        isAlive = true; // Назначение состояния персонажа
        //output = GetComponent<Output>(); // Получение компонента вывода
        currentHealth = maxHealth; // Установка значения текущего здоровья
        characterAnimator = GetComponent<Animator>(); // Получение анимаций персонажа
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
        if (godMode)
            return;

        // Условие при котором отнимается все доступное здоровье персонажа
        if (totaldamage)
            currentHealth -= currentHealth;

        // Вычитание здоровья персонажа в соответствии с уроном
        currentHealth -= damage;

        // получение состояния персонажа
        isAlive = CheckCharacterHealth();

        // Действия при "смерти" персонажа
        if (!isAlive)
        {
            ControlEnableEvent?.Invoke(isAlive); // Вызов события изменения доступности управления в соответствии с состоянием персонажа

            // Отключение всех анимаций
            DeactivatedAnimations();

            // Вызов анимации "смерти" персонажа
            characterAnimator.SetBool("Dead", !isAlive);

            // Изменение активности коллайдера в зависимости от состояния персонажа
            GetComponent<Collider2D>().enabled = isAlive;
            // Уничтожение объекта персонажа
            Destroy(gameObject, destroyTime);
        }
    }

    /// <summary>
    /// Метод отключения всех анимаций
    /// </summary>
    void DeactivatedAnimations()
    {
        foreach (var a in characterAnimator.parameters)
        {
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
