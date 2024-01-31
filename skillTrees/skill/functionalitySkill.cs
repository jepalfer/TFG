using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class functionalitySkill : skill, ISkill
{
    [SerializeField] private functionalitySkillData _data;

    public override skillData getData()
    {
        return _data;
    }
    public skill getSkill()
    {
        return this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("a");
        }
    }
}
