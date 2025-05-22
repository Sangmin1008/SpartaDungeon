using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MonoBehaviour
{
    [SerializeField] private JumpObjectDataSO jumpObjectDataSo;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("점프!!!");
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Rigidbody playerRb = other.collider.GetComponent<Rigidbody>();
            playerRb.AddForce(Vector3.up * jumpObjectDataSo.jumpPower, ForceMode.Impulse);
        }
    }
}
