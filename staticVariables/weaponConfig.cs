using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// weaponConfig es una clase que se usa para almacenar las referencias de forma estática de las armas primaria y 
/// secundaria.
/// </summary>
public static class weaponConfig
{
    /// <summary>
    /// Referencia al arma primaria.
    /// </summary>
    private static GameObject _primaryWeapon = null;

    /// <summary>
    /// Referencia al arma secundaria.
    /// </summary>
    private static GameObject _secundaryWeapon = null;

    /// <summary>
    /// Setter que modifica la referencia a <see cref="_primaryWeapon"/>.
    /// </summary>
    /// <param name="weapon">Referencia al arma a asignar.</param>
    public static void setPrimaryWeapon(GameObject weapon)
    {
        _primaryWeapon = weapon;
    }

    /// <summary>
    /// Setter que modifica la referencia a <see cref="_secundaryWeapon"/>.
    /// </summary>
    /// <param name="weapon">Referencia al arma a asignar.</param>
    public static void setSecundaryWeapon(GameObject weapon)
    {
        _secundaryWeapon = weapon;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_primaryWeapon"/>.
    /// </summary>
    /// <returns><see cref="_primaryWeapon"/>.</returns>
    public static GameObject getPrimaryWeapon()
    {
        return _primaryWeapon;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_secundaryWeapon"/>.
    /// </summary>
    /// <returns><see cref="_secundaryWeapon"/>.</returns>
    public static GameObject getSecundaryWeapon()
    {
        return _secundaryWeapon;
    }
}
