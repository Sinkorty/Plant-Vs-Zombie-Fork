#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class CaptureToAsset : EditorWindow
{
    private GameObject targetObject;
    private int width = 512;
    private int height = 512;
    private bool transparentBg = true;

    [MenuItem("Tools/Capture GameObject to Sprite Asset")]
    static void OpenWindow()
    {
        GetWindow<CaptureToAsset>("对象捕获工具");
    }

    void OnGUI()
    {
        GUILayout.Label("捕获设置", EditorStyles.boldLabel);

        targetObject = (GameObject)EditorGUILayout.ObjectField("目标对象", targetObject, typeof(GameObject), true);
        width = EditorGUILayout.IntField("宽度", width);
        height = EditorGUILayout.IntField("高度", height);
        transparentBg = EditorGUILayout.Toggle("透明背景", transparentBg);

        GUI.enabled = targetObject != null;

        if (GUILayout.Button("捕获并创建Sprite"))
        {
            CaptureAndCreateSprite();
        }

        if (GUILayout.Button("保存为PNG文件"))
        {
            SaveAsPNG();
        }

        GUI.enabled = true;
    }

    void CaptureAndCreateSprite()
    {
        Texture2D texture = CaptureTexture();
        if (texture == null) return;

        // 保存到Assets
        string path = EditorUtility.SaveFilePanel("保存Sprite", "Assets", "captured_sprite.png", "png");
        if (!string.IsNullOrEmpty(path))
        {
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(path, bytes);
            AssetDatabase.Refresh();

            // 导入为Sprite
            string relativePath = "Assets/" + Path.GetFileName(path);
            TextureImporter importer = AssetImporter.GetAtPath(relativePath) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteImportMode = SpriteImportMode.Single;
                importer.SaveAndReimport();
            }

            EditorUtility.DisplayDialog("完成", $"Sprite已保存到: {relativePath}", "确定");
        }

        DestroyImmediate(texture);
    }

    Texture2D CaptureTexture()
    {
        if (targetObject == null) return null;

        // 创建临时相机
        GameObject camObj = new GameObject("CaptureCam");
        Camera cam = camObj.AddComponent<Camera>();
        cam.clearFlags = transparentBg ? CameraClearFlags.SolidColor : CameraClearFlags.Skybox;
        cam.backgroundColor = transparentBg ? Color.clear : Color.white;
        cam.orthographic = true;

        // 计算边界
        Bounds bounds = GetBounds(targetObject);
        cam.orthographicSize = Mathf.Max(bounds.extents.x, bounds.extents.y) * 1.1f;
        cam.transform.position = new Vector3(bounds.center.x, bounds.center.y, -10);

        // 创建RenderTexture
        RenderTexture rt = new RenderTexture(width, height, 24);
        cam.targetTexture = rt;
        cam.Render();

        // 读取像素
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        RenderTexture.active = rt;
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();

        // 清理
        DestroyImmediate(camObj);
        RenderTexture.active = null;
        //cam.targetTexture = null;
        rt.Release();

        return texture;
    }

    Bounds GetBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Bounds(obj.transform.position, Vector3.one);

        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }
        return bounds;
    }

    void SaveAsPNG()
    {
        Texture2D texture = CaptureTexture();
        if (texture == null) return;

        string path = EditorUtility.SaveFilePanel("保存PNG", "", "capture.png", "png");
        if (!string.IsNullOrEmpty(path))
        {
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(path, bytes);
            EditorUtility.DisplayDialog("完成", $"图片已保存到: {path}", "确定");
        }

        DestroyImmediate(texture);
    }
}
#endif