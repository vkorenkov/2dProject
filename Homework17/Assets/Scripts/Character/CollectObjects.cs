using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectObjects : MonoBehaviour
{
    /// <summary>
    /// Объект вывода информации на экран
    /// </summary>
    [SerializeField] Output output;

    public Collected collected;

    /// <summary>
    /// Коллекция описаний возле дверей
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
        output.OutputBonusCount($"Now: {collected.CollectedObjectsCount} / All: {collected.allBonuses}");
        output.OutputKillsCount($"All: {collected.KilledEnemiesCount}");
    }
}
