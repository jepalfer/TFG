using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// soulsData es una clase que se usa para serializar la información de las almas.
/// </summary>
[System.Serializable]
public class soulsData
{
    /// <summary>
    /// Cantidad de almas.
    /// </summary>
    private long _souls;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    public soulsData()
    {
        _souls = config.getPlayer().GetComponent<combatController>().getSouls();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_souls"/>.
    /// </summary>
    /// <returns><see cref="_souls"/>.</returns>
    public long getSouls()
    {
        return _souls;
    }
}
