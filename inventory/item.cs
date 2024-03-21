using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// item es una clase utiliza para almacenar los datos internos de un objeto general.
/// </summary>
public class item : ScriptableObject
{
    /// <summary>
    /// Datos internos del objeto.
    /// </summary>
    [SerializeField] private itemData _itemData;

    /// <summary>
    /// Getter que devuelve <see cref="_itemData"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="itemData"/> que contiene los datos internos del objeto.</returns>
    public itemData getItemData()
    {
        return _itemData;
    }

    /// <summary>
    /// Setter que modifica <see cref="_itemData"/>. Se usa en la creación del inventario de <see cref="inventoryManager"/>.
    /// </summary>
    /// <param name="data">Datos internos del objeto creado.</param>
    public void setItemData(itemData data)
    {
        _itemData = data;
    }

}
