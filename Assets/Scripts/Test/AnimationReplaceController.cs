using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;

public class AnimationReplaceController : MonoBehaviour
{
    [SerializeField] private string originalClipName;
    [SerializeField] private AnimationClip newClip;

    //[SerializeField] private MySerializedDictionary m_Dictionary;
    private Animator animator;
    private AnimatorOverrideController overrideController;

    void Start()
    {
        animator = GetComponent<Animator>();

        // 创建 OverrideController 基于原始 Controller
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ReplaceAnimation();
        }
    }

    // 替换特定动画
    public void ReplaceAnimation()
    {
        overrideController[originalClipName] = newClip;
    }

    // 批量替换
    public void ReplaceAnimations(Dictionary<string, AnimationClip> animationMap)
    {
        foreach (var kvp in animationMap)
        {
            overrideController[kvp.Key] = kvp.Value;
        }
    }
}