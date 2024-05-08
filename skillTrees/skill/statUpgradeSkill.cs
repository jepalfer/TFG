using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// statUpgradeSkill es una clase que se usa para representar a las habilidades de tipo statUpgrade (aumento de atributo)
/// </summary>
[System.Serializable]
public class statUpgradeSkill : skill, ISkill
{
    /// <summary>
    /// Referencia a los datos internos de la habilidad.
    /// </summary>
    [SerializeField] private statSkillData _data;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        //Inicialización del diccionario
        //Debug.Log(_skillValues);
        _skillValues = new Dictionary<skillValuesEnum, float>();

        _skillValues.Add(skillValuesEnum.strengthUpgrade, _data.getStrength());
        _skillValues.Add(skillValuesEnum.dexterityUpgrade, _data.getDexterity());
        _skillValues.Add(skillValuesEnum.precisionUpgrade, _data.getPrecision());

        //Debug.Log(_skillValues[skillValuesEnum.strengthUpgrade]);
        //Debug.Log(_skillValues[skillValuesEnum.dexterityUpgrade]);
        //Debug.Log(_skillValues[skillValuesEnum.precisionUpgrade]);
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
