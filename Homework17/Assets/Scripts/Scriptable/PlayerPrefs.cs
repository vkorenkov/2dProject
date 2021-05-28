using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPrefs", menuName = "Create Player Prefs")]
public class PlayerPrefs : ScriptableObject
{
    [SerializeField] float maxHealth;
    public float MaxHealth
    {
        get => maxHealth;
    }

    [SerializeField] float currentHealth;
    public float CurrentHealth
    {
        get => currentHealth;
        set { currentHealth = value; }
    }

    [SerializeField] int bulletCount;
    public int BulletCount
    {
        get => bulletCount;
        set { bulletCount = value; }
    }
}
