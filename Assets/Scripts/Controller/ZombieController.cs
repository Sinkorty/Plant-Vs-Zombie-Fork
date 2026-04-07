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
    public event EventHandler OnDie;

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
            StartCoroutine(StartBiting());
        }
    }
    // TODO: 先给植物实现血量，之后在实现这个
    private IEnumerator StartBiting()
    {
        model.zombieState = ZombieState.Biting;
        yield return null;
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
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }

    // 接口实现
    public int GetCurrentLine() => model.gridLine;
    public HealthModel GetHealthModel() => healthModel;
}
