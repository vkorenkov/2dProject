using System.Collections.Generic;
using UnityEngine;

public class CollectObjects : MonoBehaviour
{
    /// <summary>
    /// ������ ������ ���������� �� �����
    /// </summary>
    [SerializeField] Output output;

    /// <summary>
    ///  ��������, ��� ����� ��������� ��� ����������� ���������� ������ ������
    /// </summary>
    public int killedEnemies;
    /// <summary>
    ///  ��������, ��� ����� ��������� ��� ����������� ���������� ��������� ���������
    /// </summary>
    public int collectedObjectsCount;

    /// <summary>
    /// ��������� �������� ����� ������
    /// </summary>
    public static Dictionary<int, string> hints = new Dictionary<int, string>()
    {
        [0] = "Maybe somewhere in a cave?",
        [1] = "Was exactly meant to be on the way here.",
        [2] = "Maybe look at the bottom?",
        [3] = "There is no beer here :(",
        [4] = "Your advertisement could be here."
    };

    private void Update()
    {
        output.OutputBonusCount($"{collectedObjectsCount}");
        output.OutputKillsCount($"{killedEnemies}");
    }
}