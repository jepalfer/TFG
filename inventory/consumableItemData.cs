using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// consumableItemData es una clase que se usa para asignar cu�nto restaura y durante cu�nto tiempo a HP o stamina un consumible.
/// </summary>
[CreateAssetMenu(fileName = "New consumable data", menuName = "Inventory/Create new consumable item internal data")]
[System.Serializable]
public class consumableItemData : ScriptableObject
{

    /// <summary>
    /// El valor de la modificaci�n.
    /// </summary>
    [SerializeField] private float _value;

    /// <summary>
    /// Cu�nto tiempo est� modificando.
    /// </summary>
    [SerializeField] private float _effectiveTime;


    /// <summary>
    /// Getter que devuelve <see cref="_value"/>.
    /// </summary>
    /// <returns>float que representa la cantidad .</returns>
    public float getValue()
    {
        return _value;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_effectiveTime"/>.
    /// </summary>
    /// <returns>float que representa el tiempo durante el que restaura.</returns>
    public float getEffectiveTime()
    {
        return _effectiveTime;
    }
}
