using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    Fuse,
}

[RequireComponent(typeof(BoxCollider2D))]
public class Collectable : MonoBehaviour
{
    public CollectableType type;

    private void Reset()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Inventory>(out var invetory))
        {
            invetory.AddItem(type);
            Destroy(gameObject);
        }
    }
}
