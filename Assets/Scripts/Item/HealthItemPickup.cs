using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private HealthItemDataSO itemDataSo;
    [SerializeField] private FloatEventChannelSO cureEventChannel;

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("플레이어 충돌!!");
            cureEventChannel.Raise(itemDataSo.hpValue);
            Destroy(transform.root.gameObject);
        }
    }
    */

    public string GetInteractPrompt()
    {
        return $"{itemDataSo.itemName}\n{itemDataSo.description}";
    }

    public void OnInteract()
    {
        cureEventChannel.Raise(itemDataSo.hpValue);
        Destroy(transform.root.gameObject);
    }
}
