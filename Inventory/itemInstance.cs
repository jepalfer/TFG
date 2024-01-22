using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemInstance
{
    [SerializeField] private item _data;
   
    public itemInstance(item data)
    {
        _data = data;
    }

    public item getData()
    {
        return _data;
    }
}
