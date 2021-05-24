using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyGround : MonoBehaviour
{
    List<GameObject> currentParts;
    [SerializeField] GameObject spawnBase;

    private void Awake()
    {
        //var t = Random.Range(0, 10);

        //Debug.Log(t);

        //if (t > 2)
        //{
        //    spawnBase.SetActive(true);
        //}

        currentParts = MenuCutscene.currentParts;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ”ничтожение блока дороги
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
