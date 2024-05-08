using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ISkill es una interfaz que implementan las habilidades.
/// </summary>
public interface ISkill
{
    /// <summary>
    /// Getter que devuelve la propia habilidad.
    /// </summary>
    /// <returns>Script de la habilidad.</returns>
    public skill getSkill();
}
