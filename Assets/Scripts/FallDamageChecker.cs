using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamageChecker : MonoBehaviour
{
    [SerializeField] private FloatEventChannelSO fallDurationEventChannel;
    [SerializeField] private float fallThreshold = 2.0f;
    [SerializeField] private float damagePerSecond = 30f;
    
    private IDamageable _damageable;

    private void Start()
    {
        _damageable = GetComponent<IDamageable>();
        fallDurationEventChannel.OnEventRaised += OnFallDurationReceived;
    }

    private void OnDestroy()
    {
        fallDurationEventChannel.OnEventRaised -= OnFallDurationReceived;
    }

    private void OnFallDurationReceived(float airTime)
    {
        if (airTime > fallThreshold)
        {
            float damage = (airTime - fallThreshold) * damagePerSecond;
            _damageable.TakeDamage(damage);
        }
    }
}
