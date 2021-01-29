using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public string sceneName;
    public string animatorTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
            StartCoroutine(AnimateTransition(animatorTrigger));
        }

    }

    IEnumerator AnimateTransition(string animatorTrigger)
    {
        // Play animation
        transition.SetTrigger(animatorTrigger);

        // Wait
        yield return new WaitForSeconds(transitionTime);
    }
}
