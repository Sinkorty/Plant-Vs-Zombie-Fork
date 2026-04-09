using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTest : MonoBehaviour
{
    private Transform child;

    private void Awake()
    {
        child = transform.GetChild(0);

        Debug.Log($"parent position: {transform.position}");
        Debug.Log($"parent localPosition: {transform.localPosition}");
        Debug.Log($"child position: {child.position}");
        Debug.Log($"child localPosition: {child.localPosition}");
    }
}
