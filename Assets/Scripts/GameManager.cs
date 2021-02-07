using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager current; // Singleton reference to GameManager

    [SerializeField] List<BreakableMechanic> operationalMechanics = new List<BreakableMechanic>();  // Set of mechanics which are operational

    void Awake()
    {
        // Avoid mutiple instances of GameManager
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;

        // Persist this object between scenes reloads
        DontDestroyOnLoad(gameObject);
    }

    public static void SetMechanicOperational(BreakableMechanic mechanic)
    {
        current?.SetMechanicOperationalForInstance(mechanic);
    }

    public static void SetMechanicBroken(BreakableMechanic mechanic)
    {
        current?.SetMechanicOperationalForInstance(mechanic);
    }

    public static bool IsMechanicOperational(BreakableMechanic mechanic)
    {
        if (current == null)
            return false;
        return current.IsMechanicOperationalForInstance(mechanic);
    }

    public void SetMechanicOperationalForInstance(BreakableMechanic mechanic)
    {
        if (!IsMechanicOperationalForInstance(mechanic))
            operationalMechanics.Add(mechanic);
    }

    public void SetMechanicBrokenForInstance(BreakableMechanic mechanic)
    {
        while (IsMechanicOperationalForInstance(mechanic))
            operationalMechanics.Remove(mechanic);
    }

    public bool IsMechanicOperationalForInstance(BreakableMechanic mechanic)
    {
        return operationalMechanics.Contains(mechanic);
    }
}
