using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New scaling", menuName = "Weapon/Create new weapon scaling")]
public class weaponData : ScriptableObject
{
    [SerializeField] private scalingEnum[] _StrengthScaling;
    [SerializeField] private scalingEnum[] _DexterityScaling;
    [SerializeField] private scalingEnum[] _PrecisionScaling;
    [SerializeField] private rangeEnum _range;
    [SerializeField] private int _numberOfAttacks;

    public scalingEnum[] getStrengthScalings()
    {
        return _StrengthScaling;
    }

    public scalingEnum[] getDexterityScalings()
    {
        return _DexterityScaling;
    }
    public scalingEnum[] getPrecisionScalings()
    {
        return _PrecisionScaling;
    }

    public rangeEnum getRange()
    {
        return _range;
    }

    public int getNumberOfAttacks()
    {
        return _numberOfAttacks;
    }
}
