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
    private bool isBiting = false;
    private bool isDead = false;

    private void Awake()
    {
        Init(2);
    }
    private void Update()
    {
        if (!isInitialized) return;

        // 执行移动
        if (!isBiting && !isDead)
        {
            float speed = zombieSO.moveSpeed;
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
    public void Init(int gridLine)
    {
        model = new ZombieModel { gridLine = 2 }; // TODO：Just for test
        healthModel = new HealthModel(zombieSO.maxHealth);
        OnHit -= ZombieController_OnHit;
        OnHit += ZombieController_OnHit;
        zombieHitboxCollsionCheck.OnCollided -= ZombieHitboxCollsionCheck_OnCollided;
        zombieHitboxCollsionCheck.OnCollided += ZombieHitboxCollsionCheck_OnCollided;

        isInitialized = true;
    }

    // 当碰撞到植物 tag: PlantCheckbox
    private void ZombieHitboxCollsionCheck_OnCollided(Transform obj)
    {
        IPlantController plantController = obj.GetComponentInParent<IPlantController>();
        isBiting = true;
    }

    private void ZombieController_OnHit(object sender, EventArgs e)
    {
        Debug.Log($"Hit! current Health: {healthModel.Health}");
    }

    public void Hit(int damage)
    {
        healthModel.Health -= damage;
        OnHit?.Invoke(this, EventArgs.Empty);
        if (healthModel.Health <= 0)
        {
            isDead = true;
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }
    public int GetCurrentLine() => model.gridLine;

    public HealthModel GetHealthModel() => healthModel;


}
