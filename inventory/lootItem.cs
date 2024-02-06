using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class lootItem
{
    //[SerializeField] private generalItem _item;
    [SerializeField] private generalItemSerializable _serializableItem;
    [SerializeField] private int _quantity;

    public lootItem(item item, int quantity)
    {
        _serializableItem = new generalItemSerializable(item);
        _quantity = quantity;
    }

    public void setQuantity(int value)
    {
        _quantity = value;
    }

    public itemTypeEnum getTipo()
    {
        return _serializableItem.getTipo();
    }

    public int getID()
    {
        return _serializableItem.getID();
    }

    public Sprite getIcon()
    {
        return _serializableItem.getIcon();
    }

    public Sprite getRender()
    {
        return _serializableItem.getRender();
    }

    public string getName()
    {
        return _serializableItem.getName();
    }

    public string getDesc()
    {
        return _serializableItem.getDesc();
    }

    public itemData getItemData()
    {
        return _serializableItem.getItemData();
    }

    public item getInstance()
    {
        return _serializableItem.getInstance();
    }

    public generalItemSerializable getItem()
    {
        return _serializableItem;
    }
    public int getQuantity()
    {
        return _quantity;
    }
}
