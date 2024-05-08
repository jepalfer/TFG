using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// soulSkillData es una clase que se usa para almacenar los datos de las habilidades de tipo soul.
/// </summary>
[System.Serializable]
public class soulSkillData : skillData
{
    /// <summary>
    /// Incremento en las almas recibidas.
    /// </summary>
    [SerializeField] private float _soulIncrease;

    /// <summary>
    /// Getter que devuelve <see cref="_soulIncrease"/>.
    /// </summary>
    /// <returns><see cref="_soulIncrease"/>.</returns>
    public float getSoulIncrease()
    {
        return _soulIncrease;
    }
}
