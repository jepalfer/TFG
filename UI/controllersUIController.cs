using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// controllersUIController es una clase que se encarga de mostrar la imagen correspondiente al (des)enchufar un mando.
/// </summary>
public class controllersUIController: MonoBehaviour
{
    /// <summary>
    /// Referencia a la imagen cuando se conecta un mando default.
    /// </summary>
    [SerializeField] private Image _blackPS;

    /// <summary>
    /// Referencia a la imagen cuando se desconecta un mando default.
    /// </summary>
    [SerializeField] private Image _whitePS;

    /// <summary>
    /// Referencia a la imagen cuando se conecta un mando de nintendo.
    /// </summary>
    [SerializeField] private Image _blackNS;

    /// <summary>
    /// Referencia a la imagen cuando se desconecta un mando de nintendo.
    /// </summary>
    [SerializeField] private Image _whiteNS;

    /// <summary>
    /// Referencia a la imagen cuando se conecta un mando de xbox.
    /// </summary>
    [SerializeField] private Image _blackXB;

    /// <summary>
    /// Referencia a la imagen cuando se desconecta un mando de xbox.
    /// </summary>
    [SerializeField] private Image _whiteXB;

    /// <summary>
    /// Diccionario para mostrar de una forma más simple las imágenes de conexión.
    /// </summary>
    private Dictionary<controllerIDEnum, Image> _blackImages;

    /// <summary>
    /// Diccionario para most4rar de una forma más simple las imágenes de desconexión.
    /// </summary>
    private Dictionary<controllerIDEnum, Image> _whiteImages;

    /// <summary>
    /// Tiempo que dura la imagen en pantalla.
    /// </summary>
    private float _showTime = 0.75f;

    /// <summary>
    /// Número total de mandos conectados.
    /// </summary>
    private static int _connectedControllers = 0;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        //Inicializamos y rellenamos los diccionarios
        _blackImages = new Dictionary<controllerIDEnum, Image>();
        _whiteImages = new Dictionary<controllerIDEnum, Image>();

        _blackImages[controllerIDEnum.PS] = _blackPS;
        _blackImages[controllerIDEnum.XBOX] = _blackXB;
        _blackImages[controllerIDEnum.NINTENDO] = _blackNS;

        _whiteImages[controllerIDEnum.PS] = _whitePS;
        _whiteImages[controllerIDEnum.XBOX]= _whiteXB;
        _whiteImages[controllerIDEnum.NINTENDO] = _whiteNS;
    }

    /// <summary>
    /// Método que se ejecuta al conectar un mando.
    /// </summary>
    private void OnEnable()
    {
        InputSystem.onDeviceChange += detectController;
    }

    /// <summary>
    /// Método que se ejecuta al desconectar un mando.
    /// </summary>
    private void OnDisable()
    {
        InputSystem.onDeviceChange -= detectController;
    }

    /// <summary>
    /// Método que se ejecuta al detectar un cambio de cambio de dispositivo.
    /// </summary>
    /// <param name="device">Dispositivo (des)conectado.</param>
    /// <param name="deviceChange">Cambio en el dispositivo.</param>
    private void detectController(InputDevice device, InputDeviceChange deviceChange)
    {
        //Si se ha añadido
        if (deviceChange == InputDeviceChange.Added)
        {
            //Aumentamos en 1 los mandos conectados
            _connectedControllers++;

            if (!inputManager.getDeviceConnected())
            {
                inputManager.changeDeviceConnected();
                //Se cargaria un archivo para ver que configuracion hay que usar
                inputManager.createGamepadDefaultInputs();
            }
            //Comprobación de nombres para mostrar la imagen
            if (device.name.Contains("Xbox"))
            {
                StartCoroutine(showImage(controllerIDEnum.XBOX, true));
            }
            else if (device.name.Contains("Switch"))
            {
                StartCoroutine(showImage(controllerIDEnum.NINTENDO, true));
            }
            else
            {
                StartCoroutine(showImage(controllerIDEnum.PS, true));
            }
        }
        else if (deviceChange == InputDeviceChange.Reconnected) //Se ha reconectado
        {
            _connectedControllers++;

            if (!inputManager.getDeviceConnected())
            {
                inputManager.changeDeviceConnected();
                //Se cargaria un archivo para ver que configuracion hay que usar
                inputManager.createGamepadDefaultInputs();
            }

            //Comprobación de nombres para mostrar la imagen
            if (device.name.Contains("Xbox"))
            {
                StartCoroutine(showImage(controllerIDEnum.XBOX, true));
            }
            else if (device.name.Contains("Switch"))
            {
                StartCoroutine(showImage(controllerIDEnum.NINTENDO, true));
            }
            else
            {
                StartCoroutine(showImage(controllerIDEnum.PS, true));
            }
        }
        else if (deviceChange == InputDeviceChange.Disconnected || deviceChange == InputDeviceChange.Removed) //Se ha desconectado
        {
            _connectedControllers--;
            if (_connectedControllers == 0)
            {
                inputManager.changeDeviceConnected();
            }

            //Comprobación de nombres para mostrar la imagen
            if (device.name.Contains("Xbox"))
            {
                StartCoroutine(showImage(controllerIDEnum.XBOX, false));
            }
            else if (device.name.Contains("Switch"))
            {
                StartCoroutine(showImage(controllerIDEnum.NINTENDO, false));
            }
            else
            {
                StartCoroutine(showImage(controllerIDEnum.PS, false));
            }
        }
    }

    /// <summary>
    /// Corrutina para mostrar la imagen correspondiente.
    /// </summary>
    /// <param name="controller">Tipo de mando conectado.</param>
    /// <param name="shouldBeBlack">Flag booleano para indicar si la imagen es negra (true) o blanca (false).</param>
    /// <returns></returns>
    private IEnumerator showImage(controllerIDEnum controller, bool shouldBeBlack)
    {
        //Asignación de imagen que corresponde
        Image showImage;
        if (shouldBeBlack)
        {
            showImage = _blackImages[controller];
        }
        else
        {

            showImage = _whiteImages[controller];
        }

        showImage.enabled = true;

        //Esperamos el tiempo correspondiente
        yield return new WaitForSeconds(_showTime);
        showImage.enabled = false;
    }
}
