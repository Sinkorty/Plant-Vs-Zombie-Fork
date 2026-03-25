using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Sun : MonoBehaviour
{
    public enum State
    {
        Idle,
        FallingDown,
        PickingUp,
    }

    [SerializeField] private float fallingTimer; // -1表示停止计时
    private float fallingTimerMax;
    private float fallingSpeed;

    [Header("Picking Up Params")]
    [SerializeField] private float interpolation = 0.8f;
    [SerializeField] private float disappearMinDistance = 0.2f;

    [Header("Falling Down Params")]
    [SerializeField] private float sunFallingDownSpeedMin = 0.03f;
    [SerializeField] private float sunFallingDownSpeedMax = 0.06f;
    [SerializeField] private float sunFallingDownTimeMin = 5;
    [SerializeField] private float sunFallingDownTimeMax = 8;

    //[Header("Scene Ref")]
    private Transform pickingUpEndPoint;    // 该引用通过SunGenerationManager来获取
    private Button button;

    private State currentState;

    private void Awake()
    {
        fallingTimer = -1;
        currentState = State.Idle;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    private void Start()
    {
        pickingUpEndPoint = SunGenerationManager.Instance.GetPickupEndPoint();
    }
    private void Update()
    {
        switch (currentState)
        {
            case State.FallingDown:
                FallingDownUpdateLogic();
                break;
            case State.PickingUp:
                PickingUpUpdateLogic();
                break;
            default:
                break;
        }
    }
    private void FallingDownUpdateLogic()
    {
        //print(fallingTimer);
        if (fallingTimer > 0)
        {
            fallingTimer -= Time.deltaTime;
            transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
            if (fallingTimer <= 0)
            {
                fallingTimer = -1;
            }
        }
    }
    private void PickingUpUpdateLogic()
    {
        Vector3 pickingUpEndPos = pickingUpEndPoint.position;

        Vector3 pos = Vector3.Lerp(gameObject.transform.position, pickingUpEndPos, interpolation * Time.deltaTime);
        gameObject.transform.position = pos;

        if (Vector3.Distance(gameObject.transform.position, pickingUpEndPos) < disappearMinDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnClick()
    {
        currentState = State.PickingUp;
    }
    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        currentState = State.PickingUp;
    //    }
    //}
    public void StartFallDown()
    {
        fallingTimerMax = UnityEngine.Random.Range(sunFallingDownTimeMin, sunFallingDownTimeMax);
        fallingSpeed = UnityEngine.Random.Range(sunFallingDownSpeedMin, sunFallingDownSpeedMax);

        fallingTimer = fallingTimerMax; // 启用计时
        currentState = State.FallingDown;
        //print(fallingTimer);
    }
    public bool IsFalling() => currentState == State.FallingDown;
    public bool IsPickingUp() => currentState == State.PickingUp;
}
