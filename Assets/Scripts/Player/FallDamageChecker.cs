using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamageChecker : MonoBehaviour
{
    [SerializeField] private FloatEventChannelSO fallDurationEventChannel;
    [SerializeField] private FloatEventChannelSO damageEventChannel;
    [SerializeField] private float fallThreshold = 2.0f;
    [SerializeField] private float damagePerSecond = 30f;
    
    private void Start()
    {
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
            damageEventChannel.Raise(damage);
        }
    }
}
