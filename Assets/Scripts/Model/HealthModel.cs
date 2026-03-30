using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModel
{
    private int health;
    private int maxHealth;

    public int Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    public HealthModel(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }
}
