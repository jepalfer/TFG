using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sceneShopData es una clase que se utiliza para representar los datos sobre la tienda de cada escena.
/// </summary>
[System.Serializable]
public class sceneShopData
{
    /// <summary>
    /// Es el ID interno de la tienda.
    /// </summary>
    [SerializeField] private int _shopID;

    /// <summary>
    /// Es el ID de la escena en la que se encuentra la tienda.
    /// </summary>
    [SerializeField] private int _sceneID;

    /// <summary>
    /// Son las cantidades de los objetos de la tienda.
    /// </summary>
    [SerializeField] private List<int> _itemsInShop;

    /// <summary>
    /// Constructor por defecto de la clase. Inicializa <see cref="_itemsInShop"/> a una lista vacía.
    /// </summary>
    public sceneShopData()
    {
        _itemsInShop = new List<int>();
    }

    /// <summary>
    /// Método para añadir un objeto a la tienda.
    /// </summary>
    /// <param name="quantity">Cantidad del objeto.</param>
    /// <param name="shopID">ID de la tienda a la que pertenece el objeto.</param>
    /// <param name="sceneID">ID de la escena de la tienda.</param>
    public void addItem(int quantity, int shopID, int sceneID)
    {
        _shopID = shopID;
        _sceneID = sceneID;
        _itemsInShop.Add(quantity);
    }

    /// <summary>
    /// Método para la compra de un objeto. Disminuye en 1 el elemento concreto de <see cref="_itemsInShop"/>. 
    /// </summary>
    /// <param name="ID">ID del objeto comprado.</param>
    /// <param name="quantity">Cantidad del objeto comprado.</param>
    public void buyItem(int ID, int quantity)
    {
        _itemsInShop[ID] = _itemsInShop[ID] - quantity;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_itemsInShop"/>.
    /// </summary>
    /// <returns>Una lista de enteros que representa las cantidades de los objetos que quedan.</returns>
    public List<int> getItemsInShop()
    {
        return _itemsInShop;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_shopID"/>.
    /// </summary>
    /// <returns>Un int que representa el ID interno de la tienda.</returns>
    public int getShopID()
    {
        return _shopID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_sceneID"/>.
    /// </summary>
    /// <returns>Un int que representa el ID de la escena de la tienda.</returns>
    public int getSceneID()
    {
        return _sceneID;
    }
}
