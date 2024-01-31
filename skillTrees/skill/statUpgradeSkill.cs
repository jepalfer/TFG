using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class statUpgradeSkill : skill, ISkill
{
    [SerializeField] private statSkillData _data;
    public override skillData getData()
    {
        return _data;
    }
    public skill getSkill()
    {
        return this;
    }
}
