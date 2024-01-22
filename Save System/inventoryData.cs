using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class inventoryData
{
    [SerializeField] private List<serializedItemData> _inventory;
    [SerializeField] private List<serializedItemData> _backUp;
    public inventoryData(List<lootItem> inventory, List<lootItem> backUp)
    {
        _inventory = new List<serializedItemData>();
        _backUp = new List<serializedItemData>();

        for (int i = 0; i < inventory.Count; ++i)
        {
            _inventory.Add(new serializedItemData(inventory[i].getItem().getInstance().getItemData(), inventory[i].getQuantity()));
        }
        for (int i = 0; i < backUp.Count; ++i)
        {
            _backUp.Add(new serializedItemData(backUp[i].getItem().getInstance().getItemData(), backUp[i].getQuantity()));
        }

    }

    public List<serializedItemData> getInventory()
    {
        return _inventory;
    }
    public List<serializedItemData> getBackup()
    {
        return _backUp;
    }

}