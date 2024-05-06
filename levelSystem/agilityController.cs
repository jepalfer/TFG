using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// agilityController es una clase utilizada para controlar el texto relacionado con la agilidad en la UI de
/// subir de nivel y los niveles que se van subiendo.
/// </summary>
public class agilityController : MonoBehaviour
{
    /// <summary>
    /// Referencia al texto a modificar.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _text;

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        //Si el objeto seleccionado es el mismo que tiene asociado este script
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            //Modificamos el color del texto a uno distintivo
            _text.color = Color.cyan;
            
            //Restamos un nivel
            if (inputManager.GetKeyDown(inputEnum.left))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().substractAgilityLvl();

                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getAgilityLevels() == 0)
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getAgilityLevel().color = Color.white;
                }

                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().allLevelsEqual0())
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelValue().color = Color.white;
                }

            }

            //Sumamos un nivel
            if (inputManager.GetKeyDown(inputEnum.right))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().addAgilityLvl();

                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getAgilityLevels() > 0)
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getAgilityLevel().color = Color.cyan;
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelValue().color = Color.cyan;
                }
            }

            //Pulsamos el botón asociado a la acción de aceptar
            if (inputManager.GetKeyDown(inputEnum.accept))
            {
                EventSystem.current.SetSelectedGameObject(UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelUpButton().gameObject);
            }
        }
        else //No es el seleccionado
        {
            _text.color = Color.white;
        }
    }
}
