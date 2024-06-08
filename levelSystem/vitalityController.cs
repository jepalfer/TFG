using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// vitalityController es una clase utilizada para controlar el texto relacionado con la vitalidad en la UI de
/// subir de nivel y los niveles que se van subiendo.
/// </summary>
public class vitalityController : MonoBehaviour, ISelectHandler
{
    /// <summary>
    /// Referencia al texto a modificar.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _text;
    
    /// <summary>
    /// Método que se ejecuta cada frame para modificar la lógica
    /// </summary>
    void Update()
    {
        //if (EventSystem.current.currentSelectedGameObject == gameObject)
        //{
        //    if (inputManager.GetKeyDown(inputEnum.up))
        //    {
        //        config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
        //    }

        //    if (inputManager.GetKeyDown(inputEnum.oneMoreItem))
        //    {
        //        config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
        //    }
        //}
        //Comprobamos que esté seleccionado el gameObject que tiene este script
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

            //Le damos al botón mapeado a la acción de aceptar
            if (inputManager.GetKeyDown(inputEnum.accept))
            {
                EventSystem.current.SetSelectedGameObject(UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelUpButton().gameObject);
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }
        else //No está seleccionado
        {
            _text.color = Color.white;
        }
    }

    /// <summary>
    /// Método que sobreescribe el método OnSelect de <see cref="ISelectHandler"/>.
    /// </summary>
    /// <param name="eventData">Evento que ocurre.</param>
    public void OnSelect(BaseEventData eventData)
    {
        config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
    }
}
