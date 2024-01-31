using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class skillData
{
    [SerializeField] protected string _skillName;
    [SerializeField] protected string _skillSprite;
    [SerializeField] protected bool _isUnlocked;
    [SerializeField] protected int _skillPoints;
    [SerializeField] protected bool _canBeUnlocked;
    [SerializeField] protected int _skillID;
    [TextArea(3, 10)]
    [SerializeField] protected string _skillDesc;
    [SerializeField] protected skillTypeEnum _skillType;
    [SerializeField] protected equipEnum _equipType;

    public string getSkillName()
    {
        return _skillName;
    }

    public skillTypeEnum getType()
    {
        return _skillType;
    }

    public Sprite getSkillSprite()
    {
        return Resources.Load<Sprite>(_skillSprite);
    }

    public bool getIsUnlocked()
    {
        return _isUnlocked;
    }

    public int getSkillPoints()
    {
        return _skillPoints;
    }

    public bool getCanBeUnlocked()
    {
        return _canBeUnlocked;
    }
    public int getSkillID()
    {
        return _skillID;
    }

    public string getSkillDescription()
    {
        return _skillDesc;
    }


    public void setCanBeUnlocked(bool value)
    {
        _canBeUnlocked = value;
    }
    public void setIsUnlocked(bool value)
    {
        _isUnlocked = value;
    }

    public equipEnum getEquipType()
    {
        return _equipType;
    }
}
