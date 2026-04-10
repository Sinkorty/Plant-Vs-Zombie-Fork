using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通过订阅事件来告诉事件的调用者对应行的groundY在哪
/// </summary>
public class GroundYPoints : MonoBehaviour
{
    [SerializeField] private GetGroundYEventSO getGroundYEventSO;

    private List<float> groundYList;

    private void Awake()
    {
        groundYList = new List<float>();
        groundYList.Clear();
        foreach (Transform child in transform)
        {
            groundYList.Add(child.position.y);
        }
    }

    public void Start()
    {
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
