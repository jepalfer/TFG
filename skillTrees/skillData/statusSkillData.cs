using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// statusSkillData es una clase que se usa para almacenar los datos internos de las habilidades status.
/// </summary>
[System.Serializable]
public class statusSkillData : skillData
{
    /// <summary>
    /// Da�o de penetraci�n.
    /// </summary>
    [SerializeField] private float _penetrationDamage;
    
    /// <summary>
    /// Da�o de sangrado.
    /// </summary>
    [SerializeField] private float _bleedingDamage;

    /// <summary>
    /// Da�o cr�tico.
    /// </summary>
    [SerializeField] private float _critDamage;
    
    /// <summary>
    /// Da�o base.
    /// </summary>
    [SerializeField] private float _baseDamage;

    /// <summary>
    /// Getter que devuelve <see cref="_penetrationDamage"/>.
    /// </summary>
    /// <returns><see cref="_penetrationDamage"/>.</returns>
    public float getPenetrationDamage()
    {
        return _penetrationDamage;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_bleedingDamage"/>.
    /// </summary>
    /// <returns><see cref="_bleedingDamage"/>.</returns>
    public float getBleedingDamage()
    {
        return _bleedingDamage;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_critDamage"/>.
    /// </summary>
    /// <returns><see cref="_critDamage"/>.</returns>
    public float getCritDamage()
    {
        return _critDamage;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_baseDamage"/>.
    /// </summary>
    /// <returns><see cref="_baseDamage"/>.</returns>
    public float getBaseDamage()
    {
        return _baseDamage;
    }
}
