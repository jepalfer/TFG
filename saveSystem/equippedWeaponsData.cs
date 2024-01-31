using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class equippedWeaponsData
{
    [SerializeField] private int _primaryIndex = -1;
    [SerializeField] private int _secundaryIndex = -1;
    [SerializeField] private List<int> _weaponLevels;

    public equippedWeaponsData(int primary, int secundary, List<int> levels)
    {
        _primaryIndex = primary;
        _secundaryIndex = secundary;
        _weaponLevels = levels;
    }

    public int getPrimaryIndex()
    {
        return _primaryIndex;
    }

    public int getSecundaryIndex()
    {
        return _secundaryIndex;
    }

    public List<int> getWeaponsLevels()
    {
        return _weaponLevels;
    }

    public void setPrimaryIndex(int index)
    {
        _primaryIndex = index;
    }

    public void setSecundaryIndex(int index)
    {
        _secundaryIndex = index;
    }

}
