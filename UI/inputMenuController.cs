using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// inputMenuController es una clase que se usa para controlar el menú de controles.
/// </summary>
public class inputMenuController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Image _keyboardBackground;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Image _gamepadBackground;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private GameObject _keyboardInputs;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private GameObject _gamepadInputs;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Button _backButton;

    /// <summary>
    /// 
    /// </summary>
    private int _index;

    /// <summary>
    /// Método que se ejecuta al entrar a la UI de controles.
    /// </summary>
    public void initializeUI()
    {
        gameObject.SetActive(true);
        _index = 0;
        _keyboardBackground.enabled = true;
        _keyboardInputs.SetActive(true);

        _gamepadBackground.enabled = false;
        _gamepadInputs.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_backButton.gameObject);
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        if (inputManager.GetKeyDown(inputEnum.next) && _index == 0)
        {
            _index++;

            _keyboardBackground.enabled = false;
            _keyboardInputs.SetActive(false);

            _gamepadBackground.enabled = true;
            _gamepadInputs.SetActive(true);
            config.getAudioManager().GetComponent<menuSFXController>().playTabSFX();
        }

        if (inputManager.GetKeyDown(inputEnum.previous) && _index == 1)
        {
            _index--;

            _keyboardBackground.enabled = true;
            _keyboardInputs.SetActive(true);

            _gamepadBackground.enabled = false;
            _gamepadInputs.SetActive(false);
            config.getAudioManager().GetComponent<menuSFXController>().playTabSFX();
        }

        if (inputManager.GetKeyDown(inputEnum.cancel))
        {
            gameObject.SetActive(false);
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
        }
    }
}
