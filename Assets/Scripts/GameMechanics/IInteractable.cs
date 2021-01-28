using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool CanInteract(GameObject other);
    void Interact(GameObject other);
}
