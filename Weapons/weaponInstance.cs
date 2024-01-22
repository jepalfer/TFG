using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// weaponInstance
/// </summary>
[System.Serializable]
public class weaponInstance
{
    [SerializeField] private weaponData _scaling;
    public weaponInstance(weaponData scaling)
    {
        _scaling = scaling;
    }

    public weaponData getScaling()
    {
        return _scaling;
    }
}
