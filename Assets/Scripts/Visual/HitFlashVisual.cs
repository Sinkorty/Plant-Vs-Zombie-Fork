using System;
using UnityEngine;

public class HitFlashVisual : MonoBehaviour
{
    [SerializeField] private GameObject healthModelHolder;
    
    [SerializeField] private float flashTimerMax = .5f;
    [SerializeField] private float peakValue = .2f;

    private float flashTimer;
    private bool startHitFlash;

    private MaterialPropertyBlock mpb;
    private SpriteRenderer[] rendererArray;
    
    private HealthModel healthModel;

    private void Awake()
    {
        rendererArray = GetComponentsInChildren<SpriteRenderer>();
        mpb = new MaterialPropertyBlock();
    }

    private void Start()
    {
        healthModel = healthModelHolder.GetComponent<ICharacter>().GetHealthModel();
        healthModel.OnHealthChanged += HealthModelOnOnHealthChanged;
    }

    private void OnDisable()
    {
        healthModel.OnHealthChanged -= HealthModelOnOnHealthChanged;
    }
    private void Update()
    {
        if (!startHitFlash) return;

        var x = flashTimerMax / 2f - Mathf.Abs(flashTimerMax / 2f - flashTimer);
        var v = peakValue / (flashTimerMax / 2f) * x;

        // ÉÁ°×²Ù×÷
        SetFlashAmount(v);

        flashTimer += Time.deltaTime;
        if (flashTimer >= flashTimerMax)
        {
            flashTimer = 0f;
            startHitFlash = false;
        }
    }
    private void HealthModelOnOnHealthChanged(object sender, HealthModel.OnHealthChangedEventArgs e)
    {
        if (e.after - e.before > 0) // ¿ÛÑª£¬ÉÁ°×
        {
            PlayFlash();
        }
    }
    void SetFlashAmount(float amount)
    {
        amount = Mathf.Clamp01(amount);
        foreach (var renderer in rendererArray)
        {
            renderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_FlashAmount", amount);
            renderer.SetPropertyBlock(mpb);
        }
    }

    private void PlayFlash()
    {
        flashTimer = 0f;
        startHitFlash = true;
    }
}