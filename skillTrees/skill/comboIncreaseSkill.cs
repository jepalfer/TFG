using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// comboIncreaseSkill es una clase que se usa para representar las habilidades de tipo comboIncrease (aumenta el número de golpes de
/// combo).
/// </summary>
[System.Serializable]
public class comboIncreaseSkill : skill, ISkill
{
    /// <summary>
    /// Referencia a los datos internos de la habilidad.
    /// </summary>
    [SerializeField] private comboSkillData _data;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        //Inicialización del diccionario
        _skillValues = new Dictionary<skillValuesEnum, float>();
        _skillValues.Add(skillValuesEnum.primaryAttackIncrease, _data.getPrimaryIncrease());
        _skillValues.Add(skillValuesEnum.secundaryAttackIncrease, _data.getSecundaryIncrease());
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
