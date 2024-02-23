using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// config es una clase utilizada para almacenar las referencias al jugador 
/// y el inventario para que se pueda acceder desde otra clase.
/// </summary>
public static class config
{
    /// <summary>
    /// Referencia al jugador.
    /// </summary>
    private static GameObject _player;

    /// <summary>
    /// Referencia al inventario.
    /// </summary>
    private static GameObject _inventory;

    /// <summary>
    /// Setter que modifica <see cref="_player"/>.
    /// </summary>
    /// <param name="player">El GameObject a asignar.</param>
    public static void setPlayer(GameObject player)
    {
        _player = player;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_player"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene la referencia al jugador.</returns>
    public static GameObject getPlayer()
    {
        return _player;
    }

    /// <summary>
    /// Setter que modifica <see cref="_inventory"/>.
    /// </summary>
    /// <param name="inventory">El GameObject a asignar.</param>
    public static void setInventory(GameObject inventory)
    {
        _inventory = inventory;
    }


    /// <summary>
    /// Getter que devuelve <see cref="_inventory"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene la referencia al inventario.</returns>
    public static GameObject getInventory()
    {
        return _inventory;
    }
}
