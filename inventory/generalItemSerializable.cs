using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generalItemSerializable es una clase usada para serializar la información de los objetos obtenidos.
/// </summary>
[System.Serializable]
public class generalItemSerializable
{
    /// <summary>
    /// Objeto a serializar.
    /// </summary>
    [SerializeField] private item _instance;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="data">Es el objeto obtenido para serializar.</param>
    public generalItemSerializable(item data)
    {
        _instance = data;
    }


    /// <summary>
    /// Getter para devolver el tipo de objeto.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="itemTypeEnum"/> que contiene el tipo de objeto que es.</returns>
    public itemTypeEnum getTipo()
    {
        return _instance.getItemData().getTipo();
    }

    /// <summary>
    /// Getter para devolver el id interno del objeto.
    /// </summary>
    /// <returns>Un id que representa el id interno del objeto.</returns>
    public int getID()
    {
        return _instance.getItemData().getID();
    }

    /// <summary>
    /// Getter para devolver el icono de objeto.
    /// </summary>
    /// <returns>Un sprite que contiene el icono del objeto.</returns>
    public Sprite getIcon()
    {
        return _instance.getItemData().getIcon();
    }
    /// <summary>
    /// Getter para devolver el sprite de un render del objeto.
    /// </summary>
    /// <returns>Un render del objeto.</returns>
    public Sprite getRender()
    {
        return _instance.getItemData().getRender();
    }


    /// <summary>
    /// Getter para devolver el nombre del objeto.
    /// </summary>
    /// <returns>Un string que contiene el nombre del objeto.</returns>
    public string getName()
    {
        return _instance.getItemData().getName();
    }


    /// <summary>
    /// Getter para devolver la descripción del objeto.
    /// </summary>
    /// <returns>Un string que contiene la descripción del objeto.</returns>
    public string getDesc()
    {
        return _instance.getItemData().getDesc();
    }

    /// <summary>
    /// Getter que devuelve los datos del objeto que estamos serializando.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="itemData"/> que c ontiene los datos del objeto.</returns>
    public itemData getItemData()
    {
        return _instance.getItemData();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_instance"/>.
    /// </summary>
    /// <returns>El objeto que estamos serializando.</returns>
    public item getInstance()
    {
        return _instance;
    }

}
