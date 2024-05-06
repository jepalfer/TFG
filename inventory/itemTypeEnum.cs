using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// itemTypeEnum es un enum que representa el tipo de objeto.
/// </summary>
[System.Serializable]
public enum itemTypeEnum
{
    /// <summary>
    /// Tipo de objeto clave.
    /// </summary>
    keyItem,

    /// <summary>
    /// Tipo de objeto consumible.
    /// </summary>
    consumable,

    /// <summary>
    /// Tipo de objeto arma.
    /// </summary>
    weapon,

    /// <summary>
    /// Tipo de objeto material de mejora.
    /// </summary>
    upgradeMaterial,

    /// <summary>
    /// Tipo de objeto (consumible) recargable
    /// </summary>
    refillable,
}
