using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class levelUpUIConfiguration
{
    private static TextMeshProUGUI _levelValue;
    private static TextMeshProUGUI _soulsValue;
    private static TextMeshProUGUI _requiredSoulsValue;
    private static TextMeshProUGUI _vitalityLevel;
    private static TextMeshProUGUI _enduranceLevel;
    private static TextMeshProUGUI _strengthLevel;
    private static TextMeshProUGUI _dexterityLevel;
    private static TextMeshProUGUI _agilityLevel;
    private static TextMeshProUGUI _precisionLevel;
    private static TextMeshProUGUI _maxHP;
    private static TextMeshProUGUI _maxStamina;
    private static TextMeshProUGUI _primaryDMG;
    private static TextMeshProUGUI _secundaryDMG;

    public static void setLevelValue(TextMeshProUGUI field)
    {
        _levelValue = field;
    }
    public static void setSoulsValue(TextMeshProUGUI field)
    {
        _soulsValue = field;
    }
    public static void setRequiredSoulsValue(TextMeshProUGUI field)
    {
        _requiredSoulsValue = field;
    }
    public static void setVitalityValue(TextMeshProUGUI field)
    {
        _vitalityLevel = field;
    }
    public static void setEnduranceValue(TextMeshProUGUI field)
    {
        _enduranceLevel = field;
    }
    public static void setStrengthValue(TextMeshProUGUI field)
    {
        _strengthLevel = field;
    }
    public static void setDexterityValue(TextMeshProUGUI field)
    {
        _dexterityLevel = field;
    }
    public static void setAgilityValue(TextMeshProUGUI field)
    {
        _agilityLevel = field;
    }
    public static void setPrecisionValue(TextMeshProUGUI field)
    {
        _precisionLevel = field;
    }
    public static void setMaxHPValue(TextMeshProUGUI field)
    {
        _maxHP = field;
    }
    public static void setMaxStaminaValue(TextMeshProUGUI field)
    {
        _maxStamina = field;
    }
    public static void setPrimaryDMGValue(TextMeshProUGUI field)
    {
        _primaryDMG = field;
    }
    public static void setSecundaryDMGValue(TextMeshProUGUI field)
    {
        _secundaryDMG = field;
    }


    public static TextMeshProUGUI getLevelValue()
    {
        return _levelValue;
    }
    public static TextMeshProUGUI getSoulsValue()
    {
        return _soulsValue;
    }
    public static TextMeshProUGUI getRequiredSoulsValue()
    {
        return _requiredSoulsValue;
    }
    public static TextMeshProUGUI getVitalityValue()
    {
        return _vitalityLevel;
    }
    public static TextMeshProUGUI getEnduranceValue()
    {
        return _enduranceLevel;
    }
    public static TextMeshProUGUI getStrengthValue()
    {
        return _strengthLevel;
    }
    public static TextMeshProUGUI getDexterityValue()
    {
        return _dexterityLevel;
    }
    public static TextMeshProUGUI getAgilityValue()
    {
        return _agilityLevel;
    }
    public static TextMeshProUGUI getPrecisionValue()
    {
        return _precisionLevel;
    }
    public static TextMeshProUGUI getMaxHPValue()
    {
        return _maxHP;
    }
    public static TextMeshProUGUI getMaxStaminaValue()
    {
        return _maxStamina;
    }
    public static TextMeshProUGUI getPrimaryDMGValue()
    {
        return _primaryDMG;
    }
    public static TextMeshProUGUI getSecundaryDMGValue()
    {
        return _secundaryDMG;
    }
}
