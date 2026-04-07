using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlantSO))]
public class PlantSO : ScriptableObject
{
    [Header("Basic Params")]
    public string plantName;
    public float cooldownTimerMax;
    public int sunCost;
    [Header("References")]
    public Transform prefab;
    public Transform visualPrefab;

    // Captureµ½µÄËõÂÔÍ¼
    public Sprite thumbnail;
}
