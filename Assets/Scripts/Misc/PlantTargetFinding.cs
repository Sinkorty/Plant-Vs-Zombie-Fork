using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plant的寻路组件，核心就是恰当的调用IPlantController.SetTarget()
/// </summary>
[RequireComponent(typeof(IPlantController))]
public class PlantTargetFinding : MonoBehaviour
{
    private IPlantController plantController;
    [SerializeField] private float distance;
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private float checkTimerMax = 1f;
    //[SerializeField] private int checkAmountMax = 60;

    private float checkTimer;
    //private RaycastHit2D[] hitArray;

    private int layerMaskFlag;

    private void Awake()
    {
        plantController = GetComponent<IPlantController>();
        layerMaskFlag = LayerMask.GetMask(LayerMaskConstant.ZOMBIE_HIT_CHECKBOX);
        //hitArray = new RaycastHit2D[checkAmountMax];
    }

    private void Update()
    {
        checkTimer -= Time.deltaTime;
        if (checkTimer < 0)
        {
            checkTimer = checkTimerMax;

            CheckTarget();
        }
    }
    // TODO: 需要更改
    private void CheckTarget()
    {
        //int count = Physics2D.RaycastNonAlloc(transform.position, Vector2.right, hitArray, distance, layerMaskFlag);
        //if (count == 0)
        //{
        //    plantController.SetTarget(null);
        //    return;
        //}
        //for (int i = 0; i < hitArray.Length; i++)
        //{
        //    GameObject hitGameObject = hitArray[i].transform.gameObject;
        //    IZombieController zombieController = hitGameObject.GetComponentInParent<IZombieController>();
        //    plantController.SetTarget(zombieController);
        //    //Debug.Log($"hit gameobject: {hitGameObject} check result: {zombieController}");
        //    break;
        //}
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, distance, layerMaskFlag);


        if (hitInfo.collider == null)
        {
            plantController.SetTarget(null);
            return;
        }
        Transform hitTransform = hitInfo.transform;
        IZombieController zombieController = hitTransform.GetComponentInParent<IZombieController>();
        int plantGridLine = plantController.GetGridCell().GetLine();

        if (zombieController.GetGridLine() != plantGridLine)
        {
            plantController.SetTarget(null);
            return;
        }
        plantController.SetTarget(zombieController);
        //Debug.Log($"hit gameobject: {hitGameObject} check result: {zombieController}");
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * distance);
    }
}
