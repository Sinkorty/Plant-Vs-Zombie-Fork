using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameModel gameModel;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple Instance err!");
            return;
        }
        Instance = this;
        gameModel = new GameModel();
    }

    public GameModel GetModel() => gameModel;
}
