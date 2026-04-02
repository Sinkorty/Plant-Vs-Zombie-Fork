using System;
using UnityEngine;

public class CornPultController : MonoBehaviour, IPlantController
{
    public event EventHandler OnLaunched;

    [Header("Basic Params")]
    [SerializeField][Range(0f, 1f)] private float butterChance = 0.2f;
    [SerializeField] private float pultTimerMax;

    [Header("Scene References")]
    [SerializeField] private Transform spawnPointTransform;
    [SerializeField] private BulletSO cornKerbalBulletSO;
    [SerializeField] private BulletSO ButterBulletSO;

    [SerializeField] private PlantVisual plantVisual;

    private GridCellController gridCellController;
    private float pultTimer;
    private Transform target;


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
                if (target != null)
                {
                    OnLaunched?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
    // 生成子弹
    private void PlantVisual_OnProject(object sender, EventArgs e)
    {
        float random = UnityEngine.Random.Range(0f, 1f);

        // 概率投出黄油
        BulletSO bulletSO = random <= butterChance ? ButterBulletSO : cornKerbalBulletSO;

        PultBullet pultBullet = BulletGenerationManager.Instance.Instantiate(bulletSO) as PultBullet;

        pultBullet.Initialize(bulletSO,
            startPoint: spawnPointTransform,
            endPoint: target.transform.position,
            currentLine: gridCellController.GetLine());
    }

    public GridCellController GetGridCell() => gridCellController;

    public void SetGridCell(GridCellController gridCellController) => this.gridCellController = gridCellController;

    public void SetTarget(IZombieController zombieController)
    {
        if (zombieController == null)
        {
            target = null;
            return;
        }
        target = (zombieController as MonoBehaviour).transform;
    }
}
