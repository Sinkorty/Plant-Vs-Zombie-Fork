using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GridMapGhostVisual : MonoBehaviour
{
    private Transform currentGhost; // 核心，需要ghost显示的游戏物体

    //private void Awake()
    //{
    //    if (Instance != null)
    //    {
    //        Debug.Log("Multiple instance err!");
    //        return;
    //    }
    //    Instance = this;
    //}

    private void Start()
    {
        GameModel gameModel = GameManager.Instance.GetGameModel();
        gameModel.OnSelectedPlantChanged += GameModel_OnSelectedPlantChanged;
    }

    private void GameModel_OnSelectedPlantChanged(object sender, GameModel.OnSelectedPlantChangedEventArgs e)
    {
        if (e.after != null) // 开始显示
        {
            Show(e.after);
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        Vector2 mousePositionOverworld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePositionOverworld;
    }

    private Transform MakeGhostFromPlantSO(PlantSO plantSO)
    {
        Transform ghostTransform = Instantiate(plantSO.visualPrefab);
        ghostTransform.GetComponent<Animator>().enabled = false;
        ghostTransform.transform.position = transform.position; // TODO: 晚点要改成对应种类的植物对应的种植锚点
        foreach (var renderer in ghostTransform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            float targetAlpha = 0.5f;
            color.a = targetAlpha;
            renderer.color = color;
            renderer.sortingLayerName = SortingLayerConstant.GRID_CELL_GHOST;
        }
        return ghostTransform;
    }

    private void Show(PlantSO plantSO)
    {
        // 销毁原来的Ghost物体，替换为新的
        Transform currentGhost = MakeGhostFromPlantSO(plantSO);
        if (this.currentGhost != null)
        {
            Destroy(this.currentGhost.gameObject);
        }
        this.currentGhost = currentGhost;
        this.currentGhost.gameObject.SetActive(true);
    }
    private void Hide()
    {
        currentGhost.gameObject.SetActive(false);
    }
}
