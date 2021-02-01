using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager current; // Singleton reference to GameManager

    HashSet<BreakableMechanic> operationalMechanics;    // Set of mechanics which are operational

    void Awake()
    {
        // Avoid mutiple instances of GameManager
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;

        operationalMechanics = new HashSet<BreakableMechanic>();

        // Persist this object between scenes reloads
        DontDestroyOnLoad(gameObject);
    }

    public static void SetMechanicOperational(BreakableMechanic mechanic)
    {
        current?.operationalMechanics.Add(mechanic);
    }

    public static void SetMechanicBroken(BreakableMechanic mechanic)
    {
        current?.operationalMechanics.Remove(mechanic);
    }

    public static bool IsMechanicOperational(BreakableMechanic mechanic)
    {
        if (current == null)
            return true;
        return current.operationalMechanics.Contains(mechanic);
    }
}
