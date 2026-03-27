using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapGhostVisual : MonoBehaviour
{
    public static GridMapGhostVisual Instance { get; private set; }

    private Transform currentGhost;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Instance");
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        GridMapController.Instance.OnAnyGridCellPlanted += GridMapManager_OnAnyGridCellPlanted;
    }

    // 如果种下了植物，则需要隐藏一次GridMapGhost
    private void GridMapManager_OnAnyGridCellPlanted(object sender, GridMapController.OnAnyGridCellPlantedEventArgs e)
    {
        Hide();
    }
    private void Update()
    {
        // 按下右键也需要隐藏，原则上右键取消选择了
        if (Input.GetMouseButtonDown(1))
        {
            Hide();
        }
    }
    private Transform MakeGhostFromPlantSO(PlantSO plantSO)
    {
        Transform ghostTransform = Instantiate(plantSO.visualPrefab);
        //ghostTransform.GetComponent<Animator>().enabled = false;
        if (ghostTransform.TryGetComponent(out Animator animator))
        {
            animator.enabled = false;
        }

        //ghostTransform.transform.position = transform.position; // TODO: 晚点要改成对应种类的植物对应的种植锚点
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

    public void Show(PlantSO plantSO, GridCellController gridCellController)
    {
        // 销毁原来的Ghost物体，替换为新的
        Transform currentGhost = MakeGhostFromPlantSO(plantSO);
        if (this.currentGhost != null)
        {
            Destroy(this.currentGhost.gameObject);
        }

        currentGhost.transform.position = gridCellController.transform.position;

        this.currentGhost = currentGhost;
        this.currentGhost.gameObject.SetActive(true);
    }
    public void Hide()
    {
        if (currentGhost == null) return;
        currentGhost.gameObject.SetActive(false);
    }
}
