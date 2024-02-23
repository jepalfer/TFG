using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// weaponData es una clase que se usa para almacenar los datos internos que son exclusivos de las armas.
/// </summary>

[CreateAssetMenu (fileName = "New scaling", menuName = "Weapon/Create new weapon scaling")]
public class weaponData : ScriptableObject
{
    /// <summary>
    /// La lista de escalados de fuerza.
    /// </summary>
    [SerializeField] private scalingEnum[] _strengthScaling;

    /// <summary>
    /// La lista de escalados de destreza.
    /// </summary>
    [SerializeField] private scalingEnum[] _dexterityScaling;

    /// <summary>
    /// La lista de escalados de precisión.
    /// </summary>
    [SerializeField] private scalingEnum[] _precisionScaling;

    /// <summary>
    /// El rango con el que golpea el arma.
    /// </summary>
    [SerializeField] private rangeEnum _range;

    /// <summary>
    /// El número de ataques del arma.
    /// </summary>
    [SerializeField] private int _numberOfAttacks;

    /// <summary>
    /// Getter que devuelve <see cref="_strengthScaling"/>
    /// </summary>
    /// <returns>Un array de <see cref="scalingEnum"/> que representa los escalados en fuerza.</returns>
    public scalingEnum[] getStrengthScalings()
    {
        return _strengthScaling;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_dexterityScaling"/>
    /// </summary>
    /// <returns>Un array de <see cref="scalingEnum"/> que representa los escalados en destreza.</returns>
    public scalingEnum[] getDexterityScalings()
    {
        return _dexterityScaling;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_precisionScaling"/>
    /// </summary>
    /// <returns>Un array de <see cref="scalingEnum"/> que representa los escalados en precisión.</returns>
    public scalingEnum[] getPrecisionScalings()
    {
        return _precisionScaling;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_range"/>
    /// </summary>
    /// <returns>Un objeto tipo <see cref="rangeEnum"/> que representa el rango de ataque del arma.</returns>
    public rangeEnum getRange()
    {
        return _range;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_numberOfAttacks"/>
    /// </summary>
    /// <returns>Un int que representa el número de ataques del arma.</returns>
    public int getNumberOfAttacks()
    {
        return _numberOfAttacks;
    }
}
