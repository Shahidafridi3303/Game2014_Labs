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

    [SerializeField] private GameObject GameCanvas;
    [SerializeField] private GameObject GameOverCanvas;

    public AudioSource audioSource;
    public AudioClip GameOverClip;

    private SoundManager _soundManager;

    public bool CanTakeDamage { get; set; } = true;

    private void Start()
    {
        _soundManager = GetComponent<SoundManager>();
        // Initialize the health bar to full
        UpdateHealthBar();
    }

    // Function to take damage
    public void TakeDamage(float damageAmount)
    {
        if (!CanTakeDamage) return; // Exit if damage is disabled

        _currentHealth -= damageAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maximumHealth);
        UpdateHealthBar();
        _soundManager.PlayTakeDamage();

        if (_currentHealth <= 0)
        {
            // Game Over logic
            _soundManager.PlayPlayerDeath();
            GameOverCanvas.SetActive(true);
            GameCanvas.SetActive(false);
            Time.timeScale = 0f;

            // Play Game Over music
            audioSource.clip = GameOverClip;
            audioSource.Play();

            // Access the PlayerMovement script and set isDead to true
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        }
    }

    // Function to add health
    public void AddHealth(float healthAmount)
    {
        _soundManager.PlayHealthPickup();
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