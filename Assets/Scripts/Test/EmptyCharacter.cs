using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空角色物体，用于测试血条相关逻辑
/// </summary>
public class EmptyCharacter : MonoBehaviour, ICharacter
{
    public HealthModel healthModel;

    private void Awake()
    {
        healthModel = new HealthModel(100);
    }

    public HealthModel GetHealthModel() => healthModel;

    public void Hit(int damage) => healthModel.Health -= damage;
}
