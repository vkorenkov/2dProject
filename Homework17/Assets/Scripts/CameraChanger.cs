using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras;
    public CinemachineVirtualCamera currentCamera;

    private void Awake()
    {
        currentCamera = cameras.FirstOrDefault();
    }
}
