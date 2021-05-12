using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChanger : MonoBehaviour
{
    [SerializeField] Transform MainCharacterPosition;
    [SerializeField] List<Transform> spawns;
    [SerializeField] List<CinemachineVirtualCamera> cameras;

    KeyCode keyCode;

    private void Update()
    {
        if (spawns.Count > 0)
        {
            switch (keyCode)
            {
                case KeyCode.Alpha1:
                    MainCharacterPosition.position = ChangePosition(0);
                    ChangeCameraPriority();
                    cameras[0].Priority = 10;
                    break;
                case KeyCode.Alpha2:
                    MainCharacterPosition.position = ChangePosition(1);
                    ChangeCameraPriority();
                    cameras[1].Priority = 10;
                    break;
                case KeyCode.Alpha3:
                    MainCharacterPosition.position = ChangePosition(2);
                    ChangeCameraPriority();
                    cameras[2].Priority = 10;
                    break;
                case KeyCode.Alpha4:
                    MainCharacterPosition.position = ChangePosition(3);
                    ChangeCameraPriority();
                    cameras[3].Priority = 10;
                    break;
                case KeyCode.Alpha5:
                    MainCharacterPosition.position = ChangePosition(4);
                    ChangeCameraPriority();
                    cameras[4].Priority = 10;
                    break;
            }

            keyCode = default;
        }
    }

    Vector2 ChangePosition(int spawnNumber)
    {
        return new Vector2(spawns[spawnNumber].position.x, spawns[spawnNumber].position.y);
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
