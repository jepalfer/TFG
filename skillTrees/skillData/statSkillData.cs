using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class statSkillData : skillData
{
    [SerializeField] private int _strength;
    [SerializeField] private int _dexterity;
    [SerializeField] private int _precision;

    public int getStrength()
    {
        return _strength;
    }

    public int getDexterity()
    {
        return _dexterity;
    }

    public int getPrecision()
    {
        return _precision;
    }
}
