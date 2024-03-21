using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class probabilityAugmentSkill : skill, ISkill
{
    [SerializeField] private probabilityAugmentSkillData _skillData;

    private void Awake()
    {
        _skillValues = new Dictionary<skillValuesEnum, float>();
        if (_skillData.getAugmentType() == probabilityTypeEnum.bleeding)
        {
            _skillValues.Add(skillValuesEnum.bleedProbability, _skillData.getAugment());
            _skillValues.Add(skillValuesEnum.critProbability, 0);
        }
        else
        {
            _skillValues.Add(skillValuesEnum.critProbability, _skillData.getAugment());
            _skillValues.Add(skillValuesEnum.bleedProbability, 0);
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
