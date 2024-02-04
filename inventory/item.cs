using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class item : ScriptableObject
{
    [SerializeField] private itemData _itemData;

    public itemData getItemData()
    {
        return _itemData;
    }

    public void setItemData(itemData data)
    {
        _itemData = data;
    }

}
