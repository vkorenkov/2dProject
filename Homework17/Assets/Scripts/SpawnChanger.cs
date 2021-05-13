using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChanger : MonoBehaviour
{
    [SerializeField] Transform MainCharacterPosition;
    [SerializeField] List<Transform> spawns;
    [SerializeField] List<CinemachineVirtualCamera> cameras;
    public static bool canChangeCamera;

    KeyCode keyCode;

    private void Update()
    {
        if (spawns.Count > 0)
        {
            switch (keyCode)
            {
                case KeyCode.Alpha1:
                    ChangePosition(0);
                    break;
                case KeyCode.Alpha2:
                    ChangePosition(1);
                    break;
                case KeyCode.Alpha3:
                    ChangePosition(2);
                    break;
                case KeyCode.Alpha4:
                    ChangePosition(3);
                    break;
                case KeyCode.Alpha5:
                    ChangePosition(4);
                    break;
            }

            keyCode = default;
        }
    }

    public void ChangePosition(int spawnNumber)
    {
        if (canChangeCamera)
        {
            MainCharacterPosition.position = new Vector2(spawns[spawnNumber].position.x, spawns[spawnNumber].position.y);
            ChangeCameraPriority();
            cameras[spawnNumber].Priority = 10;
        }
    }

    void OnGUI()
    {
        Event key = Event.current;

        if(key.isKey)
        {
            keyCode = key.keyCode;
        }
    }

    private void ChangeCameraPriority()
    {
        foreach(var c in cameras)
        {
            c.Priority = 1;
        }
    }
}
