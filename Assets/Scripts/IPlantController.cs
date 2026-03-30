using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO：Multi Plants
public interface IPlantController
{
    /// <summary>
    /// 植物在发射逻辑执行的那个时刻调用（还未执行）
    /// </summary>
    public event EventHandler OnLaunch;

    public GridCellController GetGridCell();
    public void SetGridCell(GridCellController gridCellController);

    //public Transform GetSpawnPointTransform();
    public void SetTarget();
}
