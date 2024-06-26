using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// precisionController es una clase utilizada para controlar el texto relacionado con la precisi�n en la UI de
/// subir de nivel y los niveles que se van subiendo.
/// </summary>
public class precisionController : MonoBehaviour, ISelectHandler
{
    /// <summary>
    /// Referencia al texto a modificar.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _text;
    
    /// <summary>
    /// M�todo que se ejecuta cada frame para actualizar la l�gica.
    /// </summary>
    void Update()
    {
        //Comprobamos que el objeto seleccionado sea el mismo que tiene asociado este script
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            //if (inputManager.GetKeyDown(inputEnum.up))
            //{
            //    config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
            //}

            //if (inputManager.GetKeyDown(inputEnum.oneMoreItem))
            //{
            //    config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
            //}

            //if (inputManager.GetKeyDown(inputEnum.down))
            //{
            //    config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
            //}

            //if (inputManager.GetKeyDown(inputEnum.oneLessItem))
            //{
            //    config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
            //}
            //Modificamos el color del texto a uno distintivo
            _text.color = Color.cyan;

            //Restamos un nivel
            if (inputManager.GetKeyDown(inputEnum.left))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().substractPrecisionLvl();
                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getPrecisionLevels() == 0)
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getPrecisionLevel().color = Color.white;
                }

                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().allLevelsEqual0())
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelValue().color = Color.white;
                }
                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().allDamageStatsEqual0())
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getPrimaryDMG().color = Color.white;
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getSecundaryDMG().color = Color.white;
                }

            }
            //Sumamos un nivel
            if (inputManager.GetKeyDown(inputEnum.right))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().addPrecisionLvl();
                if (UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getPrecisionLevels() > 0)
                {
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getPrecisionLevel().color = Color.cyan;
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelValue().color = Color.cyan;
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getPrimaryDMG().color = Color.cyan;
                    UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getSecundaryDMG().color = Color.cyan;
                }
            }
            //Pulsamos el bot�n asociado a la acci�n mapeada a aceptar
            if (inputManager.GetKeyDown(inputEnum.accept))
            {
                EventSystem.current.SetSelectedGameObject(UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelUpButton().gameObject);
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }
        else //No es el seleccionado
        {
            _text.color = Color.white;
        }
    }
    /// <summary>
    /// M�todo que sobreescribe el m�todo OnSelect de <see cref="ISelectHandler"/>.
    /// </summary>
    /// <param name="eventData">Evento que ocurre.</param>
    public void OnSelect(BaseEventData eventData)
    {
        config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
    }
}
