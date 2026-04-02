using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedPacketUI : MonoBehaviour
{
    [SerializeField] private Image cooldownFillingImage;
    [SerializeField] private Image uninteratableFillingImage;
    [SerializeField] private TextMeshProUGUI sunCostText;
    [SerializeField] private Image thumbNailImage;
    [SerializeField] private Button button;

    private GameModel gameModel;

    private float cooldownTimer;
    // TODO: 去掉public，just for test
    private bool isAffordable = false;
    private bool isSelecting = false;

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
        // 刚被种下的植物是不是当前卡槽对应的植物，如果是，取消选择，并重置冷却
        if (e.plantSO == plantSO)
        {
            Deselect();
            RefreshCooldown();
        }
    }

    private void OnClick()
    {
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
        // 计时
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownTimer = Mathf.Max(0, cooldownTimer);
        }


        // 按键监听
        if (isSelecting && Input.GetMouseButtonDown(1)) //右键，取消，放回
        {
            Deselect();
        }
    }
    private void LateUpdate()
    {
        // 计算并填充冷却图像
        float percentage = cooldownTimer / plantSO.cooldownTimerMax;
        cooldownFillingImage.fillAmount = percentage;

        // 不可交互 图像的显示
        uninteratableFillingImage.gameObject.SetActive(!CanSelect() || isSelecting);
    }

    private bool IsCoolingDown() => cooldownTimer != 0;
    private bool CanSelect() => !IsCoolingDown() && isAffordable;

    private void RefreshCooldown()
    {
        cooldownTimer = plantSO.cooldownTimerMax;
    }

    public void Init(int bankId)
    {
        plantSO = GameManager.Instance.GetModel().GetPlantSOFromSeedBankById(bankId);
        thumbNailImage.sprite = plantSO.thumbnail;
        sunCostText.text = plantSO.sunCost.ToString();
        cooldownFillingImage.gameObject.SetActive(true);
        uninteratableFillingImage.gameObject.SetActive(true);

        // 0阳光cost的植物默认isAffordable设置为true
        isAffordable = plantSO.sunCost == 0;

        //Only for test:
        cooldownTimer = plantSO.cooldownTimerMax;
    }
    // 选择，不过会判断是否可以选择
    public void Select()
    {
        if (gameModel.HasSelectedPlant()) return;
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
