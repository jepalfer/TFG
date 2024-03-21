using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulSkill : skill, ISkill
{
    [SerializeField] private soulSkillData _data;

    private void Awake()
    {
        _skillValues = new Dictionary<skillValuesEnum, float>();

        _skillValues.Add(skillValuesEnum.soulIncrease, _data.getSoulIncrease());
    }

    public override skillData getData()
    {
        return _data;
    }
    public skill getSkill()
    {
        return this;
    }
}
