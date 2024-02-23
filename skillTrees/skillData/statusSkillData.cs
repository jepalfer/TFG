using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class statusSkillData : skillData
{
    [SerializeField] private float _penetrationDamage;
    [SerializeField] private float _bleedingDamage;
    [SerializeField] private float _critDamage;
    [SerializeField] private float _baseDamage;

    public float getPenetrationDamage()
    {
        return _penetrationDamage;
    }

    public float getBleedingDamage()
    {
        return _bleedingDamage;
    }

    public float getCritDamage()
    {
        return _critDamage;
    }

    public float getBaseDamage()
    {
        return _baseDamage;
    }
}
