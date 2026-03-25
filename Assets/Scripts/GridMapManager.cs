using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ÉúÃüÖÜÆÚ£ºGameScene
public class GridMapManager : MonoBehaviour
{
    [SerializeField] private Transform gridMapTransform;

    private Dictionary<Vector2Int, GridCell> plantGridMap;

    private void Awake()
    {
        plantGridMap = new Dictionary<Vector2Int, GridCell>();
        ResetPlantGridMap();
    }

    private void ResetPlantGridMap()
    {
        plantGridMap.Clear();
        foreach (var gridCell in gridMapTransform.GetComponentsInChildren<GridCell>())
        {
            plantGridMap[gridCell.GetGridPosition()] = gridCell;
        }
    }
}
