using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapController : MonoBehaviour
{
    public static GridMapController Instance { get; private set; }

    public event EventHandler<OnAnyGridCellPlantedEventArgs> OnAnyGridCellPlanted; // 任意格子被种下植物的时候调用
    public class OnAnyGridCellPlantedEventArgs : EventArgs
    {
        public PlantSO plantSO;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instance err!");
            return;
        }
        Instance = this;
    }

    public void AnyGridCellPlanted(OnAnyGridCellPlantedEventArgs e)
    {
        OnAnyGridCellPlanted?.Invoke(this, e);
    }
}
