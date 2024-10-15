using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth = 100f;

    [SerializeField]
    private float _maximumHealth = 100f;

    [SerializeField]
    private Image healthBar;

    private void Start()
    {
        // Initialize the health bar to full
        UpdateHealthBar();
    }

    // Function to take damage
    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maximumHealth);
        UpdateHealthBar();

        if (_currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
        }
    }

    // Function to add health
    public void AddHealth(float healthAmount)
    {
        _currentHealth += healthAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maximumHealth);
        UpdateHealthBar();
    }

    // Function to update the health bar UI
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = _currentHealth / _maximumHealth;
        }
    }
}