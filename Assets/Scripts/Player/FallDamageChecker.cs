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

    // 공중에서 머물러있는 시간을 측정하고, 해당 값을 데미지로 변환하여 데미지 이벤트 호출
    private void OnFallDurationReceived(float airTime)
    {
        if (airTime > fallThreshold)
        {
            float damage = (airTime - fallThreshold) * damagePerSecond;
            damageEventChannel.Raise(damage);
        }
    }
}
