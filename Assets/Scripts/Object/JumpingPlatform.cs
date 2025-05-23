using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class JumpingPlatform : MonoBehaviour
{
    [SerializeField] private JumpingPlatformDataSO jumpingPlatformDataSo;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("점프!!!");
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Rigidbody playerRb = other.collider.GetComponent<Rigidbody>();
            playerRb.AddForce(Vector3.up * jumpingPlatformDataSo.jumpPower, ForceMode.Impulse);
        }
    }
}
