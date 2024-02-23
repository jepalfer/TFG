using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// comboSkillData es una clase que se usa para almacenar los datos internos de una habilidad de tipo <see cref="comboIncreaseSkill"/>.
/// </summary>
[System.Serializable]
public class comboSkillData : skillData
{
    /// <summary>
    /// El aumento de golpes al combo del arma primaria.
    /// </summary>
    [SerializeField] private int _primaryIncrease;

    /// <summary>
    /// El aumento de golpes al combo del arma secundaria.
    /// </summary>
    [SerializeField] private int _secundaryIncrease;

    /// <summary>
    /// Getter que devuelve <see cref="_primaryIncrease"/>.
    /// </summary>
    /// <returns>Un int que representa el aumento de golpes al arma primaria.</returns>
    public int getPrimaryIncrease()
    {
        return _primaryIncrease;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_secundaryIncrease"/>.
    /// </summary>
    /// <returns>Un int que representa el aumento de golpes al arma secundaria.</returns>
    public int getSecundaryIncrease()
    {
        return _secundaryIncrease;
    }
}
