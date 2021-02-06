using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelTransition : MonoBehaviour
{
    public Animator transition;

    public enum Trigger
    {
        Open,
        Close,
    }

    public IEnumerator AnimateTransition(Trigger animatorTrigger, float animationSpeed, UnityAction callback = null)
    {
        // Play animation
        transition.SetFloat("AnimationSpeed", animationSpeed);
        transition.SetTrigger(animatorTrigger.ToString());

        // Wait
        var animationInfo = transition.GetCurrentAnimatorClipInfo(0);
        var clipLength = animationInfo[0].clip.length;
        yield return new WaitForSeconds(clipLength / animationSpeed);

        callback?.Invoke();
    }
}

