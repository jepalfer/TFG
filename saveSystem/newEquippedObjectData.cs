using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class newEquippedObjectData
{
    [SerializeField] private int _itemID;

    public newEquippedObjectData(int itemID)
    {
        _itemID = itemID;
    }

    public int getItemID()
    {
        return _itemID;
    }
}
