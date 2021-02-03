using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Lever : MonoBehaviour, IInteractable
{
    public bool isActive;
    public Activatable[] activatables;
    public float animationTime = 1f;
    public Sprite[] sprites;

    bool isAnimating;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isActive)
            spriteRenderer.sprite = sprites[sprites.Length - 1];
        else
            spriteRenderer.sprite = sprites[0];
        UpdateActivatables();
    }

    public bool CanInteract(GameObject other)
    {
        if (isAnimating)
            return false;
        return GameManager.IsMechanicOperational(BreakableMechanic.Lever);
    }

    public void Interact(GameObject other)
    {
        void callback()
        {
            isActive = !isActive;
            UpdateActivatables();
        }
        Animate(callback);
    }

    void UpdateActivatables()
    {
        foreach (var activatable in activatables)
            activatable.SetActiveState(isActive);
    }

    void Animate(UnityAction callback)
    {
        isAnimating = true;
        StartCoroutine(AnimationTimer(callback));
    }

    IEnumerator AnimationTimer(UnityAction callback)
    {
        int currentSpriteIndex;
        for (int i = 1; i < sprites.Length; i++)
        {
            // Active means the index should decrease
            currentSpriteIndex = isActive ? (sprites.Length - 1) - i : i;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            yield return new WaitForSeconds(animationTime / (sprites.Length - 1));
        }
        callback?.Invoke();
        isAnimating = false;
    }
}
