using UnityEngine;

public class GridCellController : MonoBehaviour
{
    [SerializeField] private Vector2Int gridPosition;
    private GameModel gameModel;
    private GridCellModel model;

    // ????????
    private void OnMouseEnter()
    {
        if (!model.HasPlant() && gameModel.HasSelectedPlant())
        {
            GridMapGhostVisual.Instance.Show(gameModel.SelectedPlant, this);
        }
    }
    private void OnMouseExit()
    {
        if (!model.HasPlant() && gameModel.HasSelectedPlant())
        {
            GridMapGhostVisual.Instance.Hide();
        }
    }
    private void Start()
    {
        model = new GridCellModel(gridPosition);
        gameModel = GameManager.Instance.GetModel();
    }
    private void OnMouseDown()
    {
        if (gameModel.HasSelectedPlant() && !model.HasPlant())
        {
            SpawnPlant();
        }
    }
    //private void OnMouseOver()
    //{
    //    // TODO: ??????ж??????????????????????????
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        SpawnPlant();
    //    }
    //}
    private void SpawnPlant()
    {
        // ????????????????
        if (model.HasPlant())
        {
            return;
        }
        model.PlantSO = gameModel.SelectedPlant;
        Transform plantTransform = Instantiate(gameModel.SelectedPlant.prefab);
        plantTransform.position = transform.position;

        plantTransform.GetComponent<IPlantController>().SetGridCell(this); // fix


        // ????????
        gameModel.SunAmount -= model.PlantSO.sunCost;
        // ????????????????????????? SeedPacketUI ????
        GridMapController.Instance.AnyGridCellPlanted(new GridMapController.OnAnyGridCellPlantedEventArgs { plantSO = model.PlantSO });
    }

    public int GetRow() => gridPosition.y;
    public int GetLine() => gridPosition.x;
    public Vector2Int GetGridPosition() => gridPosition;
}
