using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// statsData es una clase que se usa para serializar los puntos de vida y stamina actuales.
/// </summary>
[System.Serializable]
public class statsData
{
    /// <summary>
    /// HP actuales.
    /// </summary>
    private float _currentHP;

    /// <summary>
    /// Stamina actual.
    /// </summary>
    private float _currentStamina;

    /// <summary>
    /// Constructor por defecto de la clase.
    /// </summary>
    public statsData()
    {
        _currentHP = config.getPlayer().GetComponent<statsController>().getCurrentHP();
        _currentStamina = config.getPlayer().GetComponent<statsController>().getCurrentStamina();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_currentHP"/>.
    /// </summary>
    /// <returns><see cref="_currentHP"/>.</returns>
    public float getCurrentHP()
    {
        return _currentHP;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_currentStamina"/>.
    /// </summary>
    /// <returns><see cref="_currentStamina"/>.</returns>
    public float getCurrentStamina()
    {
        return _currentStamina;
    }
}
