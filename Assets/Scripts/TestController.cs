using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 测试控制类，享有最高控制权，用于测试对象功能是否正常
public class TestController : MonoBehaviour
{
    public static TestController Instance { get; private set; }

    [SerializeField] private MelonPult melonPult;
    [SerializeField] private Transform targetTranfrom;
    [SerializeField] private PlantSO testPlantSO;

    private Dictionary<Vector2Int, PlantSO> plantGridMap; // 相对位置 -> 植物SO

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Multiple Instance!");
        Instance = this;
    }
    private void Start()
    {
        GhostObject.Instance.Show(testPlantSO);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            melonPult.SetTarget(targetTranfrom);
        }
    }
}
