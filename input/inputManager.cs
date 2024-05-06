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
    private static Dictionary<string, ButtonControl> _inputListGamepad;

    /// <summary>
    /// Establece si hay o no un dispositivo conectado.
    /// </summary>
    private static bool _deviceIsConnected = false;
    /// <summary>
    /// Método que se ejecuta el primero al iniciarse un script.
    /// Crea el mapeado de <see cref="_inputList"/> y si hay un gamepad conectado el de <see cref="_inputListGamepad"/>
    /// </summary>
    private void Awake()
    {
        _inputList = new Dictionary<string, KeyCode>();

        _inputList.Add(inputEnum.jump.ToString(), KeyCode.Space);
        _inputList.Add(inputEnum.right.ToString(), KeyCode.RightArrow);
        _inputList.Add(inputEnum.left.ToString(), KeyCode.LeftArrow);
        _inputList.Add(inputEnum.down.ToString(), KeyCode.S);
        _inputList.Add(inputEnum.up.ToString(), KeyCode.W);
        _inputList.Add(inputEnum.primaryAttack.ToString(), KeyCode.Mouse0);
        _inputList.Add(inputEnum.secundaryAttack.ToString(), KeyCode.Mouse1);
        _inputList.Add(inputEnum.showMinimap.ToString(), KeyCode.Tab);
        _inputList.Add(inputEnum.roll.ToString(), KeyCode.R);
        _inputList.Add(inputEnum.pause.ToString(), KeyCode.Escape);
        _inputList.Add(inputEnum.interact.ToString(), KeyCode.E);
        _inputList.Add(inputEnum.previous.ToString(), KeyCode.J);
        _inputList.Add(inputEnum.next.ToString(), KeyCode.K);
        _inputList.Add(inputEnum.enterEquip.ToString(), KeyCode.Return);
        _inputList.Add(inputEnum.cancel.ToString(), KeyCode.Escape);
        _inputList.Add(inputEnum.accept.ToString(), KeyCode.Return);
        _inputList.Add(inputEnum.previousItem.ToString(), KeyCode.LeftArrow);
        _inputList.Add(inputEnum.nextItem.ToString(), KeyCode.RightArrow);
        _inputList.Add(inputEnum.useItem.ToString(), KeyCode.DownArrow);
        _inputList.Add(inputEnum.oneMoreItem.ToString(), KeyCode.UpArrow);
        _inputList.Add(inputEnum.oneLessItem.ToString(), KeyCode.DownArrow);
        _inputList.Add(inputEnum.buyItem.ToString(), KeyCode.Return);

        //Comprueba si hay un gamepad conectado
        if (Gamepad.current != null)
        {
            changeDeviceConnected();
            createGamepadDefaultInputs();
        }
    }

    /// <summary>
    /// Método que crea el mapeado de string a botón de gamepad de <see cref="_inputListGamepad"/>
    /// </summary>
    public static void createGamepadDefaultInputs()
    {
        _inputListGamepad = new Dictionary<string, ButtonControl>();
        _inputListGamepad.Add(inputEnum.jump.ToString(), Gamepad.current.buttonSouth);
        _inputListGamepad.Add(inputEnum.right.ToString(), Gamepad.current.dpad.right);
        _inputListGamepad.Add(inputEnum.left.ToString(), Gamepad.current.dpad.left);
        _inputListGamepad.Add(inputEnum.down.ToString(), Gamepad.current.leftStick.down);
        _inputListGamepad.Add(inputEnum.up.ToString(), Gamepad.current.leftStick.up);
        _inputListGamepad.Add(inputEnum.primaryAttack.ToString(), Gamepad.current.rightShoulder);
        _inputListGamepad.Add(inputEnum.secundaryAttack.ToString(), Gamepad.current.leftShoulder);
        _inputListGamepad.Add(inputEnum.showMinimap.ToString(), Gamepad.current.selectButton);
        _inputListGamepad.Add(inputEnum.roll.ToString(), Gamepad.current.buttonEast);
        _inputListGamepad.Add(inputEnum.pause.ToString(), Gamepad.current.startButton);
        _inputListGamepad.Add(inputEnum.interact.ToString(), Gamepad.current.buttonWest);
        _inputListGamepad.Add(inputEnum.previous.ToString(), Gamepad.current.leftTrigger);
        _inputListGamepad.Add(inputEnum.next.ToString(), Gamepad.current.rightTrigger);
        _inputListGamepad.Add(inputEnum.enterEquip.ToString(), Gamepad.current.buttonSouth);
        _inputListGamepad.Add(inputEnum.cancel.ToString(), Gamepad.current.buttonEast);
        _inputListGamepad.Add(inputEnum.accept.ToString(), Gamepad.current.buttonSouth);
        _inputListGamepad.Add(inputEnum.previousItem.ToString(), Gamepad.current.dpad.left);
        _inputListGamepad.Add(inputEnum.nextItem.ToString(), Gamepad.current.dpad.right);
        _inputListGamepad.Add(inputEnum.useItem.ToString(), Gamepad.current.dpad.down);
        _inputListGamepad.Add(inputEnum.oneMoreItem.ToString(), Gamepad.current.dpad.up);
        _inputListGamepad.Add(inputEnum.oneLessItem.ToString(), Gamepad.current.dpad.down);
        _inputListGamepad.Add(inputEnum.buyItem.ToString(), Gamepad.current.buttonSouth);
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
    /// Método que cambia el botón asociado a ActionName en <see cref="_inputListGamepad"/>.
    /// </summary>
    /// <param name="ActionName">String que indica la acción.</param>
    /// <param name="newButton">Es el botón de la acción.</param>
    public static void changePadInput(string ActionName, ButtonControl newButton)
    {
        _inputListGamepad[ActionName] = newButton;

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
        return _inputListGamepad[actionName];
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
    /// Getter que devuelve <see cref="_inputListGamepad"/>.
    /// </summary>
    /// <returns>Un diccionario que contiene el mapeo de string a botón de gamepad.</returns>
    public static Dictionary<string, ButtonControl> getGamepadInputs()
    {
        return _inputListGamepad;
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

    /// <summary>
    /// Método auxiliar para comprobar si se ha pulsado algún botón de un mando.
    /// </summary>
    /// <returns>Un flag booleano que indica si se ha pulsado un botón de un mando.</returns>
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