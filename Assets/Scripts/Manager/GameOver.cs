using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO gameOverEventChannel;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerStatHandler statHandler;
    [SerializeField] private UIConditionController conditionController;
    

    private void Start()
    {
        gameOverEventChannel.OnEventRaised += Restart;
    }

    private void OnDestroy()
    {
        gameOverEventChannel.OnEventRaised -= Restart;
    }

    private void Restart()
    {
        player.transform.position = new Vector3(0, 2, 0);
        statHandler.TakeHealing(100);
        conditionController.TakeHealing(100);
    }
}
