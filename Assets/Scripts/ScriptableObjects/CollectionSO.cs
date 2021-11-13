using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectionSO", menuName = "ScriptableObjects/Utility/CollectionSO", order = 1)]
public class CollectionSO : ScriptableObject
{
    public List<CollectableSO> collectables;


    public void AddCollectable(CollectableSO newCollectable)
    {
        collectables.Add(newCollectable);
    }

    public CollectableSO GetCollectable(int index)
    {
        if(collectables.Count > index)
        {
            return collectables[index];
        }

        return null;
    }
}
