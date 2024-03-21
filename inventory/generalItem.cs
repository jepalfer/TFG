using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// generalItem es una clase de la que heredan los distintos items.
/// </summary>
public class generalItem : MonoBehaviour
{
    /// <summary>
    /// Datos internos de un objeto general.
    /// </summary>
    [SerializeField] protected itemInstance _data;

    /// <summary>
    /// Getter que devuelve <see cref="_data"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="itemInstance"/> que contiene los datos internos del objeto.</returns>
    public itemInstance getData()
    {
        return _data;
    }

    /// <summary>
    /// Getter para devolver el tipo de objeto.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="itemTypeEnum"/> que contiene el tipo de objeto que es.</returns>
    public itemTypeEnum getTipo()
    {
        return _data.getData().getItemData().getTipo();
    }

    /// <summary>
    /// Getter para devolver el id interno del objeto.
    /// </summary>
    /// <returns>Un id que representa el id interno del objeto.</returns>
    public int getID()
    {
        return _data.getData().getItemData().getID();
    }

    /// <summary>
    /// Getter para devolver el icono de objeto.
    /// </summary>
    /// <returns>Un sprite que contiene el icono del objeto.</returns>
    public Sprite getIcon()
    {
        return _data.getData().getItemData().getIcon();
    }

    /// <summary>
    /// Getter para devolver el nombre del objeto.
    /// </summary>
    /// <returns>Un string que contiene el nombre del objeto.</returns>
    public string getName()
    {
        return _data.getData().getItemData().getName();
    }

    /// <summary>
    /// Getter para devolver la descripción del objeto.
    /// </summary>
    /// <returns>Un string que contiene la descripción del objeto.</returns>
    public string getDesc()
    {
        return _data.getData().getItemData().getDesc();
    }

    /// <summary>
    /// Getter para devolver el sprite de un render del objeto.
    /// </summary>
    /// <returns>Un render del objeto.</returns>
    public Sprite getRender()
    {
        return _data.getData().getItemData().getRender();
    }

}
