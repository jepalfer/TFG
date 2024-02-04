using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class shopItem
{
    [SerializeField] private GameObject _item;
    [SerializeField] private int _quantity;
    [SerializeField] private int _price;

    public GameObject getItem()
    {
        return _item;
    }

    public int getQuantity()
    {
        return _quantity;
    }

    public int getPrice()
    {
        return _price;
    }


    public void setQuantity(int quantity)
    {
        _quantity = quantity;
    }

    public void buyItem()
    {
        _quantity -= 1;
    }

}
