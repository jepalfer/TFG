using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// functionalitySkill es una clase que se usa para representar las habilidades de tipo functionality (aportan una "funcionalidad")
/// </summary>
public class functionalitySkill : skill, ISkill
{
    /// <summary>
    /// Referencia a los datos internos de la habilidad.
    /// </summary>
    [SerializeField] private functionalitySkillData _data;

    /// <summary>
    /// Getter que devuelve <see cref="_data"/>.
    /// </summary>
    /// <returns></returns>
    public override skillData getData()
    {
        return _data;
    }

    /// <summary>
    /// Getter que devuelve <see cref="this"/>.
    /// </summary>
    /// <returns>Referencia al script asociado a la habilidad.</returns>
    public skill getSkill()
    {
        return this;
    }
}
