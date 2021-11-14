using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapperController : MonoBehaviour
{
    public GameObject swapItemPrefab;
    public CollectionSO characterCollection;
    // Start is called before the first frame update
    private void Awake()
    {
        InitializeList();
    }

    void InitializeList()
    {
        for(int i = 0; i < characterCollection.collectables.Count; i++)
        {
            GameObject nextItem = Instantiate(swapItemPrefab, this.transform);
            nextItem.GetComponent<SwapItem>().InitializeItem((CharacterData)characterCollection.collectables[i]);
        }
    }

    void UpdateList()
    {

    }
}
