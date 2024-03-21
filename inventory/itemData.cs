using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// itemData es una clase que se usa para representar los datos internos de un objeto general.
/// </summary>
[System.Serializable]
public class itemData
{
    /// <summary>
    /// Tipo del objeto.
    /// </summary>
    [SerializeField] private itemTypeEnum _tipo;
    
    /// <summary>
    /// ID interno del objeto.
    /// </summary>
    [SerializeField] private int _itemId;

    /// <summary>
    /// Icono del objeto.
    /// </summary>
    [SerializeField] private string _icon;

    /// <summary>
    /// Render del objeto.
    /// </summary>
    [SerializeField] private string _render;

    /// <summary>
    /// Nombre del objeto.
    /// </summary>
    [SerializeField] private string _itemName;

    /// <summary>
    /// Descripción del objeto.
    /// </summary>
    [TextArea]
    [SerializeField] private string _itemDesc;

    /// <summary>
    /// Getter que devuelve <see cref="_tipo"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="itemTypeEnum"/> que contiene el tipo de objeto.</returns>
    public itemTypeEnum getTipo()
    {
        return _tipo;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_itemId"/>.
    /// </summary>
    /// <returns>Un int que representa el id interno del objeto.</returns>
    public int getID()
    {
        return _itemId;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_icon"/>.
    /// </summary>
    /// <returns>Un sprite cargado desde Resources que contiene el icono del objeto.</returns>
    public Sprite getIcon()
    {
        return Resources.Load<Sprite>(_icon);
    }

    /// <summary>
    /// Getter que devuelve <see cref="_itemName"/>.
    /// </summary>
    /// <returns>Un string que contiene el nombre del objeto.</returns>
    public string getName()
    {
        return _itemName;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_itemDesc"/>.
    /// </summary>
    /// <returns>Un string que contiene la descripción del objeto.</returns>
    public string getDesc()
    {
        return _itemDesc;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_render"/>.
    /// </summary>
    /// <returns>Un sprite cargado desde Resources que contiene el render del objeto.</returns>
    public Sprite getRender()
    {
        return Resources.Load<Sprite>(_render);
    }
}

