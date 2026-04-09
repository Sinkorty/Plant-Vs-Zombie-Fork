using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通过订阅事件来告诉事件的调用者对应行的groundY在哪，需要业务初始化
/// </summary>
public class GroundYPoints : MonoBehaviour
{
    [SerializeField] private GetGroundYEventSO getGroundYEventSO;

    private List<float> groundYList;

    /// <summary>
    /// 业务初始化，调用了才能发挥作用
    /// </summary>
    public void Init()
    {
        groundYList.Clear();
        foreach (Transform child in transform)
        {
            groundYList.Add(child.position.y);
        }
        getGroundYEventSO.OnFloatEvent += GetGroundYEventSO_OnFloatEvent;
    }
    private void OnDisable()
    {
        getGroundYEventSO.OnFloatEvent -= GetGroundYEventSO_OnFloatEvent;
    }

    private float GetGroundYEventSO_OnFloatEvent(int gridLine)
    {
        if (gridLine < 1 || gridLine > groundYList.Count) // 初步判断这些事非法数据
        {
            Debug.LogError("Invalid grid line");
            return default;
        }
        return groundYList[gridLine - 1];
    }
}
