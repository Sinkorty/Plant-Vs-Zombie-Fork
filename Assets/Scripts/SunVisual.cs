using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 负责处理捡起来的视觉效果
public class SunVisual : MonoBehaviour
{
    [SerializeField] private Sun sun;
    [SerializeField] private float interpolation = 0.8f;
    [SerializeField] private float disappearMinDistance = 0.2f;

    private void Update()
    {
        if (sun.IsPickingUp())
        {
            Vector3 pickingUpEndPos = new Vector3(-6.28f, 3.77f, 0);
            Vector3 pos = Vector3.Lerp(sun.gameObject.transform.position, pickingUpEndPos, interpolation * Time.deltaTime);
            sun.gameObject.transform.position = pos;

            if (Vector3.Distance(sun.gameObject.transform.position, pickingUpEndPos) < disappearMinDistance)
            {
                Destroy(sun.gameObject);
            }
        }
    }
}