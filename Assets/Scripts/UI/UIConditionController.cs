using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConditionController : MonoBehaviour
{
    [SerializeField] private Condition health;
    [SerializeField] private FloatEventChannelSO damageEventChannel;
    [SerializeField] private FloatEventChannelSO cureEventChannel;

    private void Start()
    {
        damageEventChannel.OnEventRaised += TakeDamage;
        cureEventChannel.OnEventRaised += TakeHealing;
    }

    private void OnDestroy()
    {
        damageEventChannel.OnEventRaised -= TakeDamage;
        cureEventChannel.OnEventRaised -= TakeHealing;
    }

    private void TakeDamage(float amount)
    {
        Debug.Log("아파아아아아앗");
        health.Subtract(amount);
    }

    public void TakeHealing(float amount)
    {
        health.Add(amount);
    }
}
