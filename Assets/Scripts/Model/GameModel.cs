using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    // === Selected Plant === 
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

    // =======================

    public PlantGridMapModel PlantGridMapModel { get; private set; }

    public GameModel()
    {
        PlantGridMapModel = new PlantGridMapModel();
    }
}
