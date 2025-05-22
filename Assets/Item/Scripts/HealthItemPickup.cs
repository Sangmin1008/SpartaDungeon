using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemPickup : MonoBehaviour
{
    [SerializeField] private HealthItemDataSO itemDataSo;
    [SerializeField] private FloatEventChannelSO cureEventChannel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("플레이어 충돌!!");
            cureEventChannel.Raise(itemDataSo.hpValue);
            Destroy(transform.root.gameObject);
        }
    }
}
