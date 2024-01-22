using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class equippedSkillData
{
    [SerializeField] private int [] _IDs = new int [3];
    [SerializeField] private skillType[] _types = new skillType[3];

    public equippedSkillData(int [] IDs, skillType[] types)
    {
        _IDs = IDs;
        _types = types;
    }

    public int [] getIDs()
    {
        return _IDs;
    }

    public skillType[] getTypes()
    {
        return _types;
    }
}
