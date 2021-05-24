using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuCutscene : MonoBehaviour
{
    [SerializeField] List<GameObject> grounds;
    [SerializeField] GameObject startPart;
    [SerializeField] float moveSpeed = 2;
    float partLenght;
    [SerializeField] int partsCount = 3;
    public static List<GameObject> currentParts;
    public static List<GameObject> bonuses = new List<GameObject>();

    private void Start()
    {
        partLenght = startPart.GetComponent<BoxCollider2D>().bounds.size.x;
        currentParts = new List<GameObject>();
        currentParts.Add(startPart);

        for (var i = 0; i < partsCount; i++)
        {
            SetRoadPart();
        }
    }

    private void Update()
    {
        Move();

        if (currentParts.Count < partsCount)
        {
            SetRoadPart();

            Destroy(bonuses.FirstOrDefault(), 10);
            bonuses.Remove(bonuses.FirstOrDefault());
        }
    }

    void Move()
    {
        transform.Translate(Vector2.right * -1 * moveSpeed * Time.deltaTime);
    }

    void SetRoadPart()
    {
        GameObject part = Instantiate(grounds[Random.Range(0, grounds.Count)], transform);
        var temp = currentParts.Last().transform.position.x;
        temp += partLenght - 0.02f;
        part.transform.position = new Vector3(temp, 0, 0);
        currentParts.Add(part);
    }
}
