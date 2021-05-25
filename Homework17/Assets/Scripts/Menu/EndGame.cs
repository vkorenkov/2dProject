using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    /// <summary>
    /// точка появления объекта
    /// </summary>
    [SerializeField] GameObject spawnPoint;
    /// <summary>
    /// Объект для создания
    /// </summary>
    [SerializeField] GameObject spawnObject;
    /// <summary>
    /// Объект сбора предметов
    /// </summary>
    CollectObjects collectedObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Отключение управления
            collision.GetComponent<InputCharacter>().ControlEnableChange(false);
            // Получение объекта собранных предметов
            collectedObjects = collision.gameObject.GetComponent<CollectObjects>();
            StartCoroutine(EndCoroutine(collectedObjects.collectedObjectsCount));
        }
    }

    /// <summary>
    /// Запуск создания предметов
    /// </summary>
    /// <param name="beerCount"></param>
    /// <returns></returns>
    IEnumerator EndCoroutine(int beerCount)
    {
        for (int i = 0; i < beerCount; i++)
        {
            yield return new WaitForSeconds(0.5f);

            collectedObjects.collectedObjectsCount -= 1;

            // Случайная точка создания предмета по горизонтали
            var randomX = Random.Range(GetComponent<BoxCollider2D>().bounds.min.x + 0.5f, GetComponent<BoxCollider2D>().bounds.max.x);

            Instantiate(spawnObject, new Vector2(randomX, spawnPoint.transform.position.y), Quaternion.identity);
        }

        var canvas = GameObject.Find("StartCanvas").GetComponent<Animation>();

        canvas[canvas.clip.name].time = canvas[canvas.clip.name].length;
        canvas[canvas.clip.name].speed *= -1;
        canvas.Play();

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("MenuScene");
    }
}
