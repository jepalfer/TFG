using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// equipEnum es un enum que se usa para saber si una habilidad se puede equipar o es propia de arma.
/// </summary>
public enum equipEnum
{
    /// <summary>
    /// La habilidad es equipable.
    /// </summary>
    equippable,

    /// <summary>
    /// La habilidad es propia de arma.
    /// </summary>
    onWeapon,
}
