using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridMapManager : MonoBehaviour
{
    [SerializeField] private Transform gridMapTransform;

    private Dictionary<Vector2Int, GridCellController> plantGridMap;

    private void Awake()
    {
        plantGridMap = new Dictionary<Vector2Int, GridCellController>();
        ResetPlantGridMap();
    }

    private void ResetPlantGridMap()
    {
        plantGridMap.Clear();
        foreach (var gridCell in gridMapTransform.GetComponentsInChildren<GridCellController>())
        {
            plantGridMap[gridCell.GetGridPosition()] = gridCell;
        }
    }
}
