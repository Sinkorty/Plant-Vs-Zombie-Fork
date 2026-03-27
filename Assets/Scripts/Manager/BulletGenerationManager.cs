using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 负责子弹游戏对象的实例化，是个单例工厂
/// </summary>
public class BulletGenerationManager : MonoBehaviour
{
    public static BulletGenerationManager Instance { get; private set; }

    [SerializeField] private Transform bulletTemplatePrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            UnityEngine.Debug.LogError("Multiple instance err!");
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// 只负责场景当中根据种类实例化不同类型的子弹
    /// </summary>
    public IBullet Instantiate(BulletSO bulletSO)
    {
        Transform bulletTransform = Instantiate(bulletTemplatePrefab);
        bulletTransform.GetComponentInChildren<SpriteRenderer>().sprite = bulletSO.sprite;

        switch (bulletSO.projectType) // 工厂
        {
            case ProjectType.Pult: return bulletTransform.AddComponent<PultBullet>();
            case ProjectType.Trace: return bulletTransform.AddComponent<TraceBullet>();
            case ProjectType.Straight: return bulletTransform.AddComponent<StraightBullet>();
            default: throw new System.Exception("????");
        }
    }
}
