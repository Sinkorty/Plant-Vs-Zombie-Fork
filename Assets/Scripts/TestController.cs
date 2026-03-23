using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public static TestController Instance { get; private set; }

    [SerializeField] private MelonPult melonPult;
    [SerializeField] private Transform targetTranfrom;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Multiple Instance!");
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            melonPult.SetTarget(targetTranfrom);
        }
    }
}
