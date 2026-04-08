#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class VisibleMeasure : EditorWindow
{
    [MenuItem("Tools/测量Y差值 &y")]  // Alt+Y
    private static void MeasureYDifference()
    {
        var selected = Selection.gameObjects;

        if (selected.Length != 2)
        {
            Debug.LogWarning("请选中两个物体");
            return;
        }

        float y1 = selected[0].transform.position.y;
        float y2 = selected[1].transform.position.y;
        float diff = y2 - y1;

        Debug.Log($"━━━━━━━━━━━━━━━━━━━━━");
        Debug.Log($"?? {selected[0].name}: Y = {y1:F2}");
        Debug.Log($"?? {selected[1].name}: Y = {y2:F2}");
        Debug.Log($"?? Y差值: {diff:F2} ({(diff > 0 ? "↑" : "↓")})");
        Debug.Log($"━━━━━━━━━━━━━━━━━━━━━");
    }
}
#endif