using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// buttonModifier es una clase que se usa para cambiar el texto del botón cuando lo seleccionamos o
/// deseleccionamos-
/// </summary>
public class buttonModifier : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    /// <summary>
    /// flag booleano que indica si el botón está o no seleccionado
    /// </summary>
    private bool _isSelected;

    /// <summary>
    /// Referencia al texto del botón.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _buttonText;
    
    /// <summary>
    /// Referencia al botón seleccionado.
    /// </summary>
    private static Button _selected;

    /// <summary>
    /// Método para cambiar el color del texto cuando seleccionamos el botón.
    /// </summary>
    /// <param name="eventData">Evento que se ha producido.</param>
    public void OnSelect(BaseEventData eventData)
    {
        setColor(Color.yellow);
        _isSelected = true;
        _selected = gameObject.transform.GetComponent<Button>();
        config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
    }


    /// <summary>
    /// Método para cambiar el color del texto cuando deseleccionamos el botón.
    /// </summary>
    /// <param name="eventData">Evento que se ha producido.</param>
    public void OnDeselect(BaseEventData eventData)
    {
        setColor(Color.white);
        _isSelected = false;

    }

    /// <summary>
    /// Setter que modifica el color del texto. 
    /// </summary>
    /// <param name="color">Color del texto a asignar.</param>
    private void setColor(Color color)
    {
        if (_buttonText != null)
        {
            _buttonText.color = color;
        }
    }
}
