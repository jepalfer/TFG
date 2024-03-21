using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class soulSkillData : skillData
{
    [SerializeField] private float _soulIncrease;

    public float getSoulIncrease()
    {
        return _soulIncrease;
    }
}
