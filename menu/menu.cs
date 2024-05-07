using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// menu es una clase utilizada para gestionar todo lo relativo al men� principal.
/// </summary>
public class menu : MonoBehaviour
{
    /// <summary>
    /// Referencia al men� de opciones.
    /// </summary>
    [SerializeField] private GameObject _optionsMenu;

    /// <summary>
    /// Referencia al men� principal.
    /// </summary>
    [SerializeField] private GameObject _mainMenu;

    /// <summary>
    /// Referencia al bot�n de comenzar partida.
    /// </summary>
    [SerializeField] private Button _newGameButton;

    /// <summary>
    /// Referencia al bot�n de continuar partida.
    /// </summary>
    [SerializeField] private Button _continueButton;

    /// <summary>
    /// Referencia al bot�n de carga de perfiles.
    /// </summary>
    [SerializeField] private Button _loadButton;

    /// <summary>
    /// Referencia al bot�n para acceder al men� de opciones.
    /// </summary>
    [SerializeField] private Button _optionsButton;

    /// <summary>
    /// Referencia al bot�n para ver los inputs.
    /// </summary>
    [SerializeField] private Button _inputButton;

    /// <summary>
    /// Referencia al dropdown de resoluciones.
    /// </summary>
    private TMP_Dropdown _resolutionDropdown;

    /// <summary>
    /// Referencia al dropdown de visualizaci�n.
    /// </summary>
    [SerializeField] private TMP_Dropdown _displayDropdown;

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
    /// Referencia al bot�n de aceptar cambios.
    /// </summary>
    [SerializeField] private Button _acceptButton;

    /// <summary>
    /// Referencia al bot�n de cancelar cambios.
    /// </summary>
    [SerializeField] private Button _cancelButton;

    /// <summary>
    /// Referencia al texto de la opci�n de resoluciones.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _resolutionText;

    /// <summary>
    /// Referencia al texto de la opci�n de visualizaci�n.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _displayText;

    /// <summary>
    /// Referencia al texto del slider de volumen master.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _masterText;

    /// <summary>
    /// Referencia al texto del slider de volumen de OST.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _OSTText;

    /// <summary>
    /// Referencia al texto del slider del volumen de SFX.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _SFXText;

    /// <summary>
    /// Referencia al texto del bot�n de aceptar.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _acceptText;

    /// <summary>
    /// Referencia al texto del bot�n de cancelar cambios.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _cancelText;

    /// <summary>
    /// Timer para controlar el tiempo que ha pasado desde que hemos entrado al men� de opciones
    /// para que no se seleccione el bot�n de aceptar.
    /// </summary>
    private float _timer;

    /// <summary>
    /// flag booleano para dejar de aumentar el timer.
    /// </summary>
    private bool _canCount;

    /// <summary>
    /// Referencia a la configuraci�n de audio serializada.
    /// </summary>
    private audioSettingsData _audioSettings;

    /// <summary>
    /// Referencia a la UI para confirmar la cancelaci�n de cambios de configuraci�n.
    /// </summary>
    [SerializeField] private GameObject _confirmChangesUI;

    /// <summary>
    /// M�todo que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {

        //Asignamos variables
        _timer = 0.0f;
        _canCount = false;

        //Bloqueamos el cursor
        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;*/
        
        //Cargamos los datos
        profileData data = saveSystem.loadProfiles();

        if (data == null)
        {

        }
        else
        {
            //Cargamos los perfiles
            profileIndex.setNames(data.getUserNames());

            //Cargamos el �ltimo perfil jugado
            if (profileIndex.getUserNames().Count > 0)
            {
                lastPath path = saveSystem.loadPath();

                if (Directory.Exists(path.getPath()))
                {
                    profileSystem.setProfileName(path.getName());
                    profileSystem.setCurrentPath(path.getPath());
                    _continueButton.interactable = true;
                }
                else
                {
                    _continueButton.interactable = false;
                }
            }
            else
            {
                _continueButton.interactable = false;
            }
        }

