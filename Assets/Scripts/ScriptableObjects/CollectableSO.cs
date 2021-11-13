using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableSO", menuName = "ScriptableObjects/Utility/CollectableSO", order = 1)]
public class CollectableSO : ScriptableObject
{
    public bool collected;

    void Collect()
    {
        collected = true;
    }
}
