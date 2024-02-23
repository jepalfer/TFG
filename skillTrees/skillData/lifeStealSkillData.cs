using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class lifeStealSkillData : skillData
{
    [SerializeField] private float _lifeSteal;

    public float getLifeSteal()
    {
        return _lifeSteal;
    }
}
