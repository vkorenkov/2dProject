using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    /// <summary>
    /// ��������� ����������� �����
    /// </summary>
    public List<CinemachineVirtualCamera> cameras;
    /// <summary>
    /// ������� ������
    /// </summary>
    public CinemachineVirtualCamera currentCamera;

    private void Awake()
    {
        currentCamera = cameras.FirstOrDefault();
    }
}
