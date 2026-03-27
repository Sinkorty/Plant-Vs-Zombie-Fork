using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlantVisual))]
public class DynamicAnimationReplacement : MonoBehaviour
{
    [Serializable]
    public struct ReplaceParams
    {
        public string originName;
        public AnimationClip targetAnimation;
    }
    [SerializeField] private List<ReplaceParams> replaceParamList;

    private Animator animator;
    private AnimatorOverrideController overrideController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        foreach (var param in replaceParamList)
        {
            overrideController[param.originName] = param.targetAnimation;
        }
        animator.runtimeAnimatorController = overrideController;
        //animator.Rebind();

        DebugInfo();
    }
    private void DebugInfo()
    {
        var clips = overrideController.overridesCount;
        var overrideClips = new System.Collections.Generic.List<KeyValuePair<AnimationClip, AnimationClip>>();
        overrideController.GetOverrides(overrideClips);

        foreach (var kvp in overrideClips)
        {
            Debug.Log($"¿É¸²¸Ç¶¯»­: {kvp.Key.name}");
        }
    }
}
