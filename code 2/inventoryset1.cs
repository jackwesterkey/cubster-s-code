using UnityEngine;
using System.Collections.Generic;

public class inventoryset1 : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>(); // List to hold all items
    public List<GameObject> objectsToDelete = new List<GameObject>(); // The objects that can be deleted

    private Dictionary<KeyCode, GameObject> keyToItemMap = new Dictionary<KeyCode, GameObject>();
    private GameObject currentItem;

    private void Start()
    {
        InitializeKeyToItemMap();
        HideAllItems(); // Hide all items when the game starts
                        // ShowItem(KeyCode.Alpha1); // Show the first item initially
    }

    private void Update()
    {
        foreach (var kvp in keyToItemMap)
        {
            if (Input.GetKeyDown(kvp.Key))
            {
                ShowItem(kvp.Key);
            }
        }

        // Handle '0' key separately
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            HideAllItems();
        }
    }

    private void ShowItem(KeyCode key)
    {
        if (keyToItemMap.TryGetValue(key, out GameObject item))
        {
            // Hide the current item if it exists
            if (currentItem != null)
            {
                HideItem(currentItem);
            }

            // Show the new item
            currentItem = item;
            currentItem.SetActive(true);
        }
    }

    private void HideItem(GameObject item)
    {
        if (item != null)
        {
            item.SetActive(false);
        }
    }

    private void HideAllItems()
    {
        foreach (var item in items)
        {
            HideItem(item);
        }
    }

    private void InitializeKeyToItemMap()
    {
        for (int i = 0; i < Mathf.Min(items.Count, 9); i++)
        {
            keyToItemMap.Add(KeyCode.Alpha1 + i, items[i]);
        }
    }
}
