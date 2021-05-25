using System.Collections;
using System.Linq;
using UnityEngine;

public class CharacterLevelTransitions : MonoBehaviour
{
    /// <summary>
    /// Обазначение левой стороны эерана
    /// </summary>
    [SerializeField] bool isBack;
    /// <summary>
    /// Позиция главного персонажа
    /// </summary>
    [SerializeField] Transform MainCharacterPosition; 
    /// <summary>
    /// Объект изменения камеры
    /// </summary>
    [SerializeField] CameraChanger cameraChanger;

    private void Awake()
    {       
        // Подписка на событие смерти игрока
        GameObject.Find("MainCharacter").GetComponent<HealthManager>().DeathEvent += CharacterLevelTransitions_DeathEvent;
    }

    /// <summary>
    /// Обработчик события смерти игрока
    /// </summary>
    /// <param name="isAlive"></param>
    private void CharacterLevelTransitions_DeathEvent(bool isAlive)
    {
        // Установка приоритета первой камеры
        cameraChanger.cameras.FirstOrDefault().Priority = cameraChanger.currentCamera.Priority + 1;
        // Установка текущей камеры
        cameraChanger.currentCamera = cameraChanger.cameras.FirstOrDefault();
        InputCharacter.currentLevel = 0;
    }

    /// <summary>
    /// Изменяет текущую камеру
    /// </summary>
    /// <param name="spawnNumber"></param>
    public void ChangeCamera(int cameraNumber)
    {       
        cameraChanger.cameras[cameraNumber].Priority = cameraChanger.currentCamera.Priority + 1; // Установка приоритета камеры
        cameraChanger.currentCamera = cameraChanger.cameras[cameraNumber];  // Установка текущей камеры
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Установка текущего уровня
            if (isBack)
                InputCharacter.currentLevel -= 1; 
            else
                InputCharacter.currentLevel += 1;

            ChangeCamera(InputCharacter.currentLevel);

            StartCoroutine(ChangeCharacterPositionCoroutine());
        }
    }

    /// <summary>
    /// Меняет позицию игрока в зависимости от того с какой стороны игрок прошел границу камеры
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeCharacterPositionCoroutine()
    {
        yield return new WaitForSeconds(.1f);

        MainCharacterPosition.position = !isBack ? new Vector2(InputCharacter.leftCameraLine.x, MainCharacterPosition.position.y) :
            new Vector2(InputCharacter.rightCameraLine.x, MainCharacterPosition.position.y);
    }
}
