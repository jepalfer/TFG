using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// probabilityAugmentSkill es una clase que se usa para representar las habilidades de tipo probabilityAugment (aumentan una 
/// probabilidad).
/// </summary>
[System.Serializable]
public class probabilityAugmentSkill : skill, ISkill
{
    /// <summary>
    /// Referencia a los datos internos de la habilidad.
    /// </summary>
    [SerializeField] private probabilityAugmentSkillData _skillData;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        //Inicialización del diccionario
        _skillValues = new Dictionary<skillValuesEnum, float>();
        if (_skillData.getAugmentType() == probabilityTypeEnum.bleeding)
        {
            _skillValues.Add(skillValuesEnum.bleedProbability, _skillData.getAugment());
            _skillValues.Add(skillValuesEnum.critProbability, 0);
        }
        else
        {
            _skillValues.Add(skillValuesEnum.critProbability, _skillData.getAugment());
            _skillValues.Add(skillValuesEnum.bleedProbability, 0);
        }
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
    /// <returns>Referencia al script asociado a la habilidad.</returns>
    public skill getSkill()
    {
        return this;
    }
}
