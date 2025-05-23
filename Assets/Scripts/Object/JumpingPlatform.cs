using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class JumpingPlatform : MonoBehaviour
{
    [SerializeField] private JumpingPlatformDataSO jumpingPlatformDataSo;

    // 플레이어와 점프대 충돌 판정
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("점프!!!");
        // TryGetComponent 방식과 레이어를 검출하는 방식 중 어느 것이 좋은 설계인지 모르겠음
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Rigidbody playerRb = other.collider.GetComponent<Rigidbody>();
            playerRb.AddForce(Vector3.up * jumpingPlatformDataSo.jumpPower, ForceMode.Impulse);
        }
    }
}
