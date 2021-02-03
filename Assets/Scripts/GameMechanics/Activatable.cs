using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
    public bool isActive = true;
    public bool invertActivation;

    public void SetActiveState(bool state)
    {
        this.isActive = invertActivation ? !state : state;
    }
}
