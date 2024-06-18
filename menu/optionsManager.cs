using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// optionsManager es una clase que se encarga de gestionar la configuración de las opciones.
/// </summary>
public class optionsManager : MonoBehaviour
{
    /// <summary>
    /// Referencia al botón de menú para ver los inputs del juego.
    /// </summary>
    [SerializeField] private Button _inputButton;

    /// <summary>
    /// Referencia al slider de volumen master.
    /// </summary>
    [SerializeField] private Slider _masterSlider;

    /// <summary>
    /// Referencia al slider de volumen de OST.
    /// </summary>
    [SerializeField] private Slider _OSTSlider;

    /// <summary>
    /// Referencia al slider de volumen de SFX.
    /// </summary>
    [SerializeField] private Slider _SFXSlider;

    /// <summary>
    /// Referencia al botón de aceptar cambios.
    /// </summary>
    [SerializeField] private Button _acceptButton;

    /// <summary>
    /// Referencia al botón de cancelar cambios.
    /// </summary>
    [SerializeField] private Button _cancelButton;

    /// <summary>
    /// Referencia al dropdown de resoluciones.
    /// </summary>
    [SerializeField] private TMP_Dropdown _resDropdown;

    /// <summary>
    /// Referencia al dropdown de displays.
    /// </summary>
    [SerializeField] private TMP_Dropdown _displayDropdown;
    
    /// <summary>
    /// Referencia al dropdown de calidades.
    /// </summary>
    [SerializeField] private TMP_Dropdown _qualityDropdown;

    /// <summary>
    /// Referencia al texto que indica que es el volumen master.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _masterText;

    /// <summary>
    /// Referencia al texto que indica que es el volumen de OST.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _OSTText;

    /// <summary>
    /// Referencia al texto que indica que es el volumen de SFX.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _SFXText;

    /// <summary>
    /// Referencia al texto que indica que es el dropdown de resoluciones.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _resText;

    /// <summary>
    /// Referencia al texto que indica que es el dropdown de display.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _displayText;

    /// <summary>
    /// Referencia al texto que indica que es el dropdown de calidades.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _qualityText;

    /// <summary>
    /// Referencia al texto de <see cref="_acceptButton"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _acceptText;

    /// <summary>
    /// Referencia al texto de <see cref="_cancelButton"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _cancelText;

    /// <summary>
    /// Referencia a la UI para confirmar la cancelación de cambios.
    /// </summary>
    [SerializeField] private GameObject _confirmUI;

    /// <summary>
    /// Referencia a los datos de configuración de audio serializados.
    /// </summary>
    private audioSettingsData _audioSettings;

    /// <summary>
    /// Referencia al último objeto seleccionado.
    /// </summary>
    private GameObject _formerEventSystemSelected = null;

    /// <summary>
    /// Referencia al menú de controles.
    /// </summary>
    [SerializeField] private GameObject _inputsMenu;

    /// <summary>
    /// Referencia al contenido del menú de opciones.
    /// </summary>
    [SerializeField] private GameObject _optionsContent;

    /// <summary>
    /// Método que se encarga de inicializar la UI.
    /// </summary>
    public void initializeUI()
    {
        //cambiamos colores de texto
        _cancelText.color = Color.white;
        _acceptText.color = Color.white;

        //Cargamos la configuración de audio
        _audioSettings = saveSystem.loadAudioSettings();

        //Modificamos los valores
        //EventSystem.current.SetSelectedGameObject(_masterSlider.gameObject);

        _resDropdown.gameObject.transform.parent.GetComponent<resolutionController>().initializeOptions();
        _displayDropdown.gameObject.transform.parent.GetComponent<displayController>().initializeOptions();
        _qualityDropdown.gameObject.transform.parent.GetComponent<qualityController>().initializeOptions();
        EventSystem.current.SetSelectedGameObject(_resDropdown.gameObject);
        _masterSlider.value = _audioSettings.getMasterVolume();
        _OSTSlider.value = _audioSettings.getOSTVolume();
        _SFXSlider.value = _audioSettings.getSFXVolume();
        
        //Modificamos el volumen
        changeMasterVolume();
        changeOSTVolume();
        changeSFXVolume();
        _formerEventSystemSelected = null;
    }

    /// <summary>
    /// Método para aceptar los cambios.
    /// </summary>
    public void acceptChanges()
    {
        //Guardamos los cambios
        saveSystem.saveAudioSettings(_masterSlider.value, _OSTSlider.value, _SFXSlider.value);
        
        //Modificamos los valores de volumen
        changeMasterVolume();
        changeOSTVolume();
        changeSFXVolume();
        
        //Llamamos al método correspondiente para gestionar la UI
        UIConfig.getController().useOptionsUI();
    }

