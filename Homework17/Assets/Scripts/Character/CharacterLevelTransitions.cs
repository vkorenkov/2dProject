using System.Collections;
using System.Linq;
using UnityEngine;

public class CharacterLevelTransitions : MonoBehaviour
{
    /// <summary>
    /// ����������� ����� ������� ������
    /// </summary>
    [SerializeField] bool isBack;
    /// <summary>
    /// ������� �������� ���������
    /// </summary>
    [SerializeField] Transform MainCharacterPosition; 
    /// <summary>
    /// ������ ��������� ������
    /// </summary>
    [SerializeField] CameraChanger cameraChanger;

    private void Awake()
    {       
        // �������� �� ������� ������ ������
        GameObject.Find("MainCharacter").GetComponent<HealthManager>().DeathEvent += CharacterLevelTransitions_DeathEvent;
    }

    /// <summary>
    /// ���������� ������� ������ ������
    /// </summary>
    /// <param name="isAlive"></param>
    private void CharacterLevelTransitions_DeathEvent(bool isAlive)
    {
        // ��������� ���������� ������ ������
        cameraChanger.cameras.FirstOrDefault().Priority = cameraChanger.currentCamera.Priority + 1;
        // ��������� ������� ������
        cameraChanger.currentCamera = cameraChanger.cameras.FirstOrDefault();
        InputCharacter.currentLevel = 0;
    }

    /// <summary>
    /// �������� ������� ������
    /// </summary>
    /// <param name="spawnNumber"></param>
    public void ChangeCamera(int cameraNumber)
    {       
        cameraChanger.cameras[cameraNumber].Priority = cameraChanger.currentCamera.Priority + 1; // ��������� ���������� ������
        cameraChanger.currentCamera = cameraChanger.cameras[cameraNumber];  // ��������� ������� ������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // ��������� �������� ������
            if (isBack)
                InputCharacter.currentLevel -= 1; 
            else
                InputCharacter.currentLevel += 1;

            ChangeCamera(InputCharacter.currentLevel);

            StartCoroutine(ChangeCharacterPositionCoroutine());
        }
    }

    /// <summary>
    /// ������ ������� ������ � ����������� �� ���� � ����� ������� ����� ������ ������� ������
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeCharacterPositionCoroutine()
    {
        yield return new WaitForSeconds(.1f);

        MainCharacterPosition.position = !isBack ? new Vector2(InputCharacter.leftCameraLine.x, MainCharacterPosition.position.y) :
            new Vector2(InputCharacter.rightCameraLine.x, MainCharacterPosition.position.y);
    }
}
