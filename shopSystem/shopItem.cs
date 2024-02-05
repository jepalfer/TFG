using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// shopItem es una clase que representa internamente los objetos del inventario de la tienda.
/// </summary>
[System.Serializable]
public class shopItem
{
    /// <summary>
    /// Referencia al objeto de la tienda.
    /// </summary>
    [SerializeField] private GameObject _item;

    /// <summary>
    /// int que representa la cantidad que nos queda del objeto.
    /// </summary>
    [SerializeField] private int _quantity;

    /// <summary>
    /// int que representa el precio del objeto.
    /// </summary>
    [SerializeField] private int _price;

    /// <summary>
    /// Getter que devuelve <see cref="_item"/>.
    /// </summary>
    /// <returns>GameObject que representa el objeto del inventario.</returns>
    public GameObject getItem()
    {
        return _item;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_quantity"/>.
    /// </summary>
    /// <returns>int que representa la cantidad que queda del objeto en la tienda.</returns>
    public int getQuantity()
    {
        return _quantity;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_price"/>.
    /// </summary>
    /// <returns>int que representa el precio de compra del objeto.</returns>
    public int getPrice()
    {
        return _price;
    }

    /// <summary>
    /// Setter que modifica el valor de <see cref="_quantity"/>.
    /// </summary>
    /// <param name="quantity">El valor a asignar.</param>
    public void setQuantity(int quantity)
    {
        _quantity = quantity;
    }

    /// <summary>
    /// Método que modifica <see cref="_quantity"/> disminuyéndolo en 1 ya que hemos comprado 1 objeto.
    /// </summary>
    public void buyItem()
    {
        _quantity -= 1;
    }

}
