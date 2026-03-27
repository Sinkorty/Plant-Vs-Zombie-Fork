using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 投手子弹类，先生成PultBulletPrefab，之后调用脚本的Initialize(起始点，结束点)即可使用
/// </summary>
public class PultBullet : MonoBehaviour, IBullet
{
    //TODO: 应该放在 OnCollide 的时候调用
    public event EventHandler OnPultHit;

    [SerializeField] private float height = 2f;
    [SerializeField] private float duration = 1f;

    private BulletSO bulletSO;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private float elapsedTime = 0f;

    private bool isFinished = false;
    private bool isInitialized = false;

    private void Awake()
    {
        OnPultHit += PultBullect_OnPultHit;
    }

    // 自己订阅该消息，主要用来销毁自己
    private void PultBullect_OnPultHit(object sender, EventArgs e)
    {
        Destroy(gameObject);
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
                // TODO： 放在击中的时候调用
                OnPultHit?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    /// <summary>
    /// 初始化Bullet
    /// </summary>
    public void Initialize(BulletSO bulletSO, Vector2 startPoint, Vector2 endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        transform.position = startPoint;
        isInitialized = true;
    }
    /// <summary>
    /// 高级的初始化Bullet, 根据起始点计算初始角度
    /// </summary>
    public void Initialize(BulletSO bulletSO, Transform startPoint, Vector2 endPoint)
    {
        Initialize(bulletSO, startPoint.position, endPoint);
        transform.eulerAngles = startPoint.eulerAngles;
    }
}
