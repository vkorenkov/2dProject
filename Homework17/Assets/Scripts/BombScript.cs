using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombScript : MonoBehaviour
{
    Rigidbody2D bombRb;
    ParticleSystem explosionPs;
    PointEffector2D bombEffector;
    SpriteRenderer bombSprite;
    CircleCollider2D bombPointEffector;

    private void Awake()
    {
        bombRb = GetComponent<Rigidbody2D>();
        explosionPs = GetComponentInChildren<ParticleSystem>();
        bombEffector = GetComponent<PointEffector2D>();
        bombSprite = GetComponent<SpriteRenderer>();
        bombPointEffector = GetComponent<CircleCollider2D>();

        bombRb.AddForce(transform.up * Random.Range(2, 5), ForceMode2D.Impulse);
        bombRb.AddTorque(Random.Range(-2, 2), ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (!explosionPs.isPlaying && !bombSprite.enabled)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bombSprite.enabled)
        {
            explosionPs.Play();
            bombSprite.enabled = false;
            bombPointEffector.enabled = true;
            bombEffector.enabled = true;
            bombRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Debug.Log($"{collision.tag}");
    }
}
