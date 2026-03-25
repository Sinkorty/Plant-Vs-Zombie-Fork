using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostVisual : MonoBehaviour
{
    private Transform currentGhost; // 核心，需要ghost显示的游戏物体

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
        Transform currentGhost = Instantiate(plantSO.visualPrefab);
        currentGhost.GetComponent<Animator>().enabled = false;
        currentGhost.SetParent(transform);
        currentGhost.transform.localPosition = Vector3.zero;
        foreach (var renderer in currentGhost.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.sortingLayerName = SortingLayerConstant.GHOST;
        }
        return currentGhost;
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
