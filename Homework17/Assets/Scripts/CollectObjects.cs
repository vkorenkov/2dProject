using System.Collections.Generic;
using UnityEngine;

public class CollectObjects : MonoBehaviour
{
    [SerializeField] Output output;

    public int killedEnemies;
    public int collectedObjectsCount;

    public static Dictionary<int, string> hints = new Dictionary<int, string>()
    {
        [0] = "Maybe somewhere in a cave?",
        [1] = "Was exactly meant to be on the way here.",
        [2] = "Maybe look at the bottom?",
        [3] = "There is no beer here :(",
        [4] = "Your advertisement could be here."
    };

    int allBonuses;

    private void Update()
    {
        allBonuses += collectedObjectsCount;
        output.OutputBonusCount($"{collectedObjectsCount}");
    }
}
