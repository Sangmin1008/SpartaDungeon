using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private StatDataSO baseStat;
    [SerializeField] private FloatEventChannelSO damageEventChannel;

    private float _currentHealth;
    public float CurrentHealth
    {
        get { return _currentHealth; }
    }
    
    private void Start()
    {
        _currentHealth = baseStat.maxHealth;
        damageEventChannel.OnEventRaised += TakeDamage;
    }

    private void OnDestroy()
    {
        damageEventChannel.OnEventRaised -= TakeDamage;
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

    private void Die()
    {
        Debug.Log("죽음!");
    }
}
