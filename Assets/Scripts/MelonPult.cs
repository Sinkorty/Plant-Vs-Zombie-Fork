using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonPult : MonoBehaviour, IPlant
{
    public event EventHandler OnPulted;

    [SerializeField] private PlantSO plantSO;
    [SerializeField] private Transform melonBulletPrefab;

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private MelonPultVisual melonPultVisual;

    private Transform target;
    private float pultTimer;
    private float pultTimerMax = 3f;

    private void Start()
    {
        melonPultVisual.OnWillLaunch += MelonPultVisual_OnWillLaunch;
    }

    // 动画回调，表示即将发射Melon
    private void MelonPultVisual_OnWillLaunch(object sender, EventArgs e)
    {
        GenerateBullet();
    }

    private void Update()
    {
        if (pultTimer >= 0)
        {
            pultTimer -= Time.deltaTime;
            if (pultTimer < 0)
            {
                pultTimer = pultTimerMax;
                if (HasTarget())
                {
                    Pult();
                }
            }
        }
    }
    private void Pult()
    {
        OnPulted?.Invoke(this, EventArgs.Empty);
        Debug.Log("Pult");

    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
    public bool HasTarget() => target != null;

    public void GenerateBullet()
    {
        Transform melonBulletTransform = Instantiate(melonBulletPrefab);
        PultBullect melonBullet = melonBulletTransform.GetComponent<PultBullect>();
        melonBullet.Initialize(bulletSpawnPoint.position, target.position);
    }
}
