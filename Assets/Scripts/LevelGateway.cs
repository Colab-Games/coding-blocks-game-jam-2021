using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGateway : MonoBehaviour
{
    public string sceneName;

    public LevelTransition animator;

    public string animationTrigger;
    public float transitionTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);

            StartCoroutine(AnimateTransition(animationTrigger, transitionTime));
        }

    }

    IEnumerator AnimateTransition(string animatorTrigger, float transitionTime)
    {
        // Play animation
        animator.transition.SetTrigger(animatorTrigger);

        // Wait
        yield return new WaitForSeconds(transitionTime);
    }
}
