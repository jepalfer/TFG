using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class lifeStealSkill : skill, ISkill
{
    [SerializeField] private lifeStealSkillData _skillData;

    public override skillData getData()
    {
        return _skillData;
    }

    public skill getSkill()
    {
        return this;
    }
}
