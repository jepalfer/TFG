using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// serializedItemData es una clase que almacena de forma interna los datos de un objeto 
/// de forma que se puedan serializar.
/// </summary>
[System.Serializable]
public class serializedItemData
{
    /// <summary>
    /// Referencia a los datos internos del objeto.
    /// </summary>
    [SerializeField] private itemData _data;

    /// <summary>
    /// Cantidad del objeto.
    /// </summary>
    [SerializeField] private int _quantity;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="data">Datos internos a serializar.</param>
    /// <param name="quantity">Cantidad del objeto.</param>
    public serializedItemData(itemData data, int quantity)
    {
        _data = data;
        _quantity = quantity;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_data"/>.
    /// </summary>
    /// <returns><see cref="_data"/></returns>
    public itemData getData()
    {
        return _data;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_quantity"/>.
    /// </summary>
    /// <returns><see cref="_quantity"/></returns>
    public int getQuantity()
    {
        return _quantity;
    }
}
