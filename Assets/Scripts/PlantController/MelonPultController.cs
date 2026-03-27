using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonPultController : MonoBehaviour, IPlantController
{
    [Header("Config Ref")]
    [SerializeField] private PlantSO plantSO;
    [SerializeField] private Transform melonBulletPrefab;

    [Header("Scene Ref")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private PlantVisual melonPultVisual;

    private Transform target; // 要打的目标 TODO：替换为 IZombie
    private GridCellController currentGridCell; // 所在的 GridCell,TODO: 到时候把序列化去掉，这个是用来测试的

    private float pultTimer;
    private float pultTimerMax = 3f;

    public event EventHandler OnLaunch;

    private void Start()
    {
        melonPultVisual.OnProject += MelonPultVisual_OnWillLaunch;
    }

    // 动画回调，表示即将发射Melon
    private void MelonPultVisual_OnWillLaunch(object sender, EventArgs e)
    {
        GenerateBullet();
    }

    private void Update()
    {
        if (pultTimer >= 0)
        {
            pultTimer -= Time.deltaTime;
            if (pultTimer < 0)
            {
                pultTimer = pultTimerMax;
                if (HasTarget())
                {
                    Pult();
                }
            }
        }
    }
    private void Pult()
    {
        OnLaunch?.Invoke(this, EventArgs.Empty);
    }
    private void GenerateBullet()
    {
        Transform melonBulletTransform = Instantiate(melonBulletPrefab); // 生成
        // 初始化
        PultBullect melonBullet = melonBulletTransform.GetComponent<PultBullect>();
        melonBullet.Initialize(bulletSpawnPoint.position, target.position);
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
    public void SetGridCell(GridCellController gridCell)
    {
        currentGridCell = gridCell;
    }
    public bool HasTarget() => target != null;
}
