using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 投手子弹类，先生成PultBulletPrefab，之后调用脚本的Initialize(起始点，结束点)即可使用
/// </summary>
public class PultBullet : MonoBehaviour, IBullet
{
    [SerializeField] private float height = 2f;
    [SerializeField] private float duration = 1f;

    private BulletSO bulletSO;

    // 封装了碰撞检测的逻辑
    private CollisionCheck collisionCheck;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private float elapsedTime = 0f;
    private int currentLine; // 记录是第几行的植物发射的子弹

    private bool isFinished = false;
    private bool isInitialized = false;

    private void Awake()
    {
        collisionCheck = GetComponent<CollisionCheck>();
        collisionCheck.OnCollided += CollisionCheck_OnCollided;
    }

    private void CollisionCheck_OnCollided(Transform obj)
    {
        // 返回的对象的LayerMask已经是僵尸了，此时判断是不是同一行的（因为投手植物的子弹是抛物线，可能会打到上几行的僵尸）
        // 实际上是zombie的子物体HitCheckbox，因此要从最近的父级寻找ZombieController
        ZombieController zombie = obj.GetComponentInParent<ZombieController>();
        if (zombie.GetCurrentLine() == currentLine)
        {
            print($"currentLine: {currentLine}, zombie's line: {zombie.GetCurrentLine()}");
            zombie.Hit(bulletSO.damage);
            DestroySelf();
        }
    }

    private void Update()
    {
        if (!isInitialized)
        {
            Debug.LogError("场景中存在未被初始化的Bullet");
            return;
        }

        if (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float parabolicT = 4 * t * (1 - t);
            Vector2 horizontalPos = Vector2.Lerp(startPoint, endPoint, t);
            float yOffset = parabolicT * height;

            transform.position = new Vector3(horizontalPos.x, horizontalPos.y + yOffset);
            elapsedTime += Time.deltaTime;
        }
        else
        {
            if (!isFinished)
            {
                isFinished = true;
                DestroySelf();
            }
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// 初始化Bullet
    /// </summary>
    public void Initialize(BulletSO bulletSO, Vector2 startPoint, Vector2 endPoint, int currentLine)
    {
        this.bulletSO = bulletSO; // FIX：忘记给bulletSO赋值了
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.currentLine = currentLine;

        transform.position = startPoint;
        isInitialized = true;
    }
    /// <summary>
    /// 高级的初始化Bullet, 根据起始点计算初始角度
    /// </summary>
    public void Initialize(BulletSO bulletSO, Transform startPoint, Vector2 endPoint, int currentLine)
    {
        Initialize(bulletSO, startPoint.position, endPoint, currentLine);
        transform.eulerAngles = startPoint.eulerAngles;
    }
}
