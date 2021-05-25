using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    /// <summary>
    /// Коллекция фраз главного героя
    /// </summary>
    List<string> phrases = new List<string>()
    {
        "Well. I'm here.",
        "How did this stupid innkeeper manage to f@#% up the beer?",
        "Okay! An order is an order. Let's go find that damn beer."
    };

    /// <summary>
    /// Объект вывода информации на эеран
    /// </summary>
    Output output;

    /// <summary>
    /// Анимация затемнения
    /// </summary>
    [SerializeField] Animation blackoutAnimation;

    #region Использовать если катсцена на одной сцене с уровнем
    //InputCharacter inputCharacter;

    //[SerializeField] SpawnChanger spawnChanger;
    #endregion

    /// <summary>
    /// Время отображения фразы персонажа
    /// </summary>
    [SerializeField] float displayTime;

    /// <summary>
    /// очередь фразы
    /// </summary>
    int phrasQueae;

    private void Awake()
    {
        output = GameObject.Find("Hud").GetComponent<Output>();
        StartCoroutine(FirstWait());

        #region Использовать если катсцена на одной сцене с уровнем
        //inputCharacter = GameObject.Find("MainCharacter").GetComponent<InputCharacter>();
        //inputCharacter.ControlEnableChange(false);
        #endregion
    }

    /// <summary>
    /// Ожидание первой фразы
    /// </summary>
    /// <returns></returns>
    IEnumerator FirstWait()
    {
        yield return new WaitForSeconds(1);

        StartDialog();
        StartCoroutine(DialogCor());
    }

    /// <summary>
    /// Запускает последовательные фразы
    /// </summary>
    /// <returns></returns>
    IEnumerator DialogCor()
    {
        while (phrasQueae < phrases.Count)
        {
            yield return new WaitForSeconds(displayTime);

            StartDialog();

            if (phrasQueae == phrases.Count)
                yield return new WaitForSeconds(displayTime);
        }

        yield return new WaitForSeconds(displayTime / 2);

        // Запуск анимации перехода из катсцены на первый уровень
        blackoutAnimation[blackoutAnimation.clip.name].time = blackoutAnimation[blackoutAnimation.clip.name].length;
        blackoutAnimation[blackoutAnimation.clip.name].speed *= -1;
        blackoutAnimation.Play();

        yield return new WaitForSeconds(1);

        // Загрузка сцены уровня
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        #region Использовать если катсцена на одной сцене с уровнем
        //inputCharacter.ControlEnableChange(true);

        //spawnChanger.ChangePosition(0, 0);
        #endregion
    }

    /// <summary>
    /// Запускает отображение фразы персонажа
    /// </summary>
    void StartDialog()
    {
        output.timer = displayTime;
        output.goTimer = true;
        output.OutputDialog(phrases[phrasQueae]);
        phrasQueae += 1;
    }
}
