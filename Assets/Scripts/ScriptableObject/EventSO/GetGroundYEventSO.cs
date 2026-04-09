using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "eventSO/" + nameof(GetGroundYEventSO))]
public class GetGroundYEventSO : ScriptableObject
{
    public event Func<int, float> OnFloatEvent;

    public float Raise(int gridLine)
    {
        if (OnFloatEvent == null)
        {
            Debug.LogError("should not happen!");
            return default;
        }
        return OnFloatEvent.Invoke(gridLine);
    }
}
