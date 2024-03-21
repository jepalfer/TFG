using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class slotData : MonoBehaviour
{
    private int _inventoryStock;
    private int _backUpStock;/*
    private Sprite _render;
    private string _description;*/

    [SerializeField] private lootItem _itemReference;
    [SerializeField] private Image _itemOverlay;

    public void setLootItem(lootItem item)
    {
        _itemReference = item;
    }

    public int getInventoryStock()
    {
        return _inventoryStock;
    }

    public lootItem getItem()
    {
        return _itemReference;
    }

    public int getBackUpStock()
    {
        return _backUpStock;
    }
    public Sprite getRender()
    {
        return _itemReference.getRender();
    }
    public string getDescription()
    {
        return _itemReference.getDesc();
    }

    public void setInventoryStock(int value)
    {
        _inventoryStock = value;
    }

    public void setBackUpStock(int value)
    {
        _backUpStock = value;
    }/*
    public void setRender(Sprite render)
    {
        _render = render;
    }
    public void setDescription(string text)
    {
        _description = text;
    }*/
    public itemTypeEnum getTipo()
    {
        return _itemReference.getTipo();
    }

    public int getID()
    {
        return _itemReference.getID();
    }

    public Sprite getIcon()
    {
        return _itemReference.getIcon();
    }
    public Image getOverlayImage()
    {
        return _itemOverlay;
    }

    public string getName()
    {
        return _itemReference.getName();
    }

    public string getDesc()
    {
        return _itemReference.getDesc();
    }

    public itemData getItemData()
    {
        return _itemReference.getItemData();
    }

    public item getInstance()
    {
        return _itemReference.getInstance();
    }

    public int getQuantity()
    {
        return _itemReference.getQuantity();
    }
}
