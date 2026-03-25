using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 负责 Ghost 显示， 不同种类植物的 Pivot 相关逻辑实现
public class GridCell : MonoBehaviour
{
    [SerializeField] private Vector2Int gridPosition;
    //[SerializeField] private PlantSO melonPultPlantSO; // 测试用

    //[SerializeField] private bool isShowingGhost = false; // 当前格子是否正在显示Ghost
    //[SerializeField] private bool isMouseOver = false; // 鼠标是否悬停在该GridCell上

    private Transform ghostTransform;
    private GameModel gameModel;

    private void Start()
    {
        gameModel = GameManager.Instance.GetGameModel();
    }
    private void OnMouseOver()
    {
        OnMouseOver_ShowGhostLogic();
        // TODO: 光这点判断还是不够的，还要知道是否在捡阳光
        if (Input.GetMouseButtonDown(0))
        {

            SpawnPlant();
        }
    }
    private void OnMouseExit()
    {
        //if (isShowingGhost)
        //{
        //    //RemoveGhost
        //    isShowingGhost = false;
        //    Destroy(ghostTransform.gameObject);
        //    ghostTransform = null;
        //}
    }
    //private void Update()
    //{
    //    if (!isMouseOver && isShowingGhost) // 鼠标离开，Ghost还在
    //    {
    //        // RemoveGhost
    //        isShowingGhost = false;
    //        Destroy(ghostTransform.gameObject);
    //        ghostTransform = null;
    //    }
    //}
    private void OnMouseOver_ShowGhostLogic()
    {
        //if (!isShowingGhost && GhostObject.Instance.HasGhost())
        //{
        //    isShowingGhost = true; // 保证只执行一次

        //    ghostTransform = Instantiate(GhostObject.Instance.GetCurrentGhostObject());
        //    ghostTransform.SetParent(transform);
        //    ghostTransform.localPosition = Vector3.zero;
        //    // 将ghost变成半透明
        //    MakeGhostFromGhostObject(ghostTransform);
        //}
    }
    private void SpawnPlant()
    {
        Transform melonPultPlantTransform = Instantiate(gameModel.SelectedPlant.prefab); // TODO: 根据seedbank选中的卡槽来生成

        melonPultPlantTransform.position = transform.position;
    }
    // 根据GhostObject的Transform来制作属于自己的Ghost
    //private void MakeGhostFromGhostObject(Transform target)
    //{
    //    foreach (var renderer in target.GetComponentsInChildren<SpriteRenderer>())
    //    {
    //        Color color = renderer.color;
    //        float targetAlpha = 0.5f;
    //        color.a = targetAlpha;
    //        renderer.color = color;
    //        renderer.sortingLayerName = SortingLayerConstant.GRID_CELL_GHOST;
    //    }
    //}

    public int GetRow() => gridPosition.x;
    public int GetLine() => gridPosition.y;
    public Vector2Int GetGridPosition() => gridPosition;
}
