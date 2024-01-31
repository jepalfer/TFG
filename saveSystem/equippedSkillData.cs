using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class equippedSkillData
{
    [SerializeField] private int [] _IDs = new int [3];
    [SerializeField] private skillTypeEnum[] _types = new skillTypeEnum[3];

    public equippedSkillData(int [] IDs, skillTypeEnum[] types)
    {
        _IDs = IDs;
        _types = types;
    }

    public int [] getIDs()
    {
        return _IDs;
    }

    public skillTypeEnum[] getTypes()
    {
        return _types;
    }
}
