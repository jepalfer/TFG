using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// createGame es una clase que sirve para crear una nueva partida.
/// </summary>
public class createGame : MonoBehaviour
{
    /// <summary>
    /// El input que va a contener el nombre de la partida.
    /// </summary>
    [SerializeField] private TMP_InputField _nameInput;

    /// <summary>
    /// Método que se ejecuta al iniciar el script.
    /// </summary>
    private void Start()
    {
        _nameInput.onEndEdit.AddListener(handleEndEdit);
    }
    /// <summary>
    /// Método que crea el perfil y carga la escena del primer nivel, ver <see cref="statSystem"/> para más información.
    /// </summary>
    public void accept()
    {
        string profileName = _nameInput.text;

        //Si se ha podido crear el perfil
        if (profileSystem.createProfile(profileName))
        {
            //Guardamos el path actual
            saveSystem.savePath(profileSystem.getCurrentPath(), profileName);

            //Ponemos atributos por defecto
            statSystem.setLevel(6);
            statSystem.getVitality().setLevel(1);
            statSystem.getEndurance().setLevel(1);
            statSystem.getStrength().setLevel(1);
            statSystem.getDexterity().setLevel(1);
            statSystem.getAgility().setLevel(1);
            statSystem.getPrecision().setLevel(1);

            //Guardamos atributos
            saveSystem.saveAttributes();

            profileIndex.addName(profileName);
            saveSystem.saveProfiles();
            SceneManager.LoadScene("ToL");
        }
        else
        {

        }
    }
    /// <summary>
    /// Método que vuelve a la escena del menú principal.
    /// </summary>
    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Método auxiliar para añadir un evento a <see cref="_nameInput"/>.
    /// </summary>
    /// <param name="text">El texto del input field.</param>
    private void handleEndEdit(string text)
    {
        EventSystem.current.SetSelectedGameObject(_nameInput.navigation.selectOnDown.gameObject);
    }
    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        //Si tenemos seleccionado el input field
        if (_nameInput.isFocused)
        {
            if (Gamepad.current != null)
            {
                if (Gamepad.current.buttonEast.wasPressedThisFrame || Gamepad.current.buttonSouth.wasPressedThisFrame || Gamepad.current.dpad.down.wasPressedThisFrame)
                {
                    _nameInput.DeactivateInputField();
                    EventSystem.current.SetSelectedGameObject(_nameInput.navigation.selectOnDown.gameObject);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _nameInput.DeactivateInputField();
                EventSystem.current.SetSelectedGameObject(_nameInput.navigation.selectOnDown.gameObject);
            }
        }
        else
        {
            if (Gamepad.current != null)
            {
                if (Gamepad.current.buttonEast.wasPressedThisFrame)
                {
                    back();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                back();
            }
        }

    }
}
