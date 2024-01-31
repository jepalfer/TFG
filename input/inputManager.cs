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
    /// Es el mapeo de string a botón de gamepad.
    /// </summary>
    private static Dictionary<string, ButtonControl> _inputListgamepad;

    /// <summary>
    /// Establece si hay o no un dispositivo conectado.
    /// </summary>
    private static bool _deviceIsConnected = false;
    /// <summary>
    /// Método que se ejecuta el primero al iniciarse un script.
    /// Crea el mapeado de <see cref="_inputList"/> y si hay un gamepad conectado el de <see cref="_inputListgamepad"/>
    /// </summary>
    private void Awake()
    {
        _inputList = new Dictionary<string, KeyCode>();

        _inputList.Add("jump", KeyCode.Space);
        _inputList.Add("right", KeyCode.RightArrow);
        _inputList.Add("left", KeyCode.LeftArrow);
        _inputList.Add("down", KeyCode.S);
        _inputList.Add("up", KeyCode.W);
        _inputList.Add("primaryAttack", KeyCode.Mouse0);
        _inputList.Add("secundaryAttack", KeyCode.Mouse1);
        _inputList.Add("showMinimap", KeyCode.Tab);
        _inputList.Add("roll", KeyCode.R);
        _inputList.Add("pause", KeyCode.Escape);
        _inputList.Add("interact", KeyCode.E);
        _inputList.Add("previous", KeyCode.J);
        _inputList.Add("next", KeyCode.K);
        _inputList.Add("enterEquip", KeyCode.Return);
        _inputList.Add("cancel", KeyCode.Escape);
        _inputList.Add("accept", KeyCode.Return);
        _inputList.Add("previousItem", KeyCode.LeftArrow);
        _inputList.Add("nextItem", KeyCode.RightArrow);
        _inputList.Add("useItem", KeyCode.DownArrow);

        //Comprueba si hay un gamepad conectado
        if (Gamepad.current != null)
        {
            changeDeviceConnected();
            createGamepadDefaultInputs();
        }
    }

    /// <summary>
    /// Método que crea el mapeado de string a botón de gamepad de <see cref="_inputListgamepad"/>
    /// </summary>
    public static void createGamepadDefaultInputs()
    {
        _inputListgamepad = new Dictionary<string, ButtonControl>();
        _inputListgamepad.Add("jump", Gamepad.current.buttonSouth);
        _inputListgamepad.Add("right", Gamepad.current.dpad.right);
        _inputListgamepad.Add("left", Gamepad.current.dpad.left);
        _inputListgamepad.Add("down", Gamepad.current.leftStick.down);
        _inputListgamepad.Add("up", Gamepad.current.leftStick.up);
        _inputListgamepad.Add("primaryAttack", Gamepad.current.rightShoulder);
        _inputListgamepad.Add("secundaryAttack", Gamepad.current.leftShoulder);
        _inputListgamepad.Add("showMinimap", Gamepad.current.selectButton);
        _inputListgamepad.Add("roll", Gamepad.current.buttonEast);
        _inputListgamepad.Add("pause", Gamepad.current.startButton);
        _inputListgamepad.Add("interact", Gamepad.current.buttonWest);
        _inputListgamepad.Add("previous", Gamepad.current.leftTrigger);
        _inputListgamepad.Add("next", Gamepad.current.rightTrigger);
        _inputListgamepad.Add("enterEquip", Gamepad.current.buttonSouth);
        _inputListgamepad.Add("cancel", Gamepad.current.buttonEast);
        _inputListgamepad.Add("accept", Gamepad.current.buttonSouth);
        _inputListgamepad.Add("previousItem", Gamepad.current.dpad.left);
        _inputListgamepad.Add("nextItem", Gamepad.current.dpad.right);
        _inputListgamepad.Add("useItem", Gamepad.current.dpad.down);
    }

    /// <summary>
    /// Método que niega <see cref="_deviceIsConnected"/>.
    /// </summary>
    public static void changeDeviceConnected()
    {
        _deviceIsConnected = !_deviceIsConnected;
    }

    /// <summary>
    /// Método que cambia la tecla asociada a ActionName en <see cref="_inputList"/>.
    /// </summary>
    /// <param name="ActionName">String que indica la acción.</param>
    /// <param name="newKey">Es la tecla de la acción.</param>
    public static void changeInput(string ActionName, KeyCode newKey)
    {
        _inputList[ActionName] = newKey;
    }

    /// <summary>
    /// Método que cambia el botón asociado a ActionName en <see cref="_inputListgamepad"/>.
    /// </summary>
    /// <param name="ActionName">String que indica la acción.</param>
    /// <param name="newButton">Es el botón de la acción.</param>
    public static void changePadInput(string ActionName, ButtonControl newButton)
    {
        _inputListgamepad[ActionName] = newButton;

    }

    /// <summary>
    /// Método similar a <see cref="Input.GetKey(KeyCode)"/>.
    /// </summary>
    /// <param name="action">Acción que queremos realizar.</param>
    /// <returns>Un booleano que indica si se está o no pulsando la tecla que indique action</returns>
    public static bool GetKey(inputEnum action)
    {
        bool gamepadControl = false;

        if (Gamepad.current != null)
        {
            gamepadControl = getGamepadAction(action.ToString()).isPressed;
        }

        return Input.GetKey(getAction(action.ToString())) || gamepadControl;
    }
    /// <summary>
    /// Método similar a <see cref="Input.GetKeyDown(KeyCode)"/>.
    /// </summary>
    /// <param name="action">Acción que queremos realizar.</param>
    /// <returns>Un booleano que indica si se ha pulsado o no la tecla que indique action</returns>
    public static bool GetKeyDown(inputEnum action)
    {
        bool gamepadControl = false;
        if (Gamepad.current != null)
        {
            gamepadControl = getGamepadAction(action.ToString()).wasPressedThisFrame;
        }

        return Input.GetKeyDown(getAction(action.ToString())) || gamepadControl;
    }

    /// <summary>
    /// Método similar a <see cref="Input.GetKeyUp(KeyCode)"/>.
    /// </summary>
    /// <param name="action">Acción que queremos realizar.</param>
    /// <returns>Un booleano que indica si se ha dejado de pulsar o no la tecla que indique action.</returns>
    public static bool GetKeyUp(inputEnum action)
    {
        bool gamepadControl = false;
        if (Gamepad.current != null)
        {
            gamepadControl = getGamepadAction(action.ToString()).wasReleasedThisFrame;
        }

        return Input.GetKeyUp(getAction(action.ToString())) || gamepadControl;
    }

    /// <summary>
    /// Getter que devuelve la tecla asociada a actionName
    /// </summary>
    /// <param name="actionName">Acción de la que queremos saber la tecla asociada.</param>
    /// <returns>La tecla a la que está asociada actionName.</returns>
    public static KeyCode getAction(string actionName)
    {
        return _inputList[actionName];
    }

    /// <summary>
    /// Getter que devuelve el botón asociado a actionName
    /// </summary>
    /// <param name="actionName">Acción de la que queremos saber el botón asociado.</param>
    /// <returns>El botón al que está asociado actionName.</returns>
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
    /// <returns>Un diccionario que contiene el mapeo de string a botón de gamepad.</returns>
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

    /// <summary>
    /// Método que comprueba si se ha pulsado una tecla de teclado.
    /// </summary>
    /// <returns>Un booleano que indica si se ha pulsado o no una tecla.</returns>
    public static bool getHasPressedKey()
    {
        return Input.anyKeyDown;
    }

    public static bool getHasPressedButton()
    {
        bool result = false;
        if (Gamepad.current != null)
        {

            Gamepad pad = Gamepad.current;

            if (pad.buttonNorth.wasPressedThisFrame || pad.buttonEast.wasPressedThisFrame || pad.buttonSouth.wasPressedThisFrame || pad.buttonWest.wasPressedThisFrame ||
                pad.dpad.up.wasPressedThisFrame || pad.dpad.right.wasPressedThisFrame || pad.dpad.down.wasPressedThisFrame || pad.dpad.left.wasPressedThisFrame ||
                pad.leftShoulder.wasPressedThisFrame || pad.leftTrigger.wasPressedThisFrame || pad.rightShoulder.wasPressedThisFrame || pad.rightTrigger.wasPressedThisFrame ||
                pad.leftStick.up.wasPressedThisFrame || pad.leftStick.right.wasPressedThisFrame || pad.leftStick.down.wasPressedThisFrame || pad.leftStick.left.wasPressedThisFrame ||
                pad.rightStick.up.wasPressedThisFrame || pad.rightStick.right.wasPressedThisFrame || pad.rightStick.down.wasPressedThisFrame || pad.rightStick.left.wasPressedThisFrame ||
                pad.leftStickButton.wasPressedThisFrame || pad.rightStickButton.wasPressedThisFrame || pad.startButton.wasPressedThisFrame || pad.selectButton.wasPressedThisFrame)
            {
                result = true;
            }
        }
        return result;
    }

}