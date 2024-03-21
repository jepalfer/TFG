using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class regenUpgradeSkill : skill, ISkill
{
    [SerializeField] private regenUpgradeSkillData _skillData;

    private void Awake()
    {
        _skillValues = new Dictionary<skillValuesEnum, float>();
        if (_skillData.getUpgradeType() == upgradeTypeEnum.HP)
        {

            _skillValues.Add(skillValuesEnum.HPUpgrade, _skillData.getUpgradeAmount());
            _skillValues.Add(skillValuesEnum.staminaUpgrade, 0);
        }
        else
        {

            _skillValues.Add(skillValuesEnum.staminaUpgrade, _skillData.getUpgradeAmount());
            _skillValues.Add(skillValuesEnum.HPUpgrade, 0);
        }
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
