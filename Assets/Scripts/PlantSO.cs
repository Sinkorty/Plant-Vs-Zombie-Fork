using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlantSO))]
public class PlantSO : ScriptableObject
{
    public string plantName;
    public Transform prefab;
    public Transform visualPrefab;


}
