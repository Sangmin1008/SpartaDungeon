using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO gameOverEventChannel;
    
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
        SceneManager.LoadScene("MainGameScene");
    }
}
