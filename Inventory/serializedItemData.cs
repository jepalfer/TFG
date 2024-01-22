using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class serializedItemData
{
    [SerializeField] private itemData _data;
    [SerializeField] private int _quantity;

    public serializedItemData(itemData data, int quantity)
    {
        _data = data;
        _quantity = quantity;
    }

    public itemData getData()
    {
        return _data;
    }

    public int getQuantity()
    {
        return _quantity;
    }

    public void setData(itemData data)
    {
        _data = data;
    }

    public void setQuantity(int quantity)
    {
        _quantity = quantity;
    }
}
