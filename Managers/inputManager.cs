using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
/// <summary>
/// inputManager es una clase para el mapeado de las teclas y botones de gamepad.
/// </summary>
public class inputManager : MonoBehaviour
{
    /// <summary>
    /// Es el mapeo de string a tecla de teclado.
    /// </summary>
    private static Dictionary<string, KeyCode> _inputList;

    /// <summary>
    /// Es el mapeo de string a bot�n de gamepad.
    /// </summary>
    private static Dictionary<string, ButtonControl> _inputListgamepad;

    /// <summary>
    /// Establece si hay o no un dispositivo conectado.
    /// </summary>
    private static bool _deviceIsConnected = false;
    /// <summary>
    /// M�todo que se ejecuta el primero al iniciarse un script.
    /// Crea el mapeado de <see cref="_inputList"/> y si hay un gamepad conectado el de <see cref="_inputListgamepad"/>
    /// </summary>
    private void Awake()
    {
        _inputList = new Dictionary<string, KeyCode>();

        _inputList.Add("Jump", KeyCode.Space);
        _inputList.Add("Right", KeyCode.RightArrow);
        _inputList.Add("Left", KeyCode.LeftArrow);
        _inputList.Add("Down", KeyCode.S);
        _inputList.Add("Up", KeyCode.W);
        _inputList.Add("PrimaryAttack", KeyCode.Mouse0);
        _inputList.Add("SecundaryAttack", KeyCode.Mouse1);
        _inputList.Add("ShowMinimap", KeyCode.Tab);
        _inputList.Add("Roll", KeyCode.R);
        _inputList.Add("Pause", KeyCode.Escape);
        _inputList.Add("Interact", KeyCode.E);
        _inputList.Add("Previous", KeyCode.J);
        _inputList.Add("Next", KeyCode.K);
        _inputList.Add("Enter_equip", KeyCode.Return);
        _inputList.Add("Cancel", KeyCode.Escape);
        _inputList.Add("Accept", KeyCode.Return);

        //Comprueba si hay un gamepad conectado
        if (Gamepad.current != null)
        {
            changeDeviceConnected();
            createGamepadDefaultInputs();
        }
    }

    /// <summary>
    /// M�todo que crea el mapeado de string a bot�n de gamepad de <see cref="_inputListgamepad"/>
    /// </summary>
    public static void createGamepadDefaultInputs()
    {
        _inputListgamepad = new Dictionary<string, ButtonControl>();
        _inputListgamepad.Add("Jump", Gamepad.current.buttonSouth);
        _inputListgamepad.Add("Right", Gamepad.current.dpad.right);
        _inputListgamepad.Add("Left", Gamepad.current.dpad.left);
        _inputListgamepad.Add("Down", Gamepad.current.leftStick.down);
        _inputListgamepad.Add("Up", Gamepad.current.leftStick.up);
        _inputListgamepad.Add("PrimaryAttack", Gamepad.current.rightShoulder);
        _inputListgamepad.Add("SecundaryAttack", Gamepad.current.leftShoulder);
        _inputListgamepad.Add("ShowMinimap", Gamepad.current.selectButton);
        _inputListgamepad.Add("Roll", Gamepad.current.buttonEast);
        _inputListgamepad.Add("Pause", Gamepad.current.startButton);
        _inputListgamepad.Add("Interact", Gamepad.current.buttonWest);
        _inputListgamepad.Add("Previous", Gamepad.current.leftTrigger);
        _inputListgamepad.Add("Next", Gamepad.current.rightTrigger);
        _inputListgamepad.Add("Enter_equip", Gamepad.current.buttonSouth);
        _inputListgamepad.Add("Cancel", Gamepad.current.buttonEast);
        _inputListgamepad.Add("Accept", Gamepad.current.buttonSouth);
    }

    /// <summary>
    /// M�todo que niega <see cref="_deviceIsConnected"/>.
    /// </summary>
    public static void changeDeviceConnected()
    {
        _deviceIsConnected = !_deviceIsConnected;
    }

