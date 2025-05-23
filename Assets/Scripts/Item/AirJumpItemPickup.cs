using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJumpItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private AirJumpItemDataSO airJumpItemDataSo;
    [SerializeField] private FloatEventChannelSO jumpInAirEventChannel;

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            jumpInAirEventChannel.Raise(airJumpItemDataSo.airJumpDuration);
            Destroy(transform.root.gameObject);
        }
    }
    */
    
    public string GetInteractPrompt()
    {
        return $"{airJumpItemDataSo.name}\n{airJumpItemDataSo.description}\nPress the 'E' key";
    }

    // 인터랙트 실행되면 공중점프 이벤트 발생
    public void OnInteract()
    {
        jumpInAirEventChannel.Raise(airJumpItemDataSo.airJumpDuration);
        Destroy(transform.root.gameObject);
    }
}
