using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectionSO", menuName = "ScriptableObjects/Utility/CollectionSO", order = 1)]
public class CollectionSO : ScriptableObject
{
    public List<CollectableSO> collectables;

    public CollectableSO selectedCollectable;


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

    public void NextCollectable()
    {
        int index = collectables.IndexOf(selectedCollectable);

        if(index == collectables.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        SetSelectedCollectable(GetCollectable(index));

    }

    public void SetSelectedCollectable(CollectableSO collectable)
    {
        for(int i = 0; i < collectables.Count; i++)
        {
            if(collectables[i] == collectable)
            {
                selectedCollectable = collectable;
                OnSelectedChanged(collectable);
            }
        }
    }

    public delegate void OnSelectedChangedDelegate(CollectableSO selectedCollectable);
    public event OnSelectedChangedDelegate OnSelectedChanged;
}
