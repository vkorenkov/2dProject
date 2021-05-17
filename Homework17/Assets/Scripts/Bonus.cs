using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public bool isBonus;
    public bool isProjectile;
    public bool isHealth;

    [SerializeField] float healthRecovery;
    [SerializeField] int projectileRecovery;
    [SerializeField] ParticleSystem getBonusEffect;
    [SerializeField] Animation DescriptionAnimation;
    Collider2D playerCollision;

    [SerializeField] TextMeshPro bonusDescription;

    private void Update()
    {
        if (isBonus)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerCollision)
            {
                getBonusEffect.Play();
                playerCollision.GetComponent<CollectObjects>().collectedObjectsCount += 1;
                DestroyBonus(getBonusEffect.main.duration);
                playerCollision = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isBonus)
            {
                if (bonusDescription)
                {
                    playerCollision = collision;
                    DescriptionAnimation["DescriptionAnimation"].speed = 1;
                    DescriptionAnimation.Play();
                }
            }

            if (isHealth)
            {
                collision.GetComponent<HealthManager>().currentHealth += healthRecovery;
                DestroyBonus(getBonusEffect.main.startLifetime.constant);
                getBonusEffect.Play();
            }

            if (isProjectile)
            {
                collision.GetComponentInChildren<BulletLauncher>().bulletCont += projectileRecovery;
                DestroyBonus(getBonusEffect.main.startLifetime.constant);
                getBonusEffect.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollision = null;

            if (bonusDescription)
            {
                DescriptionAnimation["DescriptionAnimation"].time = DescriptionAnimation["DescriptionAnimation"].length;
                DescriptionAnimation["DescriptionAnimation"].speed = -1;
                DescriptionAnimation.Play();
            }
        }
    }

    void DestroyBonus(float destroyTime)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject.transform.parent.gameObject, destroyTime);
    }
}
