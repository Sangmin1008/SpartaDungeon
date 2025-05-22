using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJumpItemPickup : MonoBehaviour
{
    [SerializeField] private AirJumpItemDataSO airJumpItemDataSo;
    [SerializeField] private FloatEventChannelSO jumpInAirEventChannel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            jumpInAirEventChannel.Raise(airJumpItemDataSo.airJumpDuration);
            Destroy(transform.root.gameObject);
        }
    }
}
