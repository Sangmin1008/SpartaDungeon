using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTimeAtPoint = 1f;

    private int _currentTargetIndex = 0;
    private float _waitTimer = 0f;

    private void Start()
    {
        if (waypoints.Length < 2)
            Debug.LogWarning("이동 장소 2개 미만");
    }

    private void FixedUpdate()
    {
        if (waypoints.Length < 2) return;
        MovePlatform();
    }

    // 플랫폼을 waypoint마다 이동시킴
    private void MovePlatform()
    {
        Vector3 targetPos = waypoints[_currentTargetIndex].position;
        Vector3 currentPos = transform.position;

        if (Vector3.Distance(currentPos, targetPos) < 0.05f)
        {
            _waitTimer += Time.fixedDeltaTime;
            if (_waitTimer >= waitTimeAtPoint)
            {
                _waitTimer = 0f;
                _currentTargetIndex = (_currentTargetIndex + 1) % waypoints.Length;
            }
        }
        else
        {
            Vector3 direction = (targetPos - currentPos).normalized;
            transform.position += direction * moveSpeed * Time.fixedDeltaTime;
        }
    }

    // 플레이어가 플랫폼에서 떨어지지 않도록, 접촉할 경우 부모-자식 관계를 세팅
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
