using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    [SerializeField] public TextMeshPro healthCount;

    Quaternion RotationY
    {
        get => new Quaternion(healthCount.transform.rotation.x, 0, healthCount.transform.rotation.z, healthCount.transform.rotation.w);
    }

    private void Update()
    {
        healthCount.transform.rotation = transform.rotation.y < 0 ?
            RotationY : new Quaternion();
    }

    public void OutputHealthCount(string count)
    {
        healthCount.text = count;
    }

    public void ChangeTextColor(TextMeshPro textObj, Color color)
    {
        textObj.color = color;
    }
}
