using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class probabilityAugmentSkillData : skillData
{
    [SerializeField] private probabilityTypeEnum _type;
    [SerializeField] private float _augment;
    public probabilityTypeEnum getAugmentType()
    {
        return _type;
    }
    public float getAugment()
    {
        return _augment;
    }
}
