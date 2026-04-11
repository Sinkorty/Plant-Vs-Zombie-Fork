using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理僵尸受伤时 掉落和头颅时的视觉效果，挂载到ZombieVisual下
/// </summary>
public class ZombieDamagedVisual : MonoBehaviour
{
    [SerializeField] private GameObject characterHolder;
    [SerializeField] private GetGroundYEventSO getGroundYEventSO;
    
    [SerializeField] private DismemberVisual outerArmDismemberVisual;
    [SerializeField] private DismemberVisual headDismemberVisual;

    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform outerArmTransform;
    
    private HealthModel healthModel;
    private int gridLine; // 需要通过ICharacter.GetGridLine()

    private bool hasHeadDismembered;
    private bool hasArmDismembered;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        healthModel.Health -= 10;
    //    }
    //}

    private void Start()
    {
        healthModel = characterHolder.GetComponent<ICharacter>().GetHealthModel();
        gridLine = characterHolder.GetComponent<IZombieController>().GetGridLine();
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

        float groundY = getGroundYEventSO.Raise(gridLine);
        headDismemberVisual.Init(groundY);
        headTransform.gameObject.SetActive(false);
        hasHeadDismembered = true;
    }
    private void DismemberArm()
    {
        if (hasArmDismembered) return;

        Debug.Log("掉胳膊");


        float groundY = getGroundYEventSO.Raise(gridLine);

        Debug.Log(gridLine);
        Debug.Log(groundY);

        outerArmDismemberVisual.Init(groundY);
        outerArmTransform.gameObject.SetActive(false);
        hasArmDismembered = true;
    }
}
