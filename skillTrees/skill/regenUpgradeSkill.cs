using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// regenUpgradeSkill es una clase que se usa para representar las habilidades de tipo regenUpgrade (aumento de regeneraci�n)
/// </summary>
[System.Serializable]
public class regenUpgradeSkill : skill, ISkill
{
    /// <summary>
    /// Referencia a los datos internos de la habilidad.
    /// </summary>
    [SerializeField] private regenUpgradeSkillData _skillData;

    /// <summary>
    /// M�todo que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        //Inicializaci�n del diccionario
        _skillValues = new Dictionary<skillValuesEnum, float>();
        if (_skillData.getUpgradeType() == upgradeTypeEnum.HP)
        {

            _skillValues.Add(skillValuesEnum.HPUpgrade, _skillData.getUpgradeAmount());
            _skillValues.Add(skillValuesEnum.staminaUpgrade, 0);
        }
        else
        {

            _skillValues.Add(skillValuesEnum.staminaUpgrade, _skillData.getUpgradeAmount());
            _skillValues.Add(skillValuesEnum.HPUpgrade, 0);
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
