using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理僵尸受伤时 掉落和头颅时的视觉效果，挂载到ZombieVisual下
/// </summary>
public class ZombieDamagedVisual : MonoBehaviour
{
    [SerializeField] private GameObject healthModelHolder;

    [SerializeField] private Transform outerArmTransform;
    [SerializeField] private Transform outerArmBoneTransform;

    private HealthModel healthModel;

    private void Start()
    {
        healthModel = healthModelHolder.GetComponent<ICharacter>().GetHealthModel();
    }
    private void OnEnable()
    {
        healthModel.OnHealthChanged += HealthModel_OnHealthChanged;
    }
    private void OnDisable()
    {
        healthModel.OnHealthChanged -= HealthModel_OnHealthChanged;
    }

    private void HealthModel_OnHealthChanged(object sender, HealthModel.OnHealthChangedEventArgs e)
    {
        if (e.after <= 0) // 没血了，这是死掉了，掉头
        {
            DismemberHead();
        }
        else if (e.after <= healthModel.MaxHealth / 2) // 掉了半血，该掉胳膊了
        {
            DismemberArm();
        }
    }
    private void DismemberHead()
    {

    }
    private void DismemberArm()
    {

    }
}
