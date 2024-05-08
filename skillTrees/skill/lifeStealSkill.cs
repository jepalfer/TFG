using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lifeStealSkill es una clase que se usa para controlar el valor de robo de vida que obtenemos.
/// </summary>
[System.Serializable]
public class lifeStealSkill : skill, ISkill
{
    /// <summary>
    /// Referencia a los datos internos de la habilidad.
    /// </summary>
    [SerializeField] private lifeStealSkillData _skillData;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        _skillValues = new Dictionary<skillValuesEnum, float>();
        _skillValues.Add(skillValuesEnum.lifeSteal, _skillData.getLifeSteal());
    }

    /// <summary>
    /// Getter que devuelve <see cref="_skillData"/>.
    /// </summary>
    /// <returns><see cref="_skillData"/>.</returns>
    public override skillData getData()
    {
        return _skillData;
    }

    /// <summary>
    /// Getter que devuelve <see cref="this"/>.
    /// </summary>
    /// <returns>Referencia al script de la habilidad.</returns>
    public skill getSkill()
    {
        return this;
    }
}
