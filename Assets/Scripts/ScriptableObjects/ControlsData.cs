using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ControlsData", menuName = "ScriptableObjects/Utility/ControlsData", order = 1)]
public class ControlsData : ScriptableObject
{
    public InputActionMap inputs;

    public int maxJumps = 1;

    int jumpsAvailable;

    public bool canClimb;

    public bool canFly;

    public ControlsData()
    {
        jumpsAvailable = maxJumps;
    }

    public void UseJump()
    {
        jumpsAvailable--;
    }

    public bool HasMoreJumps()
    {
        if (jumpsAvailable > 0)
        {
            return true;
        }

        return false;
    }

    public bool Grounded()
    {
        if (maxJumps == jumpsAvailable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetJumps()
    {
        jumpsAvailable = maxJumps;
    }

}
