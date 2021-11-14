using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Vector3Data", menuName = "ScriptableObjects/Data/Vector3Data", order = 1)]
public class Vector3Data : ScriptableObject
{
    public Vector3 value;

    public void SetValue(Vector3 newValue)
    {
        value = newValue;
    }
}
