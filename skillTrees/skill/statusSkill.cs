using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusSkill : skill, ISkill
{
    [SerializeField] protected statusSkillData _data;

    private void Awake()
    {
        _skillValues = new Dictionary<skillValuesEnum, float>();
        _skillValues.Add(skillValuesEnum.bleedingDamage, _data.getBleedingDamage());
        _skillValues.Add(skillValuesEnum.critDamage, _data.getCritDamage());
        _skillValues.Add(skillValuesEnum.penetrationDamage, _data.getPenetrationDamage());
        _skillValues.Add(skillValuesEnum.baseDamage, _data.getBaseDamage());
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
