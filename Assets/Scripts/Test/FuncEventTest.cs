using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncEventTest : MonoBehaviour
{
    public event Func<int, float> @event;

    private void Awake()
    {
        @event += FuncEventTest_event1;
        @event += FuncEventTest_event2;

        float result = @event.Invoke(6);
        print(result);
    }

    private float FuncEventTest_event2(int arg)
    {
        print("event2 invoked");
        return arg;
    }

    private float FuncEventTest_event1(int arg)
    {
        print("event1 invoked");
        return -arg;
    }
}
