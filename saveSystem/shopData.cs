using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class shopData
{
    [SerializeField] private List<sceneShopData> _shopData;

    public shopData()
    {
        _shopData = new List<sceneShopData>();
    }

    public shopData(List<sceneShopData> data)
    {
        _shopData = data;
    }

    public void addData(sceneShopData data)
    {
        _shopData.Add(data);
    }

    public int getShopID(int sceneID)
    {
        return _shopData.Find(shop => shop.getSceneID() == sceneID).getShopID();
    }

    public List<sceneShopData> getData()
    {
        return _shopData;
    }
}
