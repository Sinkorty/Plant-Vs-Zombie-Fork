using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionCheck : MonoBehaviour
{
    /// <summary>
    /// 检测到碰撞的时候触发调用
    /// </summary>
    public event Action<Transform> OnCollided;

    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private float checkTimerMax = 0.2f;

    private Collider2D mCollider;
    private float checkTimer;
    private bool canCheck;
    private List<Collider2D> resultList;
    private ContactFilter2D filter;

    private void Awake()
    {
        mCollider = GetComponent<Collider2D>();
        resultList = new List<Collider2D>();
        filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.useLayerMask = true;
        filter.SetLayerMask(targetLayerMask);
        checkTimer = checkTimerMax;
        canCheck = true;
        //OnCollided += (_) => Debug.Log("Collided!");
    }

    private void Update()
    {
        if (!canCheck) return;

        checkTimer -= Time.deltaTime;
        if (checkTimer <= 0)
        {
            checkTimer = checkTimerMax;
            // check
            int hitCount = mCollider.OverlapCollider(filter, resultList);
            if (hitCount > 0)
            {
                OnCollided?.Invoke(resultList[0].transform);
            }
        }
    }
    public void StartCheck()
    {
        canCheck = true;
    }
    public void StopCheck()
    {
        canCheck = false;
    }
}
