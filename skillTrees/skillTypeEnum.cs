using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// skillTypeEnum es un enum que representa el tipo de habilidad.
/// </summary>
[System.Serializable]
public enum skillTypeEnum
{
    /// <summary>
    /// Habilidad para aumentar los combos.
    /// </summary>
    combo,

    /// <summary>
    /// Habilidad para aumentar stat.
    /// </summary>
    stat,

    /// <summary>
    /// Habilidad que mejora un tipo de da�o.
    /// </summary>
    status,

    /// <summary>
    /// Habilidad que representa una "funcionalidad" (doble salto por ejemplo)
    /// </summary>
    functionality,

    /// <summary>
    /// Habilidad que mejora la regeneraci�n.
    /// </summary>
    regenUpgrade,

    /// <summary>
    /// Habilidad que aumenta la probabilidad de da�o de sangrado o de cr�tico.
    /// </summary>
    probabilityAugment,

    /// <summary>
    /// Habilidad que proporciona robo de vida.
    /// </summary>
    lifeSteal,

    /// <summary>
    /// Habilidad que aumenta las almas obtenidas.
    /// </summary>
    souls
}
