using UnityEngine;

public class CollectObjects : MonoBehaviour
{
    [SerializeField] Output output;

    public int killedEnemies;
    public int collectedObjectsCount;

    int allBonuses;

    private void Update()
    {
        allBonuses += collectedObjectsCount;
        output.OutputBonusCount($"{collectedObjectsCount}");
    }
}
