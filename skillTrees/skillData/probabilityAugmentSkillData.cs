using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// probabilityAugmentSkillData es una clase que se usa para almacenar los datos de las habilidades tipo probabilityAugment.
/// </summary>
[System.Serializable]
public class probabilityAugmentSkillData : skillData
{
    /// <summary>
    /// Tipo de probabilidad que aumenta.
    /// </summary>
    [SerializeField] private probabilityTypeEnum _type;

    /// <summary>
    /// Aumento que propociona.
    /// </summary>
    [SerializeField] private float _augment;


    /// <summary>
    /// Getter que devuelve <see cref="_type"/>.
    /// </summary>
    /// <returns><see cref="_type"/>.</returns>
    public probabilityTypeEnum getAugmentType()
    {
        return _type;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_augment"/>.
    /// </summary>
    /// <returns><see cref="_augment"/>.</returns>
    public float getAugment()
    {
        return _augment;
    }
}
