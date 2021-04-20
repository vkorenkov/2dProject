using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    [SerializeField] Text healthCount;

    public void OutputHealthCount(string count)
    {
        healthCount.text = count;
    }
}
