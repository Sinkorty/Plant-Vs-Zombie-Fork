using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 测试控制类，享有最高控制权，用于测试对象功能是否正常
public class TestController : MonoBehaviour
{
    public static TestController Instance { get; private set; }

    //[Header("Test for zombie hit")]
    //[SerializeField] private MelonPultController melonPult;
    //[SerializeField] private Transform targetTranfrom;

    [Header("Temp control on SeedBank")]
    [SerializeField] private PlantSO melonPultPlantSO;
    [SerializeField] private PlantSO cornPultPlantSO;

    [SerializeField] private SeedBankUI seedBankUI; // 测试用，随时能删

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Multiple Instance!");
        Instance = this;
    }
    private void Start()
    {
        //GhostVisual.Instance.Show(testPlantSO);
        GameManager.Instance.GetModel().AddPlantToSeedBank(melonPultPlantSO);
        GameManager.Instance.GetModel().AddPlantToSeedBank(cornPultPlantSO);

        seedBankUI.UpdateVisual();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    melonPult.SetTarget(targetTranfrom);
        //}
        //// 测试刷新
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    seedBankUI.UpdateVisual();
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    melonPult.SetTarget(targetTranfrom);
        //}
    }
}
