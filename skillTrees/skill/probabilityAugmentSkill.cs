using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class probabilityAugmentSkill : skill, ISkill
{
    [SerializeField] private probabilityAugmentSkillData _skillData;

    public override skillData getData()
    {
        return _skillData;
    }

    public skill getSkill()
    {
        return this;
    }
}
