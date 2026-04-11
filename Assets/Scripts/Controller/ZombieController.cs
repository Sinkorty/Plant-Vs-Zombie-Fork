using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通僵尸 Controller
/// </summary>
public class ZombieController : MonoBehaviour, ICharacter, IZombieController
{
    public event EventHandler OnHit;
    public event EventHandler OnDied;
    
    [SerializeField] private ZombieSO zombieSO;
    [SerializeField] private CollisionCheck zombieHitboxCollsionCheck;

    private HealthModel healthModel;
    private ZombieModel model;

    // 不需要Model持有的数据
    private bool isInitialized = false;
    private IPlantController targetPlant;

    private void Update()
    {
        if (!isInitialized) return;

        if (model.zombieState == ZombieState.Walking)
        {
            float speed = zombieSO.moveSpeed;
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (model.zombieState == ZombieState.Biting) BitingUpdateLogic();
    }
    private void BitingUpdateLogic()
    {
        // 动画是响应状态机播放的，但是pvz里僵尸啃咬植物的逻辑貌似是发生在动画之中的
    }
    private void OnEnable()
    {
        zombieHitboxCollsionCheck.OnCollided += ZombieHitboxCollsionCheck_OnCollided;
        OnHit += ZombieController_OnHit;
    }
    private void OnDisable()
    {
        zombieHitboxCollsionCheck.OnCollided -= ZombieHitboxCollsionCheck_OnCollided;
        OnHit -= ZombieController_OnHit;
    }
    // 业务初始化
    public void Init(int gridLine)
    {
        model = new ZombieModel { gridLine = gridLine };
        healthModel = new HealthModel(zombieSO.maxHealth);

        isInitialized = true;
    }

    // 当检测到面前有植物的时候调用（不止一次）
    private void ZombieHitboxCollsionCheck_OnCollided(Transform obj)
    {
        IPlantController plantController = obj.GetComponentInParent<IPlantController>();
        if (model.zombieState != ZombieState.Biting)
        {
            StartBiting(targetPlant);
        }
    }
    // TODO: 先给植物实现血量，之后在实现这个
    private void StartBiting(IPlantController targetPlant)
    {
        this.targetPlant = targetPlant;
        model.zombieState = ZombieState.Biting;
    }

    // 当受伤的时候调用（用于测试）
    private void ZombieController_OnHit(object sender, EventArgs e)
    {
        Debug.Log($"Hit! current Health: {healthModel.Health}");
    }

    /// <summary>
    /// 让僵尸受伤，用于给子弹调用
    /// </summary>
    public void Hit(int damage)
    {
        healthModel.Health -= damage;
        OnHit?.Invoke(this, EventArgs.Empty);
        if (healthModel.Health <= 0)
        {
            //model.isDead = true;
            model.zombieState = ZombieState.Dying;
            OnDied?.Invoke(this, EventArgs.Empty);
            Invoke("DestroySelf", 2f);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public int GetGridLine() => model.gridLine;

    // 接口实现
    public HealthModel GetHealthModel() => healthModel;
}
