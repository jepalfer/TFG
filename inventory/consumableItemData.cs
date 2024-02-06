using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// consumableItemData es una clase que se usa para asignar cuánto restaura y durante cuánto tiempo a HP o stamina un consumible.
/// </summary>
[CreateAssetMenu(fileName = "New consumable", menuName = "Inventory/Create new consumable item")]
[System.Serializable]
public class consumableItemData : ScriptableObject
{
    /// <summary>
    /// El tipo de consumible.
    /// </summary>
    [SerializeField] private consumableTypeEnum _typeOfConsumable;

    /// <summary>
    /// Cuánto restaura.
    /// </summary>
    [SerializeField] private float _restore;

    /// <summary>
    /// El tiempo durante el que restaura.
    /// </summary>
    [SerializeField] private float _timeRestoring;

    /// <summary>
    /// Getter que devuelve <see cref="_typeOfConsumable"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="consumableTypeEnum"/> que representa si restaura hp o stamina.</returns>
    public consumableTypeEnum getTypeOfConsumable()
    {
        return _typeOfConsumable;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_restore"/>.
    /// </summary>
    /// <returns>float que representa la cantidad restaurada.</returns>
    public float getRestore()
    {
        return _restore;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_timeRestoring"/>.
    /// </summary>
    /// <returns>float que representa el tiempo durante el que restaura.</returns>
    public float getTimeRestoring()
    {
        return _timeRestoring;
    }
}
