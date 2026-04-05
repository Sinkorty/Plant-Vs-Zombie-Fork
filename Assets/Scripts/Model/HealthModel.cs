using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModel
{
    private OnHealthChangedEventArgs sharedOnHealthChangedEventArgs;

    private int health;
    private int maxHealth;

    public int Health
    {
        get => health;
        set
        {
            int before = health;
            health = Mathf.Clamp(value, 0, maxHealth);
            sharedOnHealthChangedEventArgs.before = before;
            sharedOnHealthChangedEventArgs.after = health;
            OnHealthChanged?.Invoke(this, sharedOnHealthChangedEventArgs);
        }
    }
    public int MaxHealth { get => maxHealth; }
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs : EventArgs
    {
        public int before;
        public int after;
    }

    public HealthModel(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
        sharedOnHealthChangedEventArgs = new OnHealthChangedEventArgs();
    }
}
