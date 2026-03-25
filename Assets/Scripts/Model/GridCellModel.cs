using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellModel
{
    private Vector2Int gridPosition;
    private PlantSO plantSO;

    public Vector2Int GridPosition
    {
        get => gridPosition;
        private set => gridPosition = value;
    }
    public PlantSO PlantSO
    {
        get => plantSO;
        set
        {
            plantSO = value;
        }
    }

    public GridCellModel(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;

        // ×¢²á¸øGridMaopModel
        GridMapModel plantGridMapModel = GameManager.Instance.GetModel().PlantGridMapModel;
        plantGridMapModel.Register(this);
    }
    public int GetLine() => GridPosition.y;
    public int GetRow() => GridPosition.x;

    public bool HasPlant() => plantSO != null;
}
