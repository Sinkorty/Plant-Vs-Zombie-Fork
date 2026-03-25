using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapModel
{
    private Dictionary<Vector2Int, GridCellModel> plantGridMap;

    public GridMapModel()
    {
        plantGridMap = new Dictionary<Vector2Int, GridCellModel>();
    }
    // 专门给 GridCellModel 注册的
    public void Register(GridCellModel gridCellModel)
    {
        plantGridMap[gridCellModel.GridPosition] = gridCellModel;
    }
}
