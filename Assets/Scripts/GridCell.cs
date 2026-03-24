using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private Vector2Int relativePosition;


    public int GetRow() => relativePosition.x;
    public int GetLine() => relativePosition.y;
}
