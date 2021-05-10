using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChanger : MonoBehaviour
{
    [SerializeField] List<Transform> spawns;
    [SerializeField] Transform MainCharacterPosition;
    [SerializeField] List<CinemachineVirtualCamera> cameras;

    KeyCode keyCode;

    private void Update()
    {
        if (spawns.Count > 0)
        {
            switch (keyCode)
            {
                case KeyCode.Alpha1:
                    MainCharacterPosition.position = spawns[0].position;
                    ChangeCameraPriority();
                    cameras[0].Priority = 10;
                    break;
                case KeyCode.Alpha2:
                    MainCharacterPosition.position = spawns[1].position;
                    ChangeCameraPriority();
                    cameras[1].Priority = 10;
                    break;
                case KeyCode.Alpha3:
                    MainCharacterPosition.position = spawns[2].position;
                    ChangeCameraPriority();
                    cameras[2].Priority = 10;
                    break;
                case KeyCode.Alpha4:
                    MainCharacterPosition.position = spawns[3].position;
                    ChangeCameraPriority();
                    cameras[3].Priority = 10;
                    break;
                case KeyCode.Alpha5:
                    MainCharacterPosition.position = spawns[4].position;
                    ChangeCameraPriority();
                    cameras[4].Priority = 10;
                    break;
            }
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
