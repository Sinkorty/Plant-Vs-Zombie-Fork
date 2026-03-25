using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 只要Init之后就能自行显示
public class SeedPacketUI : MonoBehaviour
{
    [SerializeField] private Image cooldownFillingImage;
    [SerializeField] private Image uninteratableFillingImage;
    [SerializeField] private TextMeshProUGUI sunCostText;
    [SerializeField] private Image thumbNailImage;
    [SerializeField] private Button button;

    private GameModel gameModel;

    private float cooldownTimer;
    public bool isInitialized = false;
    public bool isAffordable = false;
    public bool isSelecting = false;

    private PlantSO plantSO;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }
    private void Start()
    {
        gameModel = GameManager.Instance.GetModel();
        gameModel.OnSunAmountChanged += GameModel_OnSunAmountChanged;
        //GridMapController.Instance.OnAnyGridCellPlanted += GridMapController_OnAnyGridCellPlanted;
        GridMapController.Instance.OnAnyGridCellPlanted += GridMapManager_OnAnyGridCellPlanted;
    }

    private void GridMapManager_OnAnyGridCellPlanted(object sender, GridMapController.OnAnyGridCellPlantedEventArgs e)
    {
        // 刚被种下的植物是不是当前卡槽对应的植物
        if (e.plantSO == plantSO)
        {
            // 如果是，取消选择
            Deselect();
        }
    }

    private void OnClick()
    {
        print("click");
        if (!CanSelect()) return;

        if (isSelecting)
        {
            Deselect();
        }
        else
        {
            Select();
        }
    }
    private void GameModel_OnSunAmountChanged()
    {
        isAffordable = gameModel.SunAmount >= plantSO.sunCost;
    }

    private void Update()
    {
        if (!isInitialized) return;

        // 计时
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownTimer = Mathf.Max(0, cooldownTimer);
        }
        // 计算填充
        float percentage = cooldownTimer / plantSO.cooldownTimerMax;
        //print(cooldownTimer);
        cooldownFillingImage.fillAmount = percentage;

        // 按键监听
        if (isSelecting && Input.GetMouseButtonDown(1)) //右键，取消，放回
        {
            Deselect();
        }

        // 填充图像的显示
        uninteratableFillingImage.gameObject.SetActive(!isAffordable || isSelecting);
    }
    private bool IsCoolingDown() => cooldownTimer != 0;
    private bool CanSelect() => !IsCoolingDown() && isAffordable;
    public void Init(int bankId)
    {
        plantSO = GameManager.Instance.GetModel().GetPlantSOFromSeedBankById(bankId);
        thumbNailImage.sprite = plantSO.thumbnail;
        sunCostText.text = plantSO.sunCost.ToString();
        cooldownFillingImage.gameObject.SetActive(true);
        uninteratableFillingImage.gameObject.SetActive(true);

        //Only for test:
        cooldownTimer = plantSO.cooldownTimerMax;
        isInitialized = true;
    }
    // 选择，不过会判断是否可以选择
    public void Select()
    {
        if (gameModel.HasSelectedPlant()) return;
        print("aaaa");
        isSelecting = true;
        gameModel.SelectedPlant = plantSO;
    }
    public void Deselect()
    {
        if (!gameModel.HasSelectedPlant()) return;
        isSelecting = false;
        gameModel.SelectedPlant = null;
    }
}
