using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunGenerationManager : MonoBehaviour
{
    public static SunGenerationManager Instance { get; private set; }

    [Header("Scene Ref")]
    [SerializeField] private Transform topLeftPoint;
    [SerializeField] private Transform bottomRightPoint;
    [Header("Resource Ref")]
    [SerializeField] private Transform sunPrefab;
    [Header("Basic Params")]
    [SerializeField] private float sunGenerateShortTimerMax = 10;
    [SerializeField] private float sunGenerateLongTimerMax = 20;
    [SerializeField] private float sunFallingDownSpeedMin = 0.03f;
    [SerializeField] private float sunFallingDownSpeedMax = 0.06f;
    [SerializeField] private float sunFallingDownTimeMin = 5;
    [SerializeField] private float sunFallingDownTimeMax = 8;

    [SerializeField] private bool isAllowed = true;

    private float sunGenerateTimer = 0;
    private float currentSunGenerateTimerMax = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance Not null");
        }
        Instance = this;

        currentSunGenerateTimerMax = Random.Range(sunGenerateShortTimerMax, sunGenerateLongTimerMax);
    }
    private void Update()
    {
        sunGenerateTimer += Time.deltaTime;
        if (sunGenerateTimer >= currentSunGenerateTimerMax)
        {
            GenerateSun();
            currentSunGenerateTimerMax = Random.Range(sunGenerateShortTimerMax, sunGenerateLongTimerMax);
            sunGenerateTimer = 0;
        }
    }
    private void GenerateSun()
    {
        //Debug.Log("Generate Sun, current timer max:" + currentSunGenerateTimerMax);
        Transform sunTransfrom = Instantiate(sunPrefab);

        Vector3 posToSpawn = new Vector3();
        posToSpawn.x = Random.Range(topLeftPoint.position.x, bottomRightPoint.position.x);
        posToSpawn.y = Random.Range(bottomRightPoint.position.y, topLeftPoint.position.y);
        posToSpawn.z = 0f;

        Sun sun = sunTransfrom.GetComponent<Sun>();
        sun.transform.position = posToSpawn;

        float fallingTime = Random.Range(sunFallingDownTimeMin, sunFallingDownTimeMax);
        float fallingSpeed = Random.Range(sunFallingDownSpeedMin, sunFallingDownSpeedMax);
        sun.StartFallDown(fallingTime, fallingSpeed);
    }
}
