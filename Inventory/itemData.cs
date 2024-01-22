using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemData
{
    [SerializeField] private itemType _tipo;
    [SerializeField] private int _itemId;
    [SerializeField] private string _icon;
    [SerializeField] private string _render;
    [SerializeField] private string _itemName;
    [TextArea]
    [SerializeField] private string _itemDesc;
    public itemType getTipo()
    {
        return _tipo;
    }
    public int getId()
    {
        return _itemId;
    }
    public Sprite getIcon()
    {
        return Resources.Load<Sprite>(_icon);
    }
    public string getName()
    {
        return _itemName;
    }
    public string getDesc()
    {
        return _itemDesc;
    }


    public Sprite getRender()
    {
        return Resources.Load<Sprite>(_render);
    }
}
