using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// soulSkill es una clase que se usa para representar las habilidades de tipo soul (aumento de cantidad de almas recibidas).
/// </summary>
public class soulSkill : skill, ISkill
{
    /// <summary>
    /// Referencia a los datos internos de la habilidad.
    /// </summary>
    [SerializeField] private soulSkillData _data;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        //Inicialización del diccionario
        _skillValues = new Dictionary<skillValuesEnum, float>();

        _skillValues.Add(skillValuesEnum.soulIncrease, _data.getSoulIncrease());
    }

    /// <summary>
    /// Getter que devuelve <see cref="_data"/>.
    /// </summary>
    /// <returns><see cref="_data"/>.</returns>
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
