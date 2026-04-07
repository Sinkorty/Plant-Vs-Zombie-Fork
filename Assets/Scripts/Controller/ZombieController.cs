using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour, ICharacter, IZombieController
{
    public event EventHandler OnHit;
    public event EventHandler OnDie;

    [SerializeField] private ZombieSO zombieSO;
    [SerializeField] private CollisionCheck zombieHitboxCollsionCheck;

    private HealthModel healthModel;
    private ZombieModel model;

    private bool isInitialized = false;

    private void Awake()
    {
        Init(2);
    }
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

    // 当碰撞到植物 tag: PlantCheckbox
    private void ZombieHitboxCollsionCheck_OnCollided(Transform obj)
    {
        IPlantController plantController = obj.GetComponentInParent<IPlantController>();
        //model.isBiting = true;
        model.zombieState = ZombieState.Biting;
    }

    private void ZombieController_OnHit(object sender, EventArgs e)
    {
        Debug.Log($"Hit! current Health: {healthModel.Health}");
    }

    /// <summary>
    /// 僵尸受伤，用于给子弹调用
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
    public int GetCurrentLine() => model.gridLine;

    public HealthModel GetHealthModel() => healthModel;
}
