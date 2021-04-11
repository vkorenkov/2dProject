using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    Rigidbody2D bombRb;
    ParticleSystem explosionPs;
    PointEffector2D bombEffector;

    // Start is called before the first frame update
    void Start()
    {
        bombRb = GetComponent<Rigidbody2D>();
        explosionPs = GetComponentInChildren<ParticleSystem>();
        bombEffector = GetComponent<PointEffector2D>();

        bombRb.AddForce(Vector2.up * Random.Range(2, 5), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bombEffector.enabled = true;
        explosionPs.Play();
        Destroy(gameObject, 0.5f);
    }
}