    /// <summary>
    /// M�todo que cambia la tecla asociada a ActionName en <see cref="_inputList"/>.
    /// </summary>
    /// <param name="ActionName">String que indica la acci�n.</param>
    /// <param name="newKey">Es la tecla de la acci�n.</param>
    public static void changeInput(string ActionName, KeyCode newKey)
    {
        _inputList[ActionName] = newKey;
    }

    /// <summary>
    /// M�todo que cambia el bot�n asociado a ActionName en <see cref="_inputListgamepad"/>.
    /// </summary>
    /// <param name="ActionName">String que indica la acci�n.</param>
    /// <param name="newButton">Es el bot�n de la acci�n.</param>
    public static void changePadInput(string ActionName, ButtonControl newButton)
    {
        _inputListgamepad[ActionName] = newButton;

    }

    /// <summary>
    /// M�todo similar a <see cref="Input.GetKey(KeyCode)"/>.
    /// </summary>
    /// <param name="action">Acci�n que queremos realizar.</param>
    /// <returns>Un booleano que indica si se est� o no pulsando la tecla que indique action</returns>
    public static bool getKey(string action)
    {
        bool gamepadControl = false;

        if (Gamepad.current != null)
        {
            gamepadControl = getGamepadAction(action).isPressed;
        }

        return Input.GetKey(getAction(action)) || gamepadControl;
    }
    /// <summary>
    /// M�todo similar a <see cref="Input.GetKeyDown(KeyCode)"/>.
    /// </summary>
    /// <param name="action">Acci�n que queremos realizar.</param>
    /// <returns>Un booleano que indica si se ha pulsado o no la tecla que indique action</returns>
    public static bool getKeyDown(string action)
    {
        bool gamepadControl = false;
        if (Gamepad.current != null)
        {
            gamepadControl = getGamepadAction(action).wasPressedThisFrame;
        }

        return Input.GetKeyDown(getAction(action)) || gamepadControl;
    }

    /// <summary>
    /// M�todo similar a <see cref="Input.GetKeyUp(KeyCode)"/>.
    /// </summary>
    /// <param name="action">Acci�n que queremos realizar.</param>
    /// <returns>Un booleano que indica si se ha dejado de pulsar o no la tecla que indique action.</returns>
    public static bool getKeyUp(string action)
    {
        bool gamepadControl = false;
        if (Gamepad.current != null)
        {
            gamepadControl = getGamepadAction(action).wasReleasedThisFrame;
        }

        return Input.GetKeyUp(getAction(action)) || gamepadControl;
    }

    /// <summary>
    /// Getter que devuelve la tecla asociada a actionName
    /// </summary>
    /// <param name="actionName">Acci�n de la que queremos saber la tecla asociada.</param>
    /// <returns>La tecla a la que est� asociada actionName.</returns>
    public static KeyCode getAction(string actionName)
    {
        return _inputList[actionName];
    }

    /// <summary>
    /// Getter que devuelve el bot�n asociado a actionName
    /// </summary>
    /// <param name="actionName">Acci�n de la que queremos saber el bot�n asociado.</param>
    /// <returns>El bot�n al que est� asociado actionName.</returns>
    public static ButtonControl getGamepadAction(string actionName)
    {
        return _inputListgamepad[actionName];
    }

    /// <summary>
    /// Getter que devuelve <see cref="_inputList"/>.
    /// </summary>
    /// <returns>Un diccionario que contiene el mapeo de string a tecla de teclado.</returns>
    public static Dictionary<string, KeyCode> getInputList()
    {
        return _inputList;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_inputListgamepad"/>.
    /// </summary>
    /// <returns>Un diccionario que contiene el mapeo de string a bot�n de gamepad.</returns>
    public static Dictionary<string, ButtonControl> getGamepadInputs()
    {
        return _inputListgamepad;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_deviceIsConnected"/>.
    /// </summary>
    /// <returns>Un booleano que indica si hay o no conectado un dispositivo.</returns>
    public static bool getDeviceConnected()
    {
        return _deviceIsConnected;
    }
}