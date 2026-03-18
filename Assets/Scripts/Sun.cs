using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public enum State
    {
        Idle,
        FallingDown,
        PickingUp,
    }

    private float fallingTimer; // -1表示停止计时
    private float fallingTimerMax;
    private float fallingSpeed;

    private State currentState;

    private void Awake()
    {
        fallingTimer = -1;
        currentState = State.Idle;
    }
    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.FallingDown:
                FallingDownUpdateLogic();
                break;
            default:
                break;
        }

    }
    private void FallingDownUpdateLogic()
    {
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
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentState = State.PickingUp;
        }
    }
    public void StartFallDown(float fallingTime, float fallingSpeed)
    {
        fallingTimerMax = fallingTime;
        this.fallingSpeed = fallingSpeed;
        fallingTimer = fallingTimerMax; // 启用计时

        currentState = State.FallingDown;
    }
    public bool IsFalling() => currentState == State.FallingDown;
    public bool IsPickingUp() => currentState == State.PickingUp;
}
