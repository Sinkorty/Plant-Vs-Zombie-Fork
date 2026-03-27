using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornPultController : MonoBehaviour, IPlantController
{
    public event EventHandler OnLaunch;

    public void SetGridCell(GridCellController gridCell)
    {
        throw new NotImplementedException();
    }
}
