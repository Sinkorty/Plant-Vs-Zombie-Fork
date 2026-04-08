using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理僵尸受伤时 掉落和头颅时的视觉效果，挂载到ZombieVisual下
/// </summary>
public class ZombieDamagedVisual : MonoBehaviour
{
    [SerializeField] private GameObject healthModelHolder;

    [SerializeField] private DismemberVisual outerArmDismemberVisual;
    [SerializeField] private DismemberVisual headDismemberVisual;

    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform outerArmTransform;
    [SerializeField] private Transform outerArmBoneTransform;

    private HealthModel healthModel;

    private bool hasHeadDismembered;
    private bool hasArmDismembered;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            healthModel.Health -= 10;
        }
    }

    private void OnEnable()
    {
        // 这里把Start里初始化healthModel挪下来了，主要是因为OnEnable比Start先执行
        healthModel = healthModelHolder.GetComponent<ICharacter>().GetHealthModel();
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
        if (hasHeadDismembered) return;

        Debug.Log("掉头");

        // 这两句的时序很重要
        headDismemberVisual.Init();
        headTransform.gameObject.SetActive(false);

        hasHeadDismembered = true;
    }
    private void DismemberArm()
    {
        if (hasArmDismembered) return;

        Debug.Log("掉胳膊");
        outerArmDismemberVisual.Init();
        outerArmTransform.gameObject.SetActive(false);
        hasArmDismembered = true;
    }
}
