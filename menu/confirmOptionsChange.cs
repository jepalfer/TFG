using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// confirmOptionChange es una clase que se utiliza para gestionar la confirmación de la cancelación
/// de los cambios en la configuración.
/// </summary>
public class confirmOptionsChange : MonoBehaviour
{
    /// <summary>
    /// Referencia al botón de seguir cambiando.
    /// </summary>
    [SerializeField] private Button _changeButton;

    /// <summary>
    /// Referencia al botón del menú de pausa con el que se accedió al menú de opciones.
    /// </summary>
    [SerializeField] private Button _optionsButton;

    /// <summary>
    /// Referencia a la UI del menú de opciones.
    /// </summary>
    [SerializeField] private GameObject _optionsMenu;

    /// <summary>
    /// Referencia a la UI del menú principal.
    /// </summary>
    [SerializeField] private GameObject _mainMenu;

    /// <summary>
    /// Referencia al slider de volumen master.
    /// </summary>
    [SerializeField] private Slider _masterSlider;

    /// <summary>
    /// Referencia al texto del botón de cancelar.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _cancelText;
    
    /// <summary>
    /// Método que inicializa la UI.
    /// </summary>
    public void startUI()
    {
        EventSystem.current.SetSelectedGameObject(_changeButton.gameObject);
        _cancelText.color = Color.white;
    }

    /// <summary>
    /// Método que vuelve al menú de opciones.
    /// </summary>
    public void keepChanging()
    {
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_masterSlider.gameObject);
    }

    /// <summary>
    /// Método que vuelve al menú de pausa/menú principal.
    /// </summary>
    public void exitChanges()
    {
        _optionsMenu.SetActive(false);
        gameObject.SetActive(false);
        if (_mainMenu)
        {
            _mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_optionsButton.gameObject);
        }
        config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.masterVolume.ToString(), 
                                                                       saveSystem.loadAudioSettings().getMasterVolume());
        config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.OSTVolume.ToString(), 
                                                                       saveSystem.loadAudioSettings().getOSTVolume());
        config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.SFXVolume.ToString(),
                                                                       saveSystem.loadAudioSettings().getSFXVolume());
    }
}
