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
    /// Habilidad que mejora un tipo de daño.
    /// </summary>
    status,

    /// <summary>
    /// Habilidad que representa una "funcionalidad" (doble salto por ejemplo)
    /// </summary>
    functionality,

    /// <summary>
    /// Habilidad que mejora la regeneración.
    /// </summary>
    regenUpgrade,

    /// <summary>
    /// Habilidad que aumenta la probabilidad de daño de sangrado o de crítico.
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
