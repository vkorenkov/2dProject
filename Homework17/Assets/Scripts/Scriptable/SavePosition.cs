using UnityEngine;

[CreateAssetMenu(fileName = "position", menuName = "Position saver")]
public class SavePosition : ScriptableObject
{
    [SerializeField] private Vector2 position;
    public Vector2 Position
    {
        get => position;
        set { position = value; }
    }

    private bool usePosition;
    public bool UsePosition
    {
        get => usePosition;
        set { usePosition = value; }
    }
}
