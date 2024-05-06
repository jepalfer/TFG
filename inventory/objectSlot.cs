using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// objectSlot es una clase que se usa para representar el slot en el que equipas un objeto concreto.
/// </summary>
public class objectSlot : MonoBehaviour
{
    /// <summary>
    /// El botón asociado al slot.
    /// </summary>
    [SerializeField] private Button _associatedButton;

    /// <summary>
    /// El ID interno del slot.
    /// </summary>
    [SerializeField] private int _slotID;

    /// <summary>
    /// Referencia a la imagen que sirve de overlay.
    /// </summary>
    [SerializeField] private GameObject _overlayImage;

    /// <summary>
    /// Getter que devuelve <see cref="_associatedButton"/>.
    /// </summary>
    /// <returns>Un botón que es el asociado al slot.</returns>
    public Button getAssociatedButton()
    {
        return _associatedButton;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_overlayImage"/>.
    /// </summary>
    /// <returns>Una <see cref="GameObject"/> que contiene una referencia a la imagen de overlay.</returns>
    public GameObject getOverlayImage()
    {
        return _overlayImage;
    }

    /// <summary>
    /// Setter para modificar <see cref="_slotID"/>.
    /// </summary>
    /// <param name="ID">Int que representa el ID interno del slot.</param>
    public void setID(int ID)
    {
        _slotID = ID;
    }

    public void equipObject()
    {
        config.getPlayer().GetComponent<equippedInventory>().equipObject(_slotID, UIConfig.getController().getLastSelected().GetComponent<slotData>().getID());
        UIConfig.getController().useEquippingObjectUI();
    }
}
