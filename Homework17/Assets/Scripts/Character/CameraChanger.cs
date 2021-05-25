using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    /// <summary>
    /// Коллекция виртуальных камер
    /// </summary>
    public List<CinemachineVirtualCamera> cameras;
    /// <summary>
    /// Текущая камера
    /// </summary>
    public CinemachineVirtualCamera currentCamera;

    private void Awake()
    {
        currentCamera = cameras.FirstOrDefault();
    }
}
