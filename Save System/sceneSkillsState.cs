using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class sceneSkillsState
{
    [SerializeField] protected int _weaponID;
    [SerializeField] protected skillData _associatedSkill;

    public sceneSkillsState(int weaponID, skill skill)
    {
        _weaponID = weaponID;
        _associatedSkill = skill.getData();
    }

    public int getWeaponID()
    {
        return _weaponID;
    }

    public skillData getAssociatedSkill()
    {
        return _associatedSkill;
    }
}
