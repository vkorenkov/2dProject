using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyGround : MonoBehaviour
{
    /// <summary>
    /// ��������� ������������ ������ �����
    /// </summary>
    List<GameObject> currentParts;

    private void Awake()
    {
        currentParts = MenuCutscene.currentParts;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ����������� ����� ������
        if (other.CompareTag("Player"))
        {
            try
            {
                var temp = currentParts.FirstOrDefault();

                Destroy(temp);
                currentParts.Remove(temp);
            }
            catch
            {
                Debug.Log("Null");
            }
        }
    }
}
