using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class statUpgradeSkill : skill, ISkill
{
    [SerializeField] private statSkillData _data;

    private void Awake()
    {
        Debug.Log(_skillValues);
        _skillValues = new Dictionary<skillValuesEnum, float>();

        _skillValues.Add(skillValuesEnum.strengthUpgrade, _data.getStrength());
        _skillValues.Add(skillValuesEnum.dexterityUpgrade, _data.getDexterity());
        _skillValues.Add(skillValuesEnum.precisionUpgrade, _data.getPrecision());

        Debug.Log(_skillValues[skillValuesEnum.strengthUpgrade]);
        Debug.Log(_skillValues[skillValuesEnum.dexterityUpgrade]);
        Debug.Log(_skillValues[skillValuesEnum.precisionUpgrade]);
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
