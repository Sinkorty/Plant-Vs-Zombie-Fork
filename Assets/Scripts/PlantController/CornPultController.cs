using System;
using UnityEngine;

public class CornPultController : MonoBehaviour, IPlantController
{
    public event EventHandler OnLaunch;

    [Header("Basic Params")]
    [SerializeField][Range(0f, 1f)] private float butterChance = 0.2f;
    [SerializeField] private float pultTimerMax;

    [Header("Scene References")]
    [SerializeField] private Transform spawnPointTransform;
    [SerializeField] private BulletSO cornKerbalBulletSO;
    [SerializeField] private BulletSO ButterBulletSO;


    private GridCellController gridCellController;

    private float pultTimer;

    public GridCellController GetGridCell() => gridCellController;
    public void SetGridCell(GridCellController gridCellController) => this.gridCellController = gridCellController;

    [SerializeField] private PlantVisual plantVisual;
    //[SerializeField] private IBullet cornBullet;
    //[SerializeField] private IBullet butterBullet

    private void Start()
    {
        plantVisual.OnProject += PlantVisual_OnProject;
    }

    private void Update()
    {
        if (pultTimer >= 0)
        {
            pultTimer -= Time.deltaTime;
            if (pultTimer < 0)
            {
                pultTimer = pultTimerMax;
                //if (HasTarget())
                //{
                //    Pult();
                //}
                // 给Visual调用的，当然其他也可以用
                OnLaunch?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void PlantVisual_OnProject(object sender, EventArgs e)
    {

        float random = UnityEngine.Random.Range(0f, 1f);

        // 概率投出黄油
        BulletSO bulletSO = random <= butterChance ? ButterBulletSO : cornKerbalBulletSO;

        PultBullet pultBullet = BulletGenerationManager.Instance.Instantiate(bulletSO) as PultBullet;
        Vector2 targetEndPoint = new Vector2(6, 0);

        pultBullet.Initialize(bulletSO, spawnPointTransform, targetEndPoint, gridCellController.GetLine());
    }

    public void SetTarget()
    {
        throw new NotImplementedException();
    }
}
