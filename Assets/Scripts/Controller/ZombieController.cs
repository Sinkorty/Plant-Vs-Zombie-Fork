using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour, ICharacter, IZombieController
{
    [SerializeField] private ZombieSO zombieSO;

    private HealthModel healthModel;
    private ZombieModel model;

    public event EventHandler OnHit;
    public event EventHandler OnDie;

    private void Awake()
    {
        //int maxHealth = 20;
        healthModel = new HealthModel(zombieSO.maxHealth);
        model = new ZombieModel { gridLine = 2 }; // TODO£ºJust for test
        OnHit += ZombieController_OnHit;
    }

    private void ZombieController_OnHit(object sender, EventArgs e)
    {
        Debug.Log($"Hit! current Health: {healthModel.Health}");
    }

    public void Hit(int damage)
    {
        healthModel.Health -= damage;
        OnHit?.Invoke(this, EventArgs.Empty);
        if (healthModel.Health == 0)
        {
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }
    public void Attack()
    {

    }
    public int GetCurrentLine() => model.gridLine;

    public HealthModel GetHealthModel() => healthModel;
}