    /// <summary>
    /// Método para cancelar los cambios.
    /// </summary>
    public void cancelChanges()
    {
        //Si hay cambios
        if (saveSystem.loadAudioSettings().getMasterVolume() != _masterSlider.value ||
            saveSystem.loadAudioSettings().getOSTVolume() != _OSTSlider.value ||
            saveSystem.loadAudioSettings().getSFXVolume() != _SFXSlider.value)
        {
            //Mostramos la UI 
            _confirmUI.SetActive(!_confirmUI.activeSelf);
            _formerEventSystemSelected = null;
            _confirmUI.GetComponent<confirmOptionsChange>().startUI();
        }
        else
        {
            //Salimos de la configuración de audio
            config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.masterVolume.ToString(),
                                                                           saveSystem.loadAudioSettings().getMasterVolume());

            config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.OSTVolume.ToString(),
                                                                           saveSystem.loadAudioSettings().getOSTVolume());

            config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.SFXVolume.ToString(),
                                                                           saveSystem.loadAudioSettings().getSFXVolume());
            UIConfig.getController().useOptionsUI();
        }
    }

    /// <summary>
    /// Método auxiliar que se encarga de cambiar el volumen master.
    /// </summary>
    public void changeMasterVolume()
    {
        config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.masterVolume.ToString(), 
                                                                       _masterSlider.value);
        if (_formerEventSystemSelected == _masterSlider.gameObject)
        {
            config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
        }
    }

    /// <summary>
    /// Método auxiliar que se encarga de cambiar el volumen de OST.
    /// </summary>
    public void changeOSTVolume()
    {
        config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.OSTVolume.ToString(),
                                                                       _OSTSlider.value);
        if (_formerEventSystemSelected == _OSTSlider.gameObject)
        {
            config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
        }
    }

    /// <summary>
    /// Método auxiliar que se encarga de cambiar el volumen de SFX.
    /// </summary>
    public void changeSFXVolume()
    {
        config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.SFXVolume.ToString(),
                                                                       _SFXSlider.value);
        if (_formerEventSystemSelected == _SFXSlider.gameObject)
        {
            config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
        }
    }

    /// <summary>
    /// Método para mostrar el menú de controles.
    /// </summary>
    public void showInputs()
    {
        _inputsMenu.GetComponent<inputMenuController>().initializeUI();
        _optionsContent.SetActive(false);
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar su lógica.
    /// </summary>
    private void Update()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        if (currentSelected != _formerEventSystemSelected)
        {
            if (_formerEventSystemSelected != null)
            {
                Debug.Log("he");
                config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
            }
            _formerEventSystemSelected = currentSelected;
        }

        //Se ha pulsado el botón de cancelar
        if (inputManager.GetKeyDown(inputEnum.cancel) && !_resDropdown.IsExpanded &&
            !_qualityDropdown.IsExpanded && !_displayDropdown.IsExpanded && !_inputsMenu.active)
        {
            Debug.Log("hh");
            cancelChanges();
        }

        if (_inputsMenu.active && inputManager.GetKeyDown(inputEnum.cancel))
        {
            _optionsContent.SetActive(true);
            _inputsMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(_inputButton.gameObject);
        }

        //Se ha pulsado el botón de aceptar
        if (inputManager.GetKeyDown(inputEnum.accept))
        {
            //Cambiamos el objeto seleccionado si tenemos seleccionada cualquier opción
            if (EventSystem.current.currentSelectedGameObject == _masterSlider.gameObject ||
                EventSystem.current.currentSelectedGameObject == _OSTSlider.gameObject ||
                EventSystem.current.currentSelectedGameObject == _SFXSlider.gameObject ||
                EventSystem.current.currentSelectedGameObject == _inputButton.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(_acceptButton.gameObject);
            }
        }

        //Modificación de color de los textos
        if (EventSystem.current.currentSelectedGameObject == _resDropdown.gameObject)
        {
            _resText.color = Color.yellow;
            _masterText.color = Color.white;
            _qualityText.color = Color.white;
        }
        else if (EventSystem.current.currentSelectedGameObject == _qualityDropdown.gameObject)
        {
            _resText.color = Color.white;
            _masterText.color = Color.white;
            _qualityText.color = Color.yellow;
            _displayText.color = Color.white;
        }
        else if (EventSystem.current.currentSelectedGameObject == _displayDropdown.gameObject)
        {
            _displayText.color = Color.yellow;
            _qualityText.color = Color.white;
            _masterText.color = Color.white;
        }
        else if (EventSystem.current.currentSelectedGameObject == _masterSlider.gameObject)
        {
            _masterText.color = Color.yellow;
            _resText.color = Color.white;
            _displayText.color = Color.white;
            _OSTText.color = Color.white;
            _qualityText.color = Color.white;
        }
        else if (EventSystem.current.currentSelectedGameObject == _OSTSlider.gameObject)
        {
            _masterText.color = Color.white;
            _OSTText.color = Color.yellow;
            _SFXText.color = Color.white;
        }
        else if (EventSystem.current.currentSelectedGameObject == _SFXSlider.gameObject)
        {
            _masterText.color = Color.white;
            _OSTText.color = Color.white;
            _SFXText.color = Color.yellow;
        }
        else
        {
            _masterText.color = Color.white;
            _OSTText.color = Color.white;
            _SFXText.color = Color.white;
            _displayText.color = Color.white;
            _resText.color = Color.white;
        }

    }
}
