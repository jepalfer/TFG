using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class comboIncreaseSkill : skill, ISkill
{
    [SerializeField] private comboSkillData _data;

    private void Awake()
    {
        _skillValues = new Dictionary<skillValuesEnum, float>();
        _skillValues.Add(skillValuesEnum.primaryAttackIncrease, _data.getPrimaryIncrease());
        _skillValues.Add(skillValuesEnum.secundaryAttackIncrease, _data.getSecundaryIncrease());
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
