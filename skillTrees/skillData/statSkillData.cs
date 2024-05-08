using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// statSkillData es una clase que se usa para almacenar los datos de las habilidades stat.
/// </summary>
[System.Serializable]
public class statSkillData : skillData
{
    /// <summary>
    /// Puntos de fuerza.
    /// </summary>
    [SerializeField] private int _strength;
    
    /// <summary>
    /// Puntos de destreza.
    /// </summary>
    [SerializeField] private int _dexterity;

    /// <summary>
    /// Puntos de precisión.
    /// </summary>
    [SerializeField] private int _precision;

    /// <summary>
    /// Getter que devuelve <see cref="_strength"/>.
    /// </summary>
    /// <returns><see cref="_strength"/>.</returns>
    public int getStrength()
    {
        return _strength;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_dexterity"/>.
    /// </summary>
    /// <returns><see cref="_dexterity"/>.</returns>
    public int getDexterity()
    {
        return _dexterity;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_precision"/>.
    /// </summary>
    /// <returns><see cref="_precision"/>.</returns>
    public int getPrecision()
    {
        return _precision;
    }
}
