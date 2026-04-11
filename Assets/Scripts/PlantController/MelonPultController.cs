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

    private Transform target; // 要打的僵尸
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
        pultBullet.Init(bulletSO, bulletSpawnPoint, target.transform.position, currentGridCell.GetLine());
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

    /// <summary>
    /// 设置该植物的攻击目标，同时也订阅了僵尸死亡时 SetTarget(null)
    /// </summary>
    /// <param name="zombieController"></param>
    public void SetTarget(IZombieController zombieController)
    {
        if (zombieController == null)
        {
            target = null;
            return;
        }
        zombieController.OnDied += ZombieController_OnDied;
        target = (zombieController as MonoBehaviour).transform;
    }

    private void ZombieController_OnDied(object sender, EventArgs e)
    {
        print("Invoked");
        SetTarget( null);
    }


    public void Hit(int damage)
    {
        healthModel.Health -= damage;
    }

    public HealthModel GetHealthModel() => healthModel;
    public int GetGridLine() => currentGridCell.GetLine();
}
