using System;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(BulletSO))]
public class BulletSO : ScriptableObject
{
    [Header("Basic Params")]
    public string bulletName;
    public ProjectType projectType; // ·¢ÉäÖÖÀà
    public StraightBulletData straightBulletData;
    public PultBulletData pultBulletData;
    public TraceBulletData traceBulletData;

    public int damage;
    public bool hasSlowEffect;
    public bool isAreaDamage;
    public bool canImmobilized; // »ÆÓÍ

    [Header("Resource References")]
    public Sprite sprite;
}

public abstract class BulletData
{
    public abstract ProjectType GetProjectType();
}

[Serializable]
public class StraightBulletData : BulletData
{
    public override ProjectType GetProjectType() => ProjectType.Straight;
    public int speed;
}
[Serializable]
public class PultBulletData : BulletData
{
    public override ProjectType GetProjectType() => ProjectType.Pult;
    public int height;
    public int duration;
}
[Serializable]
public class TraceBulletData : BulletData
{
    public override ProjectType GetProjectType() => ProjectType.Trace;
    public int speed;
}