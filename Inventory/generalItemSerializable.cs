using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class generalItemSerializable
{
    [SerializeField] private item _instance;
    public generalItemSerializable(item data)
    {
        _instance = data;
    }
    public itemType getTipo()
    {
        return _instance.getItemData().getTipo();
    }

    public int getID()
    {
        return _instance.getItemData().getId();
    }

    public Sprite getIcon()
    {
        return _instance.getItemData().getIcon();
    }

    public Sprite getRender()
    {
        return _instance.getItemData().getRender();
    }

    public string getName()
    {
        return _instance.getItemData().getName();
    }

    public string getDesc()
    {
        return _instance.getItemData().getDesc();
    }

    public itemData getItemData()
    {
        return _instance.getItemData();
    }

    public item getInstance()
    {
        return _instance;
    }

}
