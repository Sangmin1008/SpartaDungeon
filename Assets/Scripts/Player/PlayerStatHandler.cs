using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private StatDataSO baseStat;
    [SerializeField] private FloatEventChannelSO damageEventChannel;
    [SerializeField] private FloatEventChannelSO cureEventChannel;

    private float _currentHealth;
    public float CurrentHealth
    {
        get { return _currentHealth; }
    }
    
    private void Start()
    {
        _currentHealth = baseStat.maxHealth;
        damageEventChannel.OnEventRaised += TakeDamage;
        cureEventChannel.OnEventRaised += TakeHealing;
    }

    private void OnDestroy()
    {
        damageEventChannel.OnEventRaised -= TakeDamage;
        cureEventChannel.OnEventRaised -= TakeHealing;
    }


    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0f);

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    public void TakeHealing(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(baseStat.maxHealth, _currentHealth);
    }

    private void Die()
    {
        Debug.Log("죽음!");
    }
}
