using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChanger : MonoBehaviour
{
    [SerializeField] Transform MainCharacterPosition;
    [SerializeField] List<Transform> spawns;
    [SerializeField] List<CinemachineVirtualCamera> cameras;
    CameraChanger cameraChanger;

    KeyCode keyCode;

    private void Awake()
    {
        cameraChanger = Camera.main.GetComponent<CameraChanger>();
    }

    private void Update()
    {
        if (spawns.Count > 0)
        {
            switch (keyCode)
            {
                case KeyCode.Alpha1:
                    ChangePosition(0, 0);
                    InputCharacter.currentLevel = 0;
                    break;
                case KeyCode.Alpha2:
                    ChangePosition(1, 0);
                    InputCharacter.currentLevel = 0;
                    break;
                case KeyCode.Alpha3:
                    ChangePosition(2, 1);
                    InputCharacter.currentLevel = 1;
                    break;
                case KeyCode.Alpha4:
                    ChangePosition(3, 1);
                    InputCharacter.currentLevel = 1;
                    break;
                case KeyCode.Alpha5:
                    ChangePosition(4, 2);
                    InputCharacter.currentLevel = 2;
                    break;
                case KeyCode.Alpha6:
                    ChangePosition(5, 2);
                    InputCharacter.currentLevel = 2;
                    break;
                case KeyCode.Alpha7:
                    ChangePosition(6, 3);
                    InputCharacter.currentLevel = 3;
                    break;
                case KeyCode.Alpha8:
                    ChangePosition(7, 3);
                    InputCharacter.currentLevel = 3;
                    break;
                case KeyCode.Alpha9:
                    ChangePosition(8, 4);
                    InputCharacter.currentLevel = 4;
                    break;
                case KeyCode.Alpha0:
                    ChangePosition(9, 4);
                    InputCharacter.currentLevel = 4;
                    break;
            }

            keyCode = default;
        }
    }

    public void ChangePosition(int spawnNumber, int cameraNumber)
    {
        MainCharacterPosition.position = spawns[spawnNumber].position;
        cameraChanger.cameras[cameraNumber].Priority = cameraChanger.currentCamera.Priority + 1;
        cameraChanger.currentCamera = cameraChanger.cameras[cameraNumber];

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
