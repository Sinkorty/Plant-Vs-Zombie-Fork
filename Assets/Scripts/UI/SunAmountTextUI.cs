using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunAmountTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text.text = "0";
        GameManager.Instance.GetGameModel().OnSunAmountChanged += GameModel_OnSunAmountChanged;
    }

    private void GameModel_OnSunAmountChanged()
    {
        text.text = GameManager.Instance.GetGameModel().SunAmount.ToString();
    }
}
