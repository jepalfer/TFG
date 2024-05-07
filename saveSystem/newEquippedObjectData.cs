using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// newEquippedObjectData es una clase auxiliar usada para guardar la información de los objetos
/// equipados.
/// </summary>
[System.Serializable]
public class newEquippedObjectData
{
    /// <summary>
    /// ID del objeto equipado.
    /// </summary>
    [SerializeField] private int _itemID;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="itemID">ID a serializar del objeto.</param>
    public newEquippedObjectData(int itemID)
    {
        _itemID = itemID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_itemID"/>.
    /// </summary>
    /// <returns><see cref="_itemID"/>.</returns>
    public int getItemID()
    {
        return _itemID;
    }
}
