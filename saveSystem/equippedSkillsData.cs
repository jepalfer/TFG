using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// equippedSkillsData es una clase que se usa para guardar la información de las habilidades equipadas.
/// </summary>
[System.Serializable]
public class equippedSkillsData
{
    /// <summary>
    /// Array con los IDs internos de las habilidades equipadas.
    /// </summary>
    [SerializeField] private int [] _IDs = new int [3];

    /// <summary>
    /// Array con los tipos de habilidades.
    /// </summary>
    [SerializeField] private skillTypeEnum[] _types = new skillTypeEnum[3];

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="IDs">Lista de los IDs de habilidades equipadas.</param>
    /// <param name="types">Lista de los tipos de habilidades equipadas.</param>
    public equippedSkillsData(int [] IDs, skillTypeEnum[] types)
    {
        _IDs = IDs;
        _types = types;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_IDs"/>.
    /// </summary>
    /// <returns><see cref="_IDs"/>.</returns>
    public int [] getIDs()
    {
        return _IDs;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_types"/>.
    /// </summary>
    /// <returns><see cref="_types"/>.</returns>
    public skillTypeEnum[] getTypes()
    {
        return _types;
    }
}
