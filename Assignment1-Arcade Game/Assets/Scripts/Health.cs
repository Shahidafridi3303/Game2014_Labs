using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float maximumHealth = 100f;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameOverClip;

    private SoundManager soundManager;

    public bool CanTakeDamage { get; set; } = true;

    private void Start()
    {
        InitializeComponents();
        UpdateHealthBar(); // Initialize the health bar to full
    }

    private void InitializeComponents()
    {
        soundManager = GetComponent<SoundManager>();
    }

    // Function to take damage
    public void TakeDamage(float damageAmount)
    {
        if (!CanTakeDamage) return; // Exit if damage is disabled

        ApplyDamage(damageAmount);
        UpdateHealthBar();
        soundManager.PlayTakeDamage();

        if (IsHealthDepleted())
        {
            HandleGameOver();
        }
    }

    private void ApplyDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maximumHealth);
    }

    private bool IsHealthDepleted()
    {
        return currentHealth <= 0;
    }

    private void HandleGameOver()
    {
        soundManager.PlayPlayerDeath();
        ShowGameOverScreen();
        PlayGameOverMusic();
    }

    private void ShowGameOverScreen()
    {
        gameOverCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        Time.timeScale = 0f;
    }

    private void PlayGameOverMusic()
    {
        audioSource.clip = gameOverClip;
        audioSource.Play();
    }

    // Function to add health
    public void AddHealth(float healthAmount)
    {
        soundManager.PlayHealthPickup();
        IncreaseHealth(healthAmount);
        UpdateHealthBar();
    }

    private void IncreaseHealth(float healthAmount)
    {
        currentHealth += healthAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maximumHealth);
    }

    // Function to update the health bar UI
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maximumHealth;
        }
    }
}