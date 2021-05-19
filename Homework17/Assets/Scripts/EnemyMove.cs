using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    GameObject player;
    HealthManager health;
    [SerializeField] bool thisMustMove;
    [SerializeField, Range(0.1f, 1)] float speed;
    Animator moveAnimation;
    bool isMovable;

    float distance
    {
        get => Vector2.Distance(player.transform.position, transform.position);
    }

    Vector2 playerPosition
    {
        get => player.transform.position;
    }

    private void Awake()
    {
        moveAnimation = GetComponent<Animator>();
        health = GetComponent<HealthManager>();
        health.ControlEnableEvent += Health_ControlEnableEvent;
        player = GameObject.Find("MainCharacter");
        isMovable = true;
    }

    private void Health_ControlEnableEvent(bool isAlive)
    {
        isMovable = isAlive;
        moveAnimation.SetBool("Walk", false);
    }

    private void Update()
    {
        if (isMovable && thisMustMove)
        {
            if (distance < 1 && health.isAlive)
            {
                moveAnimation.SetBool("Walk", isMovable);

                var newPosition = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

                transform.position = new Vector2(newPosition.x, transform.position.y);

                transform.rotation = EnemySight();
            }
            else
                moveAnimation.SetBool("Walk", false);
        }
    }

    Quaternion EnemySight()
    {
        return playerPosition.x < transform.position.x ? Quaternion.identity : new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
    }
}
