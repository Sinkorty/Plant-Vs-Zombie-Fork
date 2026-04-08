using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 僵尸身体部件掉落脚本
/// </summary>
public class DismemberVisual : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 2f;      // 延迟销毁时间
    [SerializeField] private float acceleration;
    [SerializeField] private Transform groundYTransform;   // 地面高度

    public bool isProcessing = false;
    //public Vector3 originPosition;
    public Vector3 velocity;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Invoke();

        if (!isProcessing) return;

        velocity += Vector3.down * acceleration * Time.deltaTime;
        if (transform.position.y > groundYTransform.position.y)
        {
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            isProcessing = false;
            // 结束，开始fade out
        }
    }


    public void Invoke()
    {
        transform.SetParent(null);
        isProcessing = true;
        //originPosition = transform.position;
        velocity = new Vector3(Random.value, 1, 0).normalized; // 赋予 初速度
    }

}