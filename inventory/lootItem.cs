using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// lootItem es una clase usada para representar internamente el loot del juego.
/// </summary>
[System.Serializable]
public class lootItem
{
    /// <summary>
    /// Referencia a los datos internos del objeto que se pueden serializar.
    /// </summary>
    [SerializeField] private generalItemSerializable _serializableItem;
    
    /// <summary>
    /// Cantidad a obtener.
    /// </summary>
    [SerializeField] private int _quantity;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="item">Datos internos del objeto que no se pueden serializar.</param>
    /// <param name="quantity">Cantidad a obtener.</param>
    public lootItem(item item, int quantity)
    {
        _serializableItem = new generalItemSerializable(item);
        _quantity = quantity;
    }

    /// <summary>
    /// Setter que modifica <see cref="_quantity"/>.
    /// </summary>
    /// <param name="value">Valor a asignar.</param>
    public void setQuantity(int value)
    {
        _quantity = value;
    }

    /// <summary>
    /// Getter que devuelve el tipo de objeto de <see cref="_serializableItem"/>.
    /// </summary>
    /// <returns><see cref="_serializableItem.getTipo()"/></returns>
    public itemTypeEnum getTipo()
    {
        return _serializableItem.getTipo();
    }

    /// <summary>
    /// Getter que devuelve el ID de <see cref="_serializableItem"/>.
    /// </summary>
    /// <returns><see cref="_serializableItem.getID()"/></returns>
    public int getID()
    {
        return _serializableItem.getID();
    }

    /// <summary>
    /// Getter que devuelve el icono de <see cref="_serializableItem"/>.
    /// </summary>
    /// <returns><see cref="_serializableItem.getIcon()"/></returns>
    public Sprite getIcon()
    {
        return _serializableItem.getIcon();
    }

    /// <summary>
    /// Getter que devuelve el render de <see cref="_serializableItem"/>.
    /// </summary>
    /// <returns><see cref="_serializableItem.getRender()"/></returns>
    public Sprite getRender()
    {
        return _serializableItem.getRender();
    }

    /// <summary>
    /// Getter que devuelve el nombre de <see cref="_serializableItem"/>.
    /// </summary>
    /// <returns><see cref="_serializableItem.getName()"/></returns>
    public string getName()
    {
        return _serializableItem.getName();
    }

    /// <summary>
    /// Getter que devuelve la descripción de <see cref="_serializableItem"/>.
    /// </summary>
    /// <returns><see cref="_serializableItem.getDesc()"/></returns>
    public string getDesc()
    {
        return _serializableItem.getDesc();
    }

    /// <summary>
    /// Getter que devuelve los datos de <see cref="_serializableItem"/>.
    /// </summary>
    /// <returns><see cref="_serializableItem.getItemData()"/></returns>
    public itemData getItemData()
    {
        return _serializableItem.getItemData();
    }

    /// <summary>
    /// Getter que devuelve la instancia de <see cref="_serializableItem"/>.
    /// </summary>
    /// <returns><see cref="_serializableItem.getInstance()"/></returns>
    public item getInstance()
    {
        return _serializableItem.getInstance();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_serializableItem"/>.
    /// </summary>
    /// <returns><see cref="_serializableItem"/></returns>
    public generalItemSerializable getItem()
    {
        return _serializableItem;
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
