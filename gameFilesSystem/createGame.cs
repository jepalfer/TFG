using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class createGame : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInput;

    private void Start()
    {
        _nameInput.onEndEdit.AddListener(HandleEndEdit);
    }
    public void Accept()
    {
        string profileName = _nameInput.text;
        if (profileSystem.createProfile(profileName))
        {
            saveSystem.savePath(profileSystem.getCurrentPath(), profileName);

            statSystem.setLevel(6);
            statSystem.getVitality().setLevel(1);
            statSystem.getEndurance().setLevel(1);
            statSystem.getStrength().setLevel(1);
            statSystem.getDexterity().setLevel(1);
            statSystem.getAgility().setLevel(1);
            statSystem.getPrecision().setLevel(1);

            saveSystem.saveAttributes();

            profileIndex.addName(profileName);
            saveSystem.saveProfiles();
            SceneManager.LoadScene("ToL");
        }
        else
        {

        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void HandleEndEdit(string text)
    {
        // Tu lógica aquí cuando se completa la edición del campo de texto
        EventSystem.current.SetSelectedGameObject(_nameInput.navigation.selectOnDown.gameObject);
    }
    private void Update()
    {
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
                    Back();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }

    }
}
