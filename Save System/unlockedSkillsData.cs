using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class unlockedSkillsData
{
    [SerializeField] private List<sceneSkillsState> _unlockedSkills = new List<sceneSkillsState>();
    
    public unlockedSkillsData()
    {
        _unlockedSkills = new List<sceneSkillsState>();
    }

    public unlockedSkillsData(List<sceneSkillsState> skills)
    {
        _unlockedSkills = skills;
    }

    public List<sceneSkillsState> getUnlockedSkills()
    {
        return _unlockedSkills;
    }
}
