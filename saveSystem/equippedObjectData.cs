using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class equippedObjectData
{
    [SerializeField] private List<newEquippedObjectData> _data;

    public List<newEquippedObjectData> getData()
    {
        return _data;
    }

    public void setEquippedObject(int index, newEquippedObjectData obj)
    {
        _data[index] = obj;
    }

    public equippedObjectData(List<newEquippedObjectData> data)
    {
        _data = data;
    }
}
