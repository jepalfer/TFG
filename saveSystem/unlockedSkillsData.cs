using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unlockedSkillsData es una clase que se usa para almacenar la información de las habilidades desbloqueadas.
/// </summary>
[System.Serializable]
public class unlockedSkillsData
{
    /// <summary>
    /// Lista con las habilidades desbloqueadas.
    /// </summary>
    [SerializeField] private List<sceneSkillsState> _unlockedSkills = new List<sceneSkillsState>();
    
    /// <summary>
    /// Constructor por defecto de la clase. Crea una lista vacía.
    /// </summary>
    public unlockedSkillsData()
    {
        _unlockedSkills = new List<sceneSkillsState>();
    }

    /// <summary>
    /// Constructor con parámetros de la clase. Asigna una lista de habilidades a <see cref="_unlockedSkills"/>.
    /// </summary>
    /// <param name="skills">Lista de habilidades a asignar.</param>
    public unlockedSkillsData(List<sceneSkillsState> skills)
    {
        _unlockedSkills = skills;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_unlockedSkills"/>.
    /// </summary>
    /// <returns><see cref="_unlockedSkills"/>.</returns>
    public List<sceneSkillsState> getUnlockedSkills()
    {
        return _unlockedSkills;
    }
}
