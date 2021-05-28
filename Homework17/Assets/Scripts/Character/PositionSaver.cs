using UnityEngine;

public class PositionSaver : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var tempP = collision.GetComponent<MoveCharacter>();

            tempP.savePosition.Position = tempP.transform.position;
        }
    }
}
