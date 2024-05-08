using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// statusSkill es una clase que se usa para representar las habilidades status (aumento de daños extra).
/// </summary>
public class statusSkill : skill, ISkill
{
    /// <summary>
    /// Referencia a los datos internos de la habilidad.
    /// </summary>
    [SerializeField] protected statusSkillData _data;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        //Inicialización del diccionario
        _skillValues = new Dictionary<skillValuesEnum, float>();
        _skillValues.Add(skillValuesEnum.bleedingDamage, _data.getBleedingDamage());
        _skillValues.Add(skillValuesEnum.critDamage, _data.getCritDamage());
        _skillValues.Add(skillValuesEnum.penetrationDamage, _data.getPenetrationDamage());
        _skillValues.Add(skillValuesEnum.baseDamage, _data.getBaseDamage());
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
    /// <returns>Una referencia al script asociado a la habilidad.</returns>
    public skill getSkill()
    {
        return this;
    }
}
