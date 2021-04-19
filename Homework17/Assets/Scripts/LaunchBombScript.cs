using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBombScript : MonoBehaviour
{
    float bombLaunchTimer;
    [SerializeField] GameObject bombObj;
    [SerializeField, Range(1, 3)] float bombLaunchTimerMaxValue = 1;
    bool canLaunch;

    private void Start()
    {
        bombLaunchTimer = Random.Range(0.5f, bombLaunchTimerMaxValue);
    }

    private void Update()
    {
        if (canLaunch)
        {
            bombLaunchTimer -= Time.deltaTime;

            if (bombLaunchTimer <= 0)
            {
                Instantiate(bombObj, transform.position, transform.rotation);
                canLaunch = false;
                bombLaunchTimer = Random.Range(0, bombLaunchTimerMaxValue);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.ToLower() == "player" || collision.tag.ToLower() == "movable")
            canLaunch = true;
    }
}
