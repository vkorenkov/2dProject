using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    /// <summary>
    /// ����� ������ ��������
    /// </summary>
    [SerializeField] public TextMeshPro healthCount;
    /// <summary>
    /// �������� �������� ���������
    /// </summary>
    Quaternion RotationY
    {
        // ���������� ������� ������
        get => new Quaternion(healthCount.transform.rotation.x, 0, healthCount.transform.rotation.z, healthCount.transform.rotation.w);
    }

    private void Update()
    {
        // ��������� � ������� ������� � ������������� �� �������� ���������
        healthCount.transform.rotation = transform.rotation.y < 0 ?
            RotationY : new Quaternion();
    }

    /// <summary>
    /// ����� ������ �������� �� �����
    /// </summary>
    /// <param name="count"></param>
    public void OutputHealthCount(string count)
    {
        healthCount.text = count;
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
