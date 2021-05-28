using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnChanger : MonoBehaviour
{
    KeyCode keyCode;

    private void Update()
    {
        switch (keyCode)
        {
            case KeyCode.Alpha1:
                SceneManager.LoadScene("L1");
                break;
            case KeyCode.Alpha2:
                SceneManager.LoadScene("L2");
                break;
            case KeyCode.Alpha3:
                SceneManager.LoadScene("L3");
                break;
            case KeyCode.Alpha4:
                SceneManager.LoadScene("L4");
                break;
            case KeyCode.Alpha5:
                SceneManager.LoadScene("L5");
                break;
        }

        keyCode = default;
    }

    void OnGUI()
    {
        Event key = Event.current;

        if (key.isKey)
        {
            keyCode = key.keyCode;
        }
    }
}
