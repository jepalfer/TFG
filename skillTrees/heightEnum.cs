using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// heightEnum es un enum que se usa para saber el tipo de ataque en picado que se ha realizado,
/// según la altura desde la que se haya hecho.
/// </summary>
public enum heightEnum
{
    /// <summary>
    /// Ataque fuerte.
    /// </summary>
    strong,

    /// <summary>
    /// Ataque débil.
    /// </summary>
    weak,

    /// <summary>
    /// No hay ataque.
    /// </summary>
    none
}
