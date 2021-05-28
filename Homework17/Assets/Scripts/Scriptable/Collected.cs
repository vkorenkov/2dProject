using UnityEngine;

[CreateAssetMenu(fileName = "Collected", menuName = "Player collected")]
public class Collected : ScriptableObject
{
    public int allBonuses;

    [SerializeField] private int killedEnemiesCount;
    public int KilledEnemiesCount
    {
        get => killedEnemiesCount;
        set { killedEnemiesCount = value; }
    }

    [SerializeField] private int collectedObjectsCount;
    public int CollectedObjectsCount
    {
        get => collectedObjectsCount;
        set { collectedObjectsCount = value; }
    }
}
