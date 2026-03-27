using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlantVisual : MonoBehaviour
{
    private const string ANIMATOR_LAUNCH = "launch";

    /// <summary>
    /// 该实际发射出子弹的时候执行
    /// </summary>
    public event EventHandler OnProject;

    [SerializeField] private GameObject plantControllerGameObject;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (plantControllerGameObject.TryGetComponent(out IPlantController plantController))
        {
            plantController.OnLaunch += PlantController_OnPulted;
        }
        else
        {
            Debug.LogError("PlantVisual.Start err! plantControllerGameObject is not valid!");
        }
    }

    private void PlantController_OnPulted(object sender, EventArgs e)
    {
        animator.SetTrigger(ANIMATOR_LAUNCH);
    }
    // Launch 动画通过关键帧回调这个方法
    public void AnimationCallback_WillLaunch()
    {
        OnProject?.Invoke(this, EventArgs.Empty);
    }
}
