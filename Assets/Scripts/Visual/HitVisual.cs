using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 订阅的 HealthModel 若血条减少 通过控制所有子物体的 SpriteRenderer 来实现受击闪烁
/// </summary>
public class HitVisual : MonoBehaviour
{
    [SerializeField] private GameObject healthModelHolder;
    private SpriteRenderer[] spriteRendererArray;

    private void Awake()
    {
        spriteRendererArray = GetComponentsInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        // 因为 HealthModel 在 Awake 的时候才完成初始化
        HealthModel healthModel = healthModelHolder.GetComponent<ICharacter>()?.GetHealthModel();
        if (healthModel == null)
        {
            Debug.LogError("Health Model Holder不是正确的引用！");
            return;
        }
        healthModel.OnHealthChanged += HealthModel_OnHealthChanged;
    }

    private void HealthModel_OnHealthChanged(object sender, HealthModel.OnHealthChangedEventArgs e)
    {
        if(e.after - e.before > 0) // 扣血
        {
            Flash();
        }
    }
    private void Flash()
    {
        //TODO: 用Shader来实现
    }
}
