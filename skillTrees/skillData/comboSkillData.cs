using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class comboSkillData : skillData
{
    [SerializeField] private int _primaryIncrease;
    [SerializeField] private int _secundaryIncrease;

    public int getPrimaryIncrease()
    {
        return _primaryIncrease;
    }
    public int getSecundaryIncrease()
    {
        return _secundaryIncrease;
    }
}
