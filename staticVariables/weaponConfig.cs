using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class weaponConfig
{
    private static GameObject _primaryWeapon = null;
    private static GameObject _secundaryWeapon = null;

    public static void setPrimaryWeapon(GameObject weapon)
    {
        _primaryWeapon = weapon;
    }
    public static void setSecundaryWeapon(GameObject weapon)
    {
        _secundaryWeapon = weapon;
    }

    public static GameObject getPrimaryWeapon()
    {
        return _primaryWeapon;
    }
    public static GameObject getSecundaryWeapon()
    {
        return _secundaryWeapon;
    }
}
