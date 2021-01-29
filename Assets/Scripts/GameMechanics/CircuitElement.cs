using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CircuitElement : MonoBehaviour, IInteractable
{
    public bool isOperational;
    public CollectableType circuitType;
    public BreakableMechanic mechanic;

    public Sprite brokenSprite;
    public Sprite operationalSprite;
    public Sprite disabledSprite;

    SpriteRenderer spriteRenderer;
    MechanicCircuit circuit;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isOperational)
            spriteRenderer.sprite = operationalSprite;
        else
            spriteRenderer.sprite = brokenSprite;

        circuit = MechanicCircuit.GetInstance(mechanic);
        circuit.elements.Add(this);
        circuit.CheckCircuit();
    }

    public bool CanInteract(GameObject other)
    {
        var inventory = other.GetComponent<Inventory>();
        // If there is no inventory there is no where to take or put the item to fix
        if (inventory == null)
            return false;

        // Is is operational, it can be disabled
        if (isOperational)
            return true;

        // Else, it can only be enabled with the item to fix
        return inventory.Contains(circuitType);
    }

    public void Interact(GameObject other)
    {
        // We assume the CanInteract has already been checked

        var inventory = other.GetComponent<Inventory>();

        // Reverse the operationality and move the item between the circuit and the invetory
        if (isOperational)
        {
            isOperational = false;
            inventory.AddItem(circuitType);
            spriteRenderer.sprite = disabledSprite;
        }
        else
        {
            isOperational = true;
            inventory.RemoveItem(circuitType);
            spriteRenderer.sprite = operationalSprite;
        }
        circuit.CheckCircuit();
    }
}
