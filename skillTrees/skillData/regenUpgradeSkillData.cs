using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// regenUpgradeSkillData es una clase que se usa para almacenar los datos concretos de una habilidad tipo regenUpgrade.
/// </summary>
[System.Serializable]
public class regenUpgradeSkillData : skillData
{
    /// <summary>
    /// Tipo de regeneración que proporciona.
    /// </summary>
    [SerializeField] private upgradeTypeEnum _upgradeType;
    
    /// <summary>
    /// Cantidad de regeneración que aporta.
    /// </summary>
    [SerializeField] private float _upgradeAmount;

    /// <summary>
    /// Getter que devuelve <see cref="_upgradeType"/>.
    /// </summary>
    /// <returns><see cref="_upgradeType"/>.</returns>
    public upgradeTypeEnum getUpgradeType()
    {
        return _upgradeType;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_upgradeAmount"/>.
    /// </summary>
    /// <returns><see cref="_upgradeAmount"/>.</returns>
    public float getUpgradeAmount()
    {
        return _upgradeAmount;
    }
}
