using UnityEngine;

public class ParabolicLerp : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float height = 2f;      // 抛物线高度
    public float duration = 1f;    // 运动时间

    private float elapsedTime = 0f;

    void Update()
    {
        if (elapsedTime < duration)
        {
            // 1. 计算线性插值因子 (0 到 1)
            float t = elapsedTime / duration;

            // 2. 修改 t 使其形成抛物线效果（例如：4 * t * (1 - t)）
            //   这个公式会让物体先快速上升，再缓慢下降，模拟重力感。
            float parabolicT = 4 * t * (1 - t);

            // 3. 计算水平位置（直线移动）
            Vector3 horizontalPos = Vector3.Lerp(startPoint.position, endPoint.position, t);

            // 4. 叠加垂直偏移（Y轴高度）
            float yOffset = parabolicT * height;
            Vector3 finalPos = new Vector3(horizontalPos.x, horizontalPos.y + yOffset, horizontalPos.z);

            transform.position = finalPos;
            elapsedTime += Time.deltaTime;
        }
        else
        {
            transform.position = endPoint.position;
            enabled = false; // 运动结束，禁用脚本
        }
    }
}