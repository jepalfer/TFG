using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class lifeStealSkill : skill, ISkill
{
    [SerializeField] private lifeStealSkillData _skillData;

    private void Awake()
    {
        _skillValues = new Dictionary<skillValuesEnum, float>();
        _skillValues.Add(skillValuesEnum.lifeSteal, _skillData.getLifeSteal());
    }
    public override skillData getData()
    {
        return _skillData;
    }

    public skill getSkill()
    {
        return this;
    }
}
