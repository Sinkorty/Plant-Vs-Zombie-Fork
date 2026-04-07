using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombieController
{
    /// <summary>
    /// 业务初始化，用于给ZombieGeneration
    /// </summary>
    /// <param name="gridLine">第几行</param>
    public void Init(int gridLine);
}
