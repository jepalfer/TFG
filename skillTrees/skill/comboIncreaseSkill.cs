using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class comboIncreaseSkill : skill, ISkill
{
    [SerializeField] private comboSkillData _data;

    public override skillData getData()
    {
        return _data;
    }

    public skill getSkill()
    {
        return this;
    }
}
