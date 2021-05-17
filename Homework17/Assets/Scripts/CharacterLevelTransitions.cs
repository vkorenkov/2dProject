using System.Collections;
using System.Linq;
using UnityEngine;

public class CharacterLevelTransitions : MonoBehaviour
{
    [SerializeField] bool isBack;
    [SerializeField] Transform MainCharacterPosition;
    [SerializeField] CameraChanger cameraChanger;

    private void Awake()
    {       
        GameObject.Find("MainCharacter").GetComponent<HealthManager>().DeathEvent += CharacterLevelTransitions_DeathEvent;
    }

    private void CharacterLevelTransitions_DeathEvent(bool isAlive)
    {
        cameraChanger.cameras.FirstOrDefault().Priority = cameraChanger.currentCamera.Priority + 1;
        cameraChanger.currentCamera = cameraChanger.cameras.FirstOrDefault();
        InputCharacter.currentLevel = 0;
    }

    public void ChangeCamera(int spawnNumber)
    {
        cameraChanger.cameras[spawnNumber].Priority = cameraChanger.currentCamera.Priority + 1;
        cameraChanger.currentCamera = cameraChanger.cameras[spawnNumber];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (isBack)
            {
                InputCharacter.currentLevel -= 1;
            }
            else
            {
                InputCharacter.currentLevel += 1;
            }

            ChangeCamera(InputCharacter.currentLevel);

            StartCoroutine(ChangeCharacterPositionCoroutine());
        }
    }

    IEnumerator ChangeCharacterPositionCoroutine()
    {
        yield return new WaitForSeconds(.1f);

        MainCharacterPosition.position = !isBack ? new Vector2(InputCharacter.leftCameraLine.x, MainCharacterPosition.position.y) : 
            new Vector2(InputCharacter.rightCameraLine.x, MainCharacterPosition.position.y);

        StopCoroutine(ChangeCharacterPositionCoroutine());
    }
}
