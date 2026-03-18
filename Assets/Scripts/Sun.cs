using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private float fallingTimer; // -1表示停止计时
    private float fallingTimerMax;
    private float fallingSpeed;

    private void Awake()
    {
        fallingTimer = -1;
    }
    private void Update()
    {
        if (fallingTimer > 0)
        {
            fallingTimer -= Time.deltaTime;
            transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
            if (fallingTimer <= 0)
            {
                fallingTimer = -1;
                Debug.Log("falling is over");
            }
        }
    }
    private void OnMouseEnter()
    {
        Debug.Log("Interact");
    }
    public void StartFallDown(float fallingTime, float fallingSpeed)
    {
        fallingTimerMax = fallingTime;
        fallingTimer = fallingTimerMax;
        this.fallingSpeed = fallingSpeed;
    }
    public bool IsFalling() => fallingTimer != -1;
}
