using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonPultVisual : MonoBehaviour
{
    private const string ANIMATOR_LAUNCH = "launch";

    public event EventHandler OnWillLaunch;

    [SerializeField] private MelonPult melonPult;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        melonPult.OnBeforeLaunch += MelonPult_OnPulted;
    }

    private void MelonPult_OnPulted(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ANIMATOR_LAUNCH);
    }
    public void AnimationCallback_WillLaunch()
    {
        OnWillLaunch?.Invoke(this, EventArgs.Empty);
    }
}
