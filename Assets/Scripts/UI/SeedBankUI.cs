using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBankUI : MonoBehaviour
{
    [SerializeField] private Transform seedPacketUITemplate;
    [SerializeField] private float xSpacing = 56.3f;

    private List<SeedPacketUI> seedPacketUIList;

    private void Awake()
    {
        seedPacketUIList = new List<SeedPacketUI>();
        seedPacketUITemplate.gameObject.SetActive(false);
    }

    public void UpdateVisual()
    {
        // Destroy previous objects
        foreach (var item in transform.GetComponentsInChildren<SeedPacketUI>())
        {
            if (item.transform == seedPacketUITemplate) break;
            Destroy(item.gameObject);
        }


        PlantSO[] plantSOArray = GameManager.Instance.GetModel().GetPlantSOArrayFromSeedBank();

        int bankId = 0;
        foreach (var plantSO in plantSOArray)
        {
            // Generate GameObject
            RectTransform seedPacketUITransform = Instantiate(seedPacketUITemplate).GetComponent<RectTransform>();
            seedPacketUITransform.SetParent(transform);
            seedPacketUITransform.position = seedPacketUITemplate.position;
            float xOffset = xSpacing * bankId;
            seedPacketUITransform.localPosition += Vector3.right * xOffset;
            seedPacketUITransform.gameObject.SetActive(true);

            // Config UI settings
            SeedPacketUI seedPacketUI = seedPacketUITransform.GetComponent<SeedPacketUI>();
            seedPacketUI.Init(bankId);

            seedPacketUIList.Add(seedPacketUI);
            bankId++;
        }
    }
}
