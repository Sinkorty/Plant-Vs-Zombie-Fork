using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonPultController : MonoBehaviour, IPlantController, ICharacter
{
    [Header("Config Ref")]
    [SerializeField] private PlantSO plantSO;
    [SerializeField] private BulletSO bulletSO;

    [Header("Scene Ref")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private PlantVisual melonPultVisual;

    private Transform target; // 要打的目标 TODO：替换为 IZombie
    private GridCellController currentGridCell; // 所在的 GridCell

    private HealthModel healthModel;


    private float pultTimer;
    private float pultTimerMax = 3f;

    public event EventHandler OnLaunched;

    private void Awake()
    {
        healthModel = new HealthModel(plantSO.maxHealth);
    }
    private void OnEnable()
    {
        melonPultVisual.OnProject += MelonPultVisual_OnWillLaunch;
    }
    private void OnDisable()
    {
        melonPultVisual.OnProject -= MelonPultVisual_OnWillLaunch;
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
        OnLaunched?.Invoke(this, EventArgs.Empty);
    }
    private void GenerateBullet()
    {
        //Transform melonBulletTransform = Instantiate(melonBulletPrefab); // 生成
        //// 初始化
        //PultBullect melonBullet = melonBulletTransform.GetComponent<PultBullect>();
        ////melonBullet.Initialize(bulletSpawnPoint.position, target.position);
        //melonBullet.Initialize(bulletSO, bulletSpawnPoint, target.position);

        PultBullet pultBullet = BulletGenerationManager.Instance.Instantiate(bulletSO) as PultBullet;
        print(currentGridCell.GetLine());
        pultBullet.Init(bulletSO, bulletSpawnPoint, target.position, currentGridCell.GetLine());
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
    public GridCellController GetGridCell()
    {
        return currentGridCell;
    }
    public void SetGridCell(GridCellController gridCell)
    {
        currentGridCell = gridCell;
    }
    public bool HasTarget() => target != null;

    public void SetTarget(IZombieController zombieController)
    {
        if (zombieController == null)
        {
            target = null;
            return;
        }
        target = (zombieController as MonoBehaviour).transform;
    }

    public void Hit(int damage)
    {
        healthModel.Health -= damage;
    }

    public HealthModel GetHealthModel() => healthModel;
}
