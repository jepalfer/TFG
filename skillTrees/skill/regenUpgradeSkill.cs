using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class regenUpgradeSkill : skill, ISkill
{
    [SerializeField] private regenUpgradeSkillData _skillData;
    public override skillData getData()
    {
        return _skillData;
    }
    public skill getSkill()
    {
        return this;
    }
}
