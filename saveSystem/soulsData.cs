using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class soulsData
{
    private long _souls;
    public soulsData()
    {
        _souls = config.getPlayer().GetComponent<combatController>().getSouls();
    }

    public long getSouls()
    {
        return _souls;
    }
}
