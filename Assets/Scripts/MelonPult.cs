using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonPult : MonoBehaviour, IPlant
{
    public event EventHandler OnBeforeLaunch;

    [SerializeField] private PlantSO plantSO;
    [SerializeField] private Transform melonBulletPrefab;

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private MelonPultVisual melonPultVisual;

    private Transform target; // 要打的目标 TODO：替换为 IZombie
    [SerializeField] private GridCell currentGridCell; // 所在的 GridCell,TODO: 到时候把序列化去掉，这个是用来测试的

    private float pultTimer;
    private float pultTimerMax = 3f;

    private void Start()
    {
        melonPultVisual.OnWillLaunch += MelonPultVisual_OnWillLaunch;
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
        OnBeforeLaunch?.Invoke(this, EventArgs.Empty);
        Debug.Log("Pult");
    }
    private void GenerateBullet()
    {
        Transform melonBulletTransform = Instantiate(melonBulletPrefab);
        PultBullect melonBullet = melonBulletTransform.GetComponent<PultBullect>();
        melonBullet.Initialize(bulletSpawnPoint.position, target.position);
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
    public void SetGridCell(GridCell gridCell)
    {
        currentGridCell = gridCell;
    }
    public bool HasTarget() => target != null;
}
