using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChanger : MonoBehaviour
{
    [SerializeField] Transform MainCharacterPosition;
    [SerializeField] public List<Transform> spawns;

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
            }

            keyCode = default;
        }
    }

    public void ChangePosition(int spawnNumber)
    {
        MainCharacterPosition.position = spawns[spawnNumber].position;
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
