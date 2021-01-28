using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicCircuit : MonoBehaviour
{
    public BreakableMechanic mechanic;
    public List<CircuitElement> elements;

    static Dictionary<BreakableMechanic, MechanicCircuit> mechanicCircuits = new Dictionary<BreakableMechanic, MechanicCircuit>();

    public static MechanicCircuit GetInstance(BreakableMechanic mechanic)
    {
        if (!mechanicCircuits.ContainsKey(mechanic))
        {
            var mechanicCircuitObj = new GameObject(mechanic.ToString() + "Circuit");
            var mechanicCircuit = mechanicCircuitObj.AddComponent<MechanicCircuit>();
            mechanicCircuit.mechanic = mechanic;
            DontDestroyOnLoad(mechanicCircuitObj);
            mechanicCircuits[mechanic] = mechanicCircuit;
        }

        return mechanicCircuits[mechanic];
    }

    void Awake()
    {
        elements = new List<CircuitElement>();
    }

    public void CheckCircuit()
    {
        // Check if all elements are operational, if so the mechanic can be enabled
        foreach (var element in elements)
        {
            if (!element.isOperational)
            {
                GameManager.SetMechanicBroken(mechanic);
                return;
            }
        }
        GameManager.SetMechanicOperational(mechanic);
    }
}
