using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class statsData
{
    private float _currentHP;
    private float _currentStamina;

    public statsData()
    {
        _currentHP = config.getPlayer().GetComponent<statsController>().getCurrentHP();
        _currentStamina = config.getPlayer().GetComponent<statsController>().getCurrentStamina();
    }

    public float getCurrentHP()
    {
        return _currentHP;
    }
    public float getCurrentStamina()
    {
        return _currentStamina;
    }
}
