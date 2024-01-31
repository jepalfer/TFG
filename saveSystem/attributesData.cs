using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class attributesData
{
    private int _level;
    private int _vitality;
    private int _endurance;
    private int _strength;
    private int _dexterity;
    private int _agility;
    private int _precision;

    public attributesData()
    {
        _level = statSystem.getLevel();
        _vitality = statSystem.getVitality().getLevel();
        _endurance = statSystem.getEndurance().getLevel();
        _strength = statSystem.getStrength().getLevel();
        _dexterity = statSystem.getDexterity().getLevel();
        _agility = statSystem.getAgility().getLevel();
        _precision = statSystem.getPrecision().getLevel(); 
        
    }

    public int getLevel()
    {
        return _level;
    }
    public int getVitality()
    {
        return _vitality;
    }
    public int getEndurance()
    {
        return _endurance;
    }
    public int getStrength()
    {
        return _strength;
    }
    public int getDexterity()
    {
        return _dexterity;
    }
    public int getAgility()
    {
        return _agility;
    }
    public int getPrecision()
    {
        return _precision;
    }
}
