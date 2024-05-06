using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// vitalityController es una clase utilizada para controlar el texto relacionado con la vitalidad en la UI de
/// subir de nivel y los niveles que se van subiendo.
/// </summary>
public class vitalityController : MonoBehaviour
{
    /// <summary>
    /// Referencia al texto a modificar.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _text;
    
    /// <summary>
    /// M�todo que se ejecuta cada frame para modificar la l�gica
    /// </summary>
    void Update()
    {
        //Comprobamos que est� seleccionado el gameObject que tiene este script
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            //Le ponemos el color a uno distintivo
            _text.color = Color.cyan;

            //Restamos un nivel
            if (inputManager.GetKeyDown(inputEnum.left))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().substractVitalityLvl();

                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getVitalityLevels() == 0)
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getVitalityLevel().color = Color.white;
                }

                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().allLevelsEqual0())
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelValue().color = Color.white;
                }
            }

            //Sumamos un nivel
            if (inputManager.GetKeyDown(inputEnum.right))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().addVitalityLvl();

                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getVitalityLevels() > 0)
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getVitalityLevel().color = Color.cyan;
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelValue().color = Color.cyan;
                }
            }

            //Le damos al bot�n mapeado a la acci�n de aceptar
            if (inputManager.GetKeyDown(inputEnum.accept))
            {
                EventSystem.current.SetSelectedGameObject(UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelUpButton().gameObject);
            }
        }
        else //No est� seleccionado
        {
            _text.color = Color.white;
        }
    }
}
