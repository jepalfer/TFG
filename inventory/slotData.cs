using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// slotData es una clase usada para representar los objetos dentro de la UI del inventario.
/// </summary>
public class slotData : MonoBehaviour
{
    /// <summary>
    /// Cantidad de la que disponemos en inventario.
    /// </summary>
    private int _inventoryStock;

    /// <summary>
    /// Cantidad de la que disponemos en el backup.
    /// </summary>
    private int _backUpStock;

    /// <summary>
    /// Referencia al objeto que estamos representando.
    /// </summary>
    [SerializeField] private lootItem _itemReference;

    /// <summary>
    /// Referencia a la imagen de overlay que da claridad visual sobre el objeto
    /// que el usuario tiene seleccionado.
    /// </summary>
    [SerializeField] private Image _itemOverlay;

    /// <summary>
    /// Setter que modifica <see cref="_itemReference"/>.
    /// </summary>
    /// <param name="item">Objeto a asignar.</param>
    public void setLootItem(lootItem item)
    {
        _itemReference = item;
    }

    /// <summary>
    /// Setter que modifica <see cref="_inventoryStock"/>.
    /// </summary>
    /// <param name="value">Cantidad de la que disponemos en el inventario de <see cref="_itemReference"/>.</param>
    public void setInventoryStock(int value)
    {
        _inventoryStock = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_backUpStock"/>.
    /// </summary>
    /// <param name="value">Cantidad de la que disponemos en el backup de <see cref="_itemReference"/></param>
    public void setBackUpStock(int value)
    {
        _backUpStock = value;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_inventoryStock"/>.
    /// </summary>
    /// <returns><see cref="_inventoryStock"/></returns>
    public int getInventoryStock()
    {
        return _inventoryStock;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_backUpStock"/>.
    /// </summary>
    /// <returns><see cref="_backUpStock"/></returns>
    public int getBackUpStock()
    {
        return _backUpStock;
    }

    /// <summary>
    /// Getter que devuelve el render del objeto.
    /// </summary>
    /// <returns><see cref="_itemReference.getRender()"/></returns>
    public Sprite getRender()
    {
        return _itemReference.getRender();
    }
    /// <summary>
    /// Getter que devuelve la descripción del objeto.
    /// </summary>
    /// <returns><see cref="_itemReference.getDesc()"/></returns>
    public string getDescription()
    {
        return _itemReference.getDesc();
    }
    /// <summary>
    /// Getter que devuelve el tipo del objeto.
    /// </summary>
    /// <returns><see cref="_itemReference.getTipo()"/></returns>
    public itemTypeEnum getTipo()
    {
        return _itemReference.getTipo();
    }

    /// <summary>
    /// Getter que devuelve el ID interno del objeto.
    /// </summary>
    /// <returns><see cref="_itemReference.getID()"/></returns>
    public int getID()
    {
        return _itemReference.getID();
    }

    /// <summary>
    /// Getter que devuelve el overlay de la UI.
    /// </summary>
    /// <returns><see cref="_itemOverlay"/></returns>
    public Image getOverlayImage()
    {
        return _itemOverlay;
    }

    /// <summary>
    /// Getter que devuelve el nombre del objeto.
    /// </summary>
    /// <returns><see cref="_itemReference.getName()"/></returns>
    public string getName()
    {
        return _itemReference.getName();
    }
}
