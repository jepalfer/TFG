using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// equippedWeaponsData es una clase que se usa para guardar los ID internos y niveles de las armas equipadas.
/// </summary>
[System.Serializable]
public class equippedWeaponsData
{
    /// <summary>
    /// ID del arma primaria.
    /// </summary>
    [SerializeField] private int _primaryIndex = -1;

    /// <summary>
    /// ID del arma secundaria.
    /// </summary>
    [SerializeField] private int _secundaryIndex = -1;

    /// <summary>
    /// Lista con los niveles de las armas.
    /// </summary>
    [SerializeField] private List<int> _weaponLevels;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="primary">ID de arma primaria.</param>
    /// <param name="secundary">ID de arma secundaria.</param>
    /// <param name="levels">Niveles de las armas.</param>
    public equippedWeaponsData(int primary, int secundary, List<int> levels)
    {
        _primaryIndex = primary;
        _secundaryIndex = secundary;
        _weaponLevels = levels;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_primaryIndex"/>.
    /// </summary>
    /// <returns><see cref="_primaryIndex"/>.</returns>
    public int getPrimaryIndex()
    {
        return _primaryIndex;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_secundaryIndex"/>.
    /// </summary>
    /// <returns><see cref="_secundaryIndex"/>.</returns>
    public int getSecundaryIndex()
    {
        return _secundaryIndex;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_weaponLevels"/>.
    /// </summary>
    /// <returns><see cref="_weaponLevels"/>.</returns>
    public List<int> getWeaponsLevels()
    {
        return _weaponLevels;
    }

    /// <summary>
    /// Setter que modifica <see cref="_primaryIndex"/>.
    /// </summary>
    /// <param name="index">ID a asignar.</param>
    public void setPrimaryIndex(int index)
    {
        _primaryIndex = index;
    }

    /// <summary>
    /// Setter que modifica <see cref="_secundaryIndex"/>.
    /// </summary>
    /// <param name="index">ID a asignar.</param>
    public void setSecundaryIndex(int index)
    {
        _secundaryIndex = index;
    }

}
