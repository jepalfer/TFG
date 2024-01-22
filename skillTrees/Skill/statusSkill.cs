using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusSkill : skill, ISkill
{
    [SerializeField] protected statusSkillData _data;
    public override skillData getData()
    {
        return _data;
    }
    public skill getSkill()
    {
        return this;
    }
}
