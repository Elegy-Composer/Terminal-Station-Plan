using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is just for temporary use(objective item check), so it may not be the final implementation of backpack system
public class TempBackpack : MonoBehaviour
{
    private List<int> _itemsIDList = new List<int>();
    public List<int> ItemsIDList 
    {
        get 
        {
            return _itemsIDList;
        } 
    }

    public void AddItem(int id)
    {
        if (!_itemsIDList.Contains(id))
        {
            _itemsIDList.Add(id);
        }
    }
}
