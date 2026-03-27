using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有拥有血条，能受伤的角色应该要实现该接口（植物、僵尸）
/// </summary>
public interface ICharacter
{
    public void Hit();
}
