using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapItem : MonoBehaviour
{
    public CharacterData itemData;
    public CollectionSO itemCollection;

    public void InitializeItem(CharacterData newData)
    {
        itemData = newData;

        GetComponentInChildren<Text>().text = itemData.name;

        itemCollection.OnSelectedChanged += UpdateSelection;

        if (itemData == itemCollection.selectedCollectable)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    void UpdateSelection(CollectableSO newSelection)
    {
        if((CharacterData)newSelection != itemData)
        {
            GetComponent<Button>().interactable = true;
        }
    }

    public void SetCurrentCollectable()
    {
        itemCollection.SetSelectedCollectable(itemData);
        GetComponent<Button>().interactable = false;
    }
}