        //Modificamos la navegaci�n de los botones correspondientes
        Navigation newGameNavigation = _newGameButton.navigation;
        Navigation continueNavigation = _continueButton.navigation;
        Navigation loadGameNavigation = _loadButton.navigation;
        if (_continueButton.interactable)
        {
            newGameNavigation.selectOnDown = _continueButton;

            continueNavigation.selectOnUp = _newGameButton;
            continueNavigation.selectOnDown = _loadButton;

            loadGameNavigation.selectOnUp = _continueButton;
        }
        else
        {

            newGameNavigation.selectOnDown = _loadButton;

            loadGameNavigation.selectOnUp = _newGameButton;
        }

        _newGameButton.navigation = newGameNavigation;
        _continueButton.navigation = continueNavigation;
        _loadButton.navigation = loadGameNavigation;
    }

    /// <summary>
    /// M�todo que se ejecuta al inicio del script tras el <see cref="Awake()"/>.
    /// </summary>
    private void Start()
    {
        _audioSettings = saveSystem.loadAudioSettings();
        _resolutionDropdown = GetComponent<resolutionController>().getResolutionDropdown();
    }

    /// <summary>
    /// M�todo para entrar en la creaci�n de un nuevo perfil.
    /// </summary>
    public void createGame()
    {
        SceneManager.LoadScene("newGame");
    }

    /// <summary>
    /// M�todo para entrar en la selecci�n de perfiles.
    /// </summary>
    public void loadGame()
    {
        SceneManager.LoadScene("loadGame");
    }

    /// <summary>
    /// M�todo para cerrar el juego.
    /// </summary>
    public void quitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// M�todo para mostrar el men� de configuraci�n de opciones.
    /// </summary>
    public void showOptions()
    {
        _timer = 0.0f;
        //Mostramos la UI
        _optionsMenu.SetActive(!_optionsMenu.activeSelf);
        _mainMenu.SetActive(!_mainMenu.activeSelf);

        //Si el men� se est� mostrando
        if (_optionsMenu.activeSelf)
        {
            //Modificamos los colores para claridad visual y por si se ha quedado de otro color
            _cancelText.color = Color.white;
            _acceptText.color = Color.white;
            _canCount = true;
            //Cargamos la configuraci�n de audio
            _audioSettings = saveSystem.loadAudioSettings();

            //EventSystem.current.SetSelectedGameObject(_resolutionDropdown.gameObject);
            EventSystem.current.SetSelectedGameObject(_masterSlider.gameObject);

            //Modificamos el valor de cada slider
            _masterSlider.value = _audioSettings.getMasterVolume();
            _OSTSlider.value = _audioSettings.getOSTVolume();
            _SFXSlider.value = _audioSettings.getSFXVolume();
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(_optionsButton.gameObject);
        }
    }

    /// <summary>
    /// M�todo para cancelar los cambios.
    /// </summary>
    public void cancelOptions()
    {
        //Si hay cambios
        if (saveSystem.loadAudioSettings().getMasterVolume() != _masterSlider.value ||
            saveSystem.loadAudioSettings().getOSTVolume() != _OSTSlider.value ||
            saveSystem.loadAudioSettings().getSFXVolume() != _SFXSlider.value)
        {
            _confirmChangesUI.SetActive(!_confirmChangesUI.activeSelf);
            _confirmChangesUI.GetComponent<confirmOptionsChange>().startUI();
        }
        else //Si no hay cambios
        {
            config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.masterVolume.ToString(),
                                                                           saveSystem.loadAudioSettings().getMasterVolume());

            config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.OSTVolume.ToString(),
                                                                           saveSystem.loadAudioSettings().getOSTVolume());

            config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.SFXVolume.ToString(),
                                                                           saveSystem.loadAudioSettings().getSFXVolume());
            showOptions();
        }
    }

    /// <summary>
    /// M�todo auxiliar para cambiar el volumen master.
    /// </summary>
    public void changeMasterVolume()
    {
        config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.masterVolume.ToString(),
                                                                       _masterSlider.value);
    }

    /// <summary>
    /// M�todo auxiliar para cambiar el volumen de OST.
    /// </summary>
    public void changeOSTVolume()
    {
        config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.OSTVolume.ToString(),
                                                                       _OSTSlider.value);
    }

    /// <summary>
    /// M�todo auxiliar para cambiar el volumen de SFX.
    /// </summary>
    public void changeSFXVolume()
    {
        config.getAudioManager().GetComponent<audioManager>().setAudio(audioSettingsEnum.SFXVolume.ToString(),
                                                                       _SFXSlider.value);
    }

    /// <summary>
    /// M�todo para aceptar los cambios.
    /// </summary>
    public void acceptOptions()
    {
        saveSystem.saveAudioSettings(_masterSlider.value, _OSTSlider.value, _SFXSlider.value);
        Debug.Log("accept options sliders " + _masterSlider.value + " " + _OSTSlider.value + " " + _SFXSlider.value);

        changeMasterVolume();
        changeOSTVolume();
        changeSFXVolume();
        showOptions();
    }

    /// <summary>
    /// M�todo para continuar la partida del �ltimo perfil cargado.
    /// </summary>
    public void continueGame()
    {
        lastSceneData data = saveSystem.loadLastSceneData();
        SceneManager.LoadScene(data.getSceneID());
    }

    /// <summary>
    /// M�todo que se ejecuta cada frame para actualizar la l�gica.
    /// </summary>
    private void Update()
    {
        //Contamos el tiempo transcurrido
        if (_canCount)
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= 0.05f)
        {
            _canCount = false;
        }

        //Si estamos mostrando el men� de opciones y no el de cancelar cambios
        if (_optionsMenu.activeSelf && !_confirmChangesUI.activeSelf)
        {
            //Si pulsamos el bot�n de cancelar
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                cancelOptions();
            }

            //Si pulsamos el bot�n de aceptar y adem�s el timer ha pasado un umbral
            if (inputManager.GetKeyDown(inputEnum.accept) && _timer >= 0.05f)
            {
                if (EventSystem.current.currentSelectedGameObject == _masterSlider.gameObject ||
                    EventSystem.current.currentSelectedGameObject == _OSTSlider.gameObject ||
                    EventSystem.current.currentSelectedGameObject == _SFXSlider.gameObject ||
                    EventSystem.current.currentSelectedGameObject == _inputButton.gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(_acceptButton.gameObject);
                }
            }

            //Modificamos los colores de los textos seg�n el gameObject seleccionado
            if (EventSystem.current.currentSelectedGameObject == _resolutionDropdown.gameObject)
            {
                _resolutionText.color = Color.yellow;
                _displayText.color = Color.white;
                _masterText.color = Color.white;
                _OSTText.color = Color.white;
                _SFXText.color = Color.white;
            }
            else if (EventSystem.current.currentSelectedGameObject == _displayDropdown.gameObject)
            {
                _resolutionText.color = Color.white;
                _displayText.color = Color.yellow;
                _masterText.color = Color.white;
                _OSTText.color = Color.white;
                _SFXText.color = Color.white;
            }
            else if (EventSystem.current.currentSelectedGameObject == _masterSlider.gameObject)
            {
                _resolutionText.color = Color.white;
                _displayText.color = Color.white;
                _masterText.color = Color.yellow;
                _OSTText.color = Color.white;
                _SFXText.color = Color.white;
            }
            else if (EventSystem.current.currentSelectedGameObject == _OSTSlider.gameObject)
            {
                _resolutionText.color = Color.white;
                _displayText.color = Color.white;
                _masterText.color = Color.white;
                _OSTText.color = Color.yellow;
                _SFXText.color = Color.white;
            }
            else if (EventSystem.current.currentSelectedGameObject == _SFXSlider.gameObject)
            {
                _resolutionText.color = Color.white;
                _displayText.color = Color.white;
                _masterText.color = Color.white;
                _OSTText.color = Color.white;
                _SFXText.color = Color.yellow;
            }
            else
            {
                _resolutionText.color = Color.white;
                _displayText.color = Color.white;
                _masterText.color = Color.white;
                _OSTText.color = Color.white;
                _SFXText.color = Color.white;
            }

        }
    }

}
