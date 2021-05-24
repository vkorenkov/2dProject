using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    [HideInInspector] public AnimationActivator animationActivator = new AnimationActivator();

    [SerializeField] Transform hudPosition;
    /// <summary>
    /// ����� ������ ��������
    /// </summary>
    [SerializeField] public TextMeshPro healthCount;
    /// <summary>
    /// ����� ������ ��������� ��������
    /// </summary>
    [SerializeField] private TextMeshPro projectileCount;
    [SerializeField] private TextMeshPro killedCount;
    [SerializeField] private TextMeshPro bonusCount;
    [SerializeField] public TextMeshPro dialogText;
    [HideInInspector] public float timer;
    [HideInInspector] public bool goTimer;

    private void Update()
    {
        if (hudPosition) transform.position = hudPosition.position;

        // ��������� � ������� ������� � ������������� �� �������� ���������
        if (healthCount) healthCount.transform.rotation = TextRotation();
        if (projectileCount) projectileCount.transform.rotation = TextRotation();
        if (bonusCount) bonusCount.transform.rotation = TextRotation();

        if (goTimer)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                goTimer = false;
                animationActivator.AnimationPlayback(dialogText, false);
            }
        }
    }

    Quaternion TextRotation()
    {
        return transform.rotation.y < 0 || transform.rotation.y > 0 ? Quaternion.identity : new Quaternion();
    }

    /// <summary>
    /// ����� ������ �������� �� �����
    /// </summary>
    /// <param name="count"></param>
    public void OutputHealthCount(string count)
    {
        healthCount.text = $"{count} %";
        //healthCount.text = count;
    }

    /// <summary>
    /// ����� ������ �������� �� �����
    /// </summary>
    /// <param name="count"></param>
    public void OutputProjectilesCount(string count)
    {
        projectileCount.text = count;
    }

    public void OutputBonusCount(string count)
    {
        bonusCount.text = count;
    }

    public void OutputKillsCount(string count)
    {
        if(killedCount) killedCount.text = count;
    }

    public void OutputDialog(string dialog)
    {
        dialogText.text = dialog;
        animationActivator.AnimationPlayback(dialogText, true);
    }

    /// <summary>
    /// ����� ��������� ����� ������
    /// </summary>
    /// <param name="textObj"></param>
    /// <param name="color"></param>
    public void ChangeTextColor(TextMeshPro textObj, Color color)
    {
        textObj.color = color;
    }
}
