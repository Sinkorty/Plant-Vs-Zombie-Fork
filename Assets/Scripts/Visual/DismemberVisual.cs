using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 僵尸身体部件掉落视觉相关代码
/// </summary>
public class DismemberVisual : MonoBehaviour
{
    [SerializeField] private GameObject dismemberPart;      // 延迟销毁时间
    [Header("位移相关")]
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float localGroundY;
    [Header("旋转相关")]
    [SerializeField] private float initialSpeedMin = 420f;           // 初始旋转速度（度/秒）
    [SerializeField] private float initialRotateSpeedMax = 720f;     // 初始旋转速度（度/秒）
    [SerializeField] private float damping = 3f;                     // 阻尼系数（越大减速越快）
    [Header("渐出相关")]
    [SerializeField] private float fadeoutDelay = 2f;       // 延迟销毁时间
    [SerializeField] private float fadeoutTimerMax;
    private bool isInitialized = false;     // 防止未经初始化就开始运作

    private SpriteRenderer[] rendererArray;
    private Color sharedColor;

    [Header("暴露参数，之后会删")]
    public Vector3 velocity;
    public bool isProcessing = false;
    public bool hasDismembered;
    public bool isFadingoutDelaying = false;
    public bool isFadingout = false;

    public float currentRotateSpeed;
    public float rotateDirection;           // 旋转方向（1或-1）
    public float fadeoutDelayTimer;
    public float fadeoutTimer;

    private void Update()
    {
        if (!isInitialized) return;

        if (Input.GetKeyDown(KeyCode.Space))
            Init();

        ProcessMovementUpdateLogic();
        ProcessRotationUpdateLogic();

        FadeoutDelayUpdateLogic();
        FadeoutUpdateLogic();
    }
    private void ProcessMovementUpdateLogic()
    {
        if (!isProcessing) return;
        velocity += Vector3.down * acceleration * Time.deltaTime;
        if (transform.localPosition.y > localGroundY)
        {
            transform.localPosition += velocity * Time.deltaTime;
        }
        else
        {
            isProcessing = false;
            // 结束，开始fade out delay
            isFadingoutDelaying = true;
        }
    }
    private void ProcessRotationUpdateLogic()
    {
        if (!isProcessing) return;
        currentRotateSpeed = Mathf.Lerp(currentRotateSpeed, 0, damping * Time.deltaTime); // 应用阻尼（速度逐渐减小）
        transform.Rotate(0, 0, currentRotateSpeed * Time.deltaTime);
    }
    private void FadeoutDelayUpdateLogic()
    {
        if (!isFadingoutDelaying) return;

        fadeoutDelayTimer += Time.deltaTime;
        if (fadeoutDelayTimer > fadeoutDelay)
        {
            fadeoutDelayTimer = 0;
            isFadingoutDelaying = false;

            isFadingout = true;
        }
    }
    private void FadeoutUpdateLogic()
    {
        if (!isFadingout) return;

        fadeoutTimer += Time.deltaTime;

        sharedColor.a = Mathf.Clamp01(1f - fadeoutTimer / fadeoutTimerMax);
        foreach (var renderer in rendererArray)
        {
            renderer.color = sharedColor;
        }

        if (fadeoutTimer > fadeoutTimerMax)
        {
            fadeoutTimer = 0;
            isFadingout = false;
        }
    }

    /// <summary>
    /// 业务初始化
    /// </summary>
    public void Init()
    {
        if (hasDismembered) return;

        isInitialized = true;
        isProcessing = true;
        fadeoutDelayTimer = 0;
        sharedColor = Color.white;

        // 将位置设置到对应肢体的位置之后，设置父子级关系
        Transform dismemberPartTransform = Instantiate(dismemberPart).transform;
        transform.position = dismemberPartTransform.position;
        dismemberPartTransform.SetParent(transform);

        //originPosition = transform.position;
        velocity = new Vector3(Random.value, 1, 0).normalized; // 赋予 初速度

        rotateDirection = Random.value > 0.5f ? 1f : -1f;
        currentRotateSpeed = Random.Range(initialSpeedMin, initialRotateSpeedMax) * rotateDirection;

        rendererArray = transform.GetComponentsInChildren<SpriteRenderer>();
    }
}