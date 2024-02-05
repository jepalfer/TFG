using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// shopItemSlotLogic es una clase que maneja la lógica de los slots que aparecen en la tienda.
/// </summary>
public class shopItemSlotLogic : MonoBehaviour
{
    /// <summary>
    /// Referencia al botón del objeto (para comprar)
    /// </summary>
    [SerializeField] private Button _slotButton;

    /// <summary>
    /// Referencia a la imagen del slot para poner el sprite del objeto.
    /// </summary>
    [SerializeField] private Image _slotImage;

    /// <summary>
    /// int que representa el ID interno del slot.
    /// </summary>
    private int _slotID;

    /// <summary>
    /// Getter que devuelve <see cref="_slotButton"/>.
    /// </summary>
    /// <returns>Button que representa el botón del slot.</returns>
    public Button getSlotButton()
    {
        return _slotButton;
    }

    /// <summary>
    /// Setter que modifica <see cref="_slotID"/>.
    /// </summary>
    /// <param name="ID">int que representa el valor a asignar.</param>
    public void setSlotID(int ID)
    {
        _slotID = ID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_slotID"/>.
    /// </summary>
    /// <returns>int que representa el ID interno del slot.</returns>
    public int getSlotID()
    {
        return _slotID;
    }

    /// <summary>
    /// Setter que modifica <see cref="_slotImage"/>.
    /// </summary>
    /// <param name="sprite">sprite a asignar al sprite de <see cref="_slotImage"/>.</param>
    public void setSprite(Sprite sprite)
    {
        _slotImage.sprite = sprite;
    }
}
