using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    /// <summary>
    /// ��������� ���� �������� �����
    /// </summary>
    List<string> phrases = new List<string>()
    {
        "Well. I'm here.",
        "How did this stupid innkeeper manage to f@#% up the beer?",
        "Okay! An order is an order. Let's go find that damn beer."
    };

    /// <summary>
    /// ������ ������ ���������� �� �����
    /// </summary>
    Output output;

    /// <summary>
    /// �������� ����������
    /// </summary>
    [SerializeField] Animation blackoutAnimation;

    #region ������������ ���� �������� �� ����� ����� � �������
    //InputCharacter inputCharacter;

    //[SerializeField] SpawnChanger spawnChanger;
    #endregion

    /// <summary>
    /// ����� ����������� ����� ���������
    /// </summary>
    [SerializeField] float displayTime;

    /// <summary>
    /// ������� �����
    /// </summary>
    int phrasQueae;

    private void Awake()
    {
        output = GameObject.Find("Hud").GetComponent<Output>();
        StartCoroutine(FirstWait());

        #region ������������ ���� �������� �� ����� ����� � �������
        //inputCharacter = GameObject.Find("MainCharacter").GetComponent<InputCharacter>();
        //inputCharacter.ControlEnableChange(false);
        #endregion
    }

    /// <summary>
    /// �������� ������ �����
    /// </summary>
    /// <returns></returns>
    IEnumerator FirstWait()
    {
        yield return new WaitForSeconds(1);

        StartDialog();
        StartCoroutine(DialogCor());
    }

    /// <summary>
    /// ��������� ���������������� �����
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

        // ������ �������� �������� �� �������� �� ������ �������
        blackoutAnimation[blackoutAnimation.clip.name].time = blackoutAnimation[blackoutAnimation.clip.name].length;
        blackoutAnimation[blackoutAnimation.clip.name].speed *= -1;
        blackoutAnimation.Play();

        yield return new WaitForSeconds(1);

        // �������� ����� ������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        #region ������������ ���� �������� �� ����� ����� � �������
        //inputCharacter.ControlEnableChange(true);

        //spawnChanger.ChangePosition(0, 0);
        #endregion
    }

    /// <summary>
    /// ��������� ����������� ����� ���������
    /// </summary>
    void StartDialog()
    {
        output.timer = displayTime;
        output.goTimer = true;
        output.OutputDialog(phrases[phrasQueae]);
        phrasQueae += 1;
    }
}
