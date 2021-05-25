using UnityEngine;

public class MenuCharactermove : MonoBehaviour
{
    /// <summary>
    /// RigitBody ГГ
    /// </summary>
    [SerializeField] Rigidbody2D characterRb;

    private void Start()
    {
        GetComponentInChildren<Animator>().SetBool("Run", true); // Запуск анимации бега
    }
}
