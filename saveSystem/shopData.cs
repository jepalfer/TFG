using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// shopData es una clase que se usa para almacenar los datos de cada tienda del juego.
/// </summary>
[System.Serializable]
public class shopData
{
    /// <summary>
    /// Lista con la información de las distintas tiendas.
    /// </summary>
    [SerializeField] private List<sceneShopData> _shopData;

    /// <summary>
    /// Constructor por defecto de la clase. Crea una lista vacía.
    /// </summary>
    public shopData()
    {
        _shopData = new List<sceneShopData>();
    }

    /// <summary>
    /// Constructor con parámetros de la clase. Asigna una lista de tiendas a <see cref="_shopData"/>.
    /// </summary>
    /// <param name="data">Lista a asignar.</param>
    public shopData(List<sceneShopData> data)
    {
        _shopData = data;
    }

    /// <summary>
    /// Método que añade una tienda a <see cref="_shopData"/>.
    /// </summary>
    /// <param name="data">Tienda a añadir.</param>
    public void addData(sceneShopData data)
    {
        _shopData.Add(data);
    }

    /// <summary>
    /// Getter que devuelve el ID interno de la tienda de una escena concreta.
    /// </summary>
    /// <param name="sceneID">ID de la escena para la que busamos la tienda.</param>
    /// <returns><see cref="sceneShopData.getShopID()"/>.</returns>
    public int getShopID(int sceneID)
    {
        return _shopData.Find(shop => shop.getSceneID() == sceneID).getShopID();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_shopData"/>.
    /// </summary>
    /// <returns><see cref="_shopData"/>.</returns>
    public List<sceneShopData> getData()
    {
        return _shopData;
    }
}
