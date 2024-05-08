using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lifeStealSkillData es una clase que se usa para almacenar los datos de las habilidades tipo lifeSteal.
/// </summary>
[System.Serializable]
public class lifeStealSkillData : skillData
{
    /// <summary>
    /// Robo de vida proporcionado.
    /// </summary>
    [SerializeField] private float _lifeSteal;

    /// <summary>
    /// Getter que devuelve <see cref="_lifeSteal"/>.
    /// </summary>
    /// <returns><see cref="_lifeSteal"/>.</returns>
    public float getLifeSteal()
    {
        return _lifeSteal;
    }
}
