using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    // Selected Plant
    private PlantSO selectedPlant;
    public PlantSO SelectedPlant
    {
        get => selectedPlant;
        set
        {
            OnSelectedPlantChanged?.Invoke(this, new OnSelectedPlantChangedEventArgs { before = selectedPlant, after = value });
            selectedPlant = value;
        }
    }
    public event EventHandler<OnSelectedPlantChangedEventArgs> OnSelectedPlantChanged;
    public class OnSelectedPlantChangedEventArgs : EventArgs
    {
        public PlantSO before;
        public PlantSO after;
    }
    public bool HasSelectedPlant() => selectedPlant != null;


    // Sun Amount
    private int sunAmount;
    public int SunAmount
    {
        get => sunAmount;
        set
        {
            sunAmount = Mathf.Clamp(value, 0, 9999);
            OnSunAmountChanged?.Invoke();
        }
    }
    public void IncreaseSunAmountBy25() => SunAmount += 25;
    public void IncreaseSunAmountBy15() => SunAmount += 15;
    public event Action OnSunAmountChanged;



    // Other Models

    public PlantGridMapModel PlantGridMapModel { get; private set; }

    public GameModel()
    {
        PlantGridMapModel = new PlantGridMapModel();
    }
}
