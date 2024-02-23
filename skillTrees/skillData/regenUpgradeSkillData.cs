using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class regenUpgradeSkillData : skillData
{
    [SerializeField] private upgradeTypeEnum _upgradeType;
    [SerializeField] private float _upgradeAmount;
    public upgradeTypeEnum getUpgradeType()
    {
        return _upgradeType;
    }

    public float getUpgradeAmount()
    {
        return _upgradeAmount;
    }
}
