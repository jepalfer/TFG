using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sceneShopData
{
    [SerializeField] private int _shopID;
    [SerializeField] private int _sceneID;
    [SerializeField] private List<int> _itemsInShop;

    public sceneShopData()
    {
        _itemsInShop = new List<int>();
    }


    public void addItem(int quantity, int shopID, int sceneID)
    {
        _shopID = shopID;
        _sceneID = sceneID;
        _itemsInShop.Add(quantity);
    }

    public void buyItem(int ID)
    {
        _itemsInShop[ID] = _itemsInShop[ID] - 1;
        Debug.Log(_itemsInShop[ID]);
    }

    public List<int> getItemsInShop()
    {
        return _itemsInShop;
    }

    public int getShopID()
    {
        return _shopID;
    }

    public int getSceneID()
    {
        return _sceneID;
    }
}
