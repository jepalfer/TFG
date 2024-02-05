using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statSystem : MonoBehaviour
{
    private static int _level = 6;

    //Atributos

    private static attribute _vitality = new attribute();
    private static attribute _endurance = new attribute();
    private static attribute _strength = new attribute();
    private static attribute _dexterity = new attribute();
    private static attribute _agility = new attribute();
    private static attribute _precision = new attribute();
    public static int getLevel()
    {
        return _level;
    }

    public static void setLevel(int level)
    {
        _level = level;
    }

    public static attribute getVitality()
    {
        return _vitality;
    }

    public static attribute getEndurance()
    {
        return _endurance;
    }

    public static attribute getStrength()
    {
        return _strength;
    }

    public static attribute getDexterity()
    {
        return _dexterity;
    }

    public static attribute getAgility()
    {
        return _agility;
    }

    public static attribute getPrecision()
    {
        return _precision;
    }
}
