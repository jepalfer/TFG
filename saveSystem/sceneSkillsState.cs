using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sceneSkillsState es una clase auxiliar usada para guardar la información de una habilidad asociada a un arma.
/// </summary>
[System.Serializable]
public class sceneSkillsState
{
    /// <summary>
    /// ID interno del arma a la que está asociada la habilidad.
    /// </summary>
    [SerializeField] private int _weaponID;

    /// <summary>
    /// Datos internos de la habilidad a serializar.
    /// </summary>
    [SerializeField] private skillData _associatedSkill;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="weaponID">ID interno del arma.</param>
    /// <param name="skill">Habilidad a serializar.</param>
    public sceneSkillsState(int weaponID, skill skill)
    {
        _weaponID = weaponID;
        _associatedSkill = skill.getData();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_weaponID"/>.
    /// </summary>
    /// <returns><see cref="_weaponID"/>.</returns>
    public int getWeaponID()
    {
        return _weaponID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_associatedSkill"/>.
    /// </summary>
    /// <returns><see cref="_associatedSkill"/>.</returns>
    public skillData getAssociatedSkill()
    {
        return _associatedSkill;
    }
}
