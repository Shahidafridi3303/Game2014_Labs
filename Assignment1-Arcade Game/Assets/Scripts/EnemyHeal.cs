using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void HandleDeath()
    {
        currentHealth = maxHealth;
        gameObject.SetActive(false);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
