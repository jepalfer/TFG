using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class equippedObjectData
{
    [SerializeField] private List<newEquippedObjectData> _data;
    [SerializeField] private int _indexInEquipped;

    public List<newEquippedObjectData> getData()
    {
        return _data;
    }

    public int getIndexInEquipped()
    {
        return _indexInEquipped;
    }

    public void setEquippedObject(int index, newEquippedObjectData obj)
    {
        _data[index] = obj;
    }

    public equippedObjectData(List<newEquippedObjectData> data, int id)
    {
        _data = data;
        _indexInEquipped = id;
    }
}
