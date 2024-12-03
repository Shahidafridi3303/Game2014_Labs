 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    int _value;
    [SerializeField]
    int _maxHealth;

    Slider _slider;

    [SerializeField] bool _gameOverCondition = false;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;
    }

    public void ResetHealthBar()
    {
        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _slider.value -= damage;

        if (_slider.value <= 0)
        {
            // Player died. Lose one life

            if (_gameOverCondition == true) // If there is no life left, and this character's life is a game condition,
            {
                // Game Over
            }
        }
    }

    public void HealHealth(int healAmount)
    {
        _slider.value += healAmount;
        if (_slider.value >= _maxHealth)
        {
            _slider.value = _maxHealth;
        }
    }

}

