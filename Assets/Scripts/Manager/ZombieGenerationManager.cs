using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerationManager : MonoBehaviour
{
    [SerializeField] private Transform zombiePrefab;
    [Header("Scene References")]
    [SerializeField] private List<Transform> spawnPointList; // [0]对应的是第一行

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateZombie(Random.Range(1, 6));
        }
    }

    private void GenerateZombie(int gridLine)
    {
        Transform zombieInstance = Instantiate(zombiePrefab);
        zombieInstance.transform.position = spawnPointList[gridLine - 1].position;
        IZombieController zombieController = zombieInstance.GetComponent<IZombieController>();
        zombieController.Init(gridLine);
    }
}
