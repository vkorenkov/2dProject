using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    Transform playerDefaultParent;

    List<WheelJoint2D> wheels;

    private void Start()
    {
        wheels = GetComponentsInChildren<WheelJoint2D>().ToList();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag.ToLower() == "player")
        {
            playerDefaultParent = collision.transform.parent;

            collision.transform.SetParent(transform);

            foreach(var w in wheels)
            {
                w.GetComponent<Rigidbody2D>().freezeRotation = false;
                w.useMotor = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.ToLower() == "player")
        {
            playerDefaultParent = null;

            collision.transform.SetParent(playerDefaultParent);

            foreach (var w in wheels)
            {
                w.useMotor = false;
                w.GetComponent<Rigidbody2D>().freezeRotation = true;
            }
        }
    }
}
