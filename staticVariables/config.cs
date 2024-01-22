using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class config
{
    private static GameObject _player;
    private static GameObject _inventory;

    public static void setPlayer(GameObject player)
    {
        _player = player;
    }

    public static GameObject getPlayer()
    {
        return _player;
    }

    public static void setInventory(GameObject inventory)
    {
        _inventory = inventory;
    }

    public static GameObject getInventory()
    {
        return _inventory;
    }
}
