using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public bool isBonus;
    public bool isProjectile;
    public bool isHealth;

    private bool canPickUp;

    HealthManager healthManager;
    CollectObjects collectObjects;
    BulletLauncher bulletLauncher;

    [SerializeField] float healthRecovery;
    [SerializeField] int projectileRecovery;
    [SerializeField] ParticleSystem getBonusEffect;
    [SerializeField] Animation DescriptionAnimation;

    [SerializeField] TextMeshPro bonusDescription;

    private void Awake()
    {
        var player = GameObject.Find("MainCharacter");

        if (isHealth)
            healthManager = player.GetComponent<HealthManager>();
        if (isProjectile)
            bulletLauncher = player.GetComponentInChildren<BulletLauncher>();
        if (isBonus)
            collectObjects = player.GetComponent<CollectObjects>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickUp)
        {
            if (isBonus) collectObjects.collectedObjectsCount += 1;

            if (isHealth && healthManager.currentHealth < healthManager.maxHealth)
            {
                var current = healthManager.maxHealth - healthManager.currentHealth;
                healthManager.currentHealth += current > healthRecovery ? healthRecovery : current;
            }
            else if (!isBonus)
                return;

            getBonusEffect.Play();

            DestroyBonus(getBonusEffect.main.duration);

            canPickUp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canPickUp = true;

            if (isBonus)
                ShowDescription();

            if (isHealth)
                ShowDescription();

            if (isProjectile)
            {
                bulletLauncher.bulletCount += projectileRecovery;
                DestroyBonus(getBonusEffect.main.startLifetime.constant);
                getBonusEffect.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canPickUp = false;

            if (bonusDescription)
            {
                DescriptionAnimation["DescriptionAnimation"].time = DescriptionAnimation["DescriptionAnimation"].length;
                DescriptionAnimation["DescriptionAnimation"].speed = -1;
                DescriptionAnimation.Play();
            }
        }
    }

    void ShowDescription()
    {
        DescriptionAnimation["DescriptionAnimation"].speed = 1;
        DescriptionAnimation.Play();
    }

    void DestroyBonus(float destroyTime)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject.transform.parent.gameObject, destroyTime);
    }
}
