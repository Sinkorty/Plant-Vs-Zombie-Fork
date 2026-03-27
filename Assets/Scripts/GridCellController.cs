using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellController : MonoBehaviour
{
    [SerializeField] private Vector2Int gridPosition;
    private GameModel gameModel;
    private GridCellModel model;

    // 简单实现好了
    private void OnMouseEnter()
    {
        if (!model.HasPlant() && gameModel.HasSelectedPlant())
        {
            GridMapGhostVisual.Instance.Show(gameModel.SelectedPlant, this);
        }
    }
    private void OnMouseExit()
    {
        if (!model.HasPlant() && gameModel.HasSelectedPlant())
        {
            GridMapGhostVisual.Instance.Hide();
        }
    }
    private void Start()
    {
        model = new GridCellModel(gridPosition);
        gameModel = GameManager.Instance.GetModel();
    }
    private void OnMouseDown()
    {
        if (gameModel.HasSelectedPlant() && !model.HasPlant())
        {
            SpawnPlant();
        }
    }
    //private void OnMouseOver()
    //{
    //    // TODO: 光这点判断还是不够的，还要知道是否在捡阳光
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        SpawnPlant();
    //    }
    //}
    private void SpawnPlant()
    {
        // 如果有植物了，就不种
        if (model.HasPlant())
        {
            return;
        }
        model.PlantSO = gameModel.SelectedPlant;
        Transform melonPultPlantTransform = Instantiate(gameModel.SelectedPlant.prefab);
        melonPultPlantTransform.position = transform.position;

        // 减少阳光
        gameModel.SunAmount -= model.PlantSO.sunCost;
        // 集中统一事件管理，这里主要是给 SeedPacketUI 解耦
        GridMapController.Instance.AnyGridCellPlanted(new GridMapController.OnAnyGridCellPlantedEventArgs { plantSO = model.PlantSO });
    }

    public int GetRow() => gridPosition.x;
    public int GetLine() => gridPosition.y;
    public Vector2Int GetGridPosition() => gridPosition;
}
