using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Animation LoseAnimation;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>().ControlEnableEvent += MenuScript_ControlEnableEvent;
    }

    private void MenuScript_ControlEnableEvent(bool isAlive)
    {
        LoseAnimation.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
