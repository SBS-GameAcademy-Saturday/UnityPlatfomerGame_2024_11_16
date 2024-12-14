using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Slider _playerHealthBar;
    [SerializeField] TextMeshProUGUI _playerHealthText;
    
    Damagable _playerDamagable;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player) Debug.LogError("Player Tag can't Found");
        _playerDamagable = player.GetComponent<Damagable>();

        _playerDamagable._onHealthChanged.AddListener(OnPlayerHealthChanged);

        OnPlayerHealthChanged(_playerDamagable.Health, _playerDamagable.MaxHealth);
    }

    private void OnDestroy()
    {
        _playerDamagable._onHealthChanged.RemoveListener(OnPlayerHealthChanged);   
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        _playerHealthBar.value = CalculrateSliderValue(newHealth, maxHealth);
        _playerHealthText.text = $"{newHealth} / {maxHealth}";
    }

    private float CalculrateSliderValue(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

}
