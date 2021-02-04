using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGateway : MonoBehaviour
{
    public string sceneName;

    public LevelTransition transition;

    public LevelTransition.Trigger animationTrigger;
    public float animationSpeed = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            void callback() { SceneManager.LoadScene(sceneName); }
            var animation = transition.AnimateTransition(animationTrigger, animationSpeed, callback);
            StartCoroutine(animation);
        }

    }
}
