using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 要用就预制体拖拽
/// </summary>
[RequireComponent(typeof(TextMeshPro))]
public class DebugHealthVisual : MonoBehaviour
{
    [SerializeField] private GameObject characterHolder;

    private TextMeshPro text;
    private HealthModel healthModel;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    private void Start()
    {
        healthModel = characterHolder.GetComponent<ICharacter>().GetHealthModel();
        healthModel.OnHealthChanged += HealthModel_OnHealthChanged;
        text.text = $"HP: {healthModel.MaxHealth}/{healthModel.MaxHealth}";
    }

    private void HealthModel_OnHealthChanged(object sender, HealthModel.OnHealthChangedEventArgs e)
    {
        text.text = $"HP: {e.after}/{healthModel.MaxHealth}";
    }
}
