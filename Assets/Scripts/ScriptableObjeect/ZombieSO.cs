using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ZombieSO))]
public class ZombieSO : ScriptableObject
{
    public string zombieName;
    public int maxHealth;
}
