using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
public class generalUIController : MonoBehaviour
{
    [SerializeField] private Image _blackPS;
    [SerializeField] private Image _whitePS;
    [SerializeField] private Image _blackNS;
    [SerializeField] private Image _whiteNS;
    [SerializeField] private Image _blackXB;
    [SerializeField] private Image _whiteXB;

    private Dictionary<controllerID, Image> _blackImages;
    private Dictionary<controllerID, Image> _whiteImages;

    private float _showTime = 0.75f;

    private static int _connectedControllers = 0;

    private void Start()
    {
        _blackImages = new Dictionary<controllerID, Image>();
        _whiteImages = new Dictionary<controllerID, Image>();

        _blackImages[controllerID.PS] = _blackPS;
        _blackImages[controllerID.XBOX] = _blackXB;
        _blackImages[controllerID.NINTENDO] = _blackNS;

        _whiteImages[controllerID.PS] = _whitePS;
        _whiteImages[controllerID.XBOX]= _whiteXB;
        _whiteImages[controllerID.NINTENDO] = _whiteNS;
    }

    private void OnEnable()
    {
        InputSystem.onDeviceChange += detectController;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= detectController;
    }

    private void detectController(InputDevice device, InputDeviceChange deviceChange)
    {
        if (deviceChange == InputDeviceChange.Added)
        {
            _connectedControllers++;

            if (!inputManager.getDeviceConnected())
            {
                inputManager.changeDeviceConnected();
                //Se cargaria un archivo para ver que configuracion hay que usar
                inputManager.createGamepadDefaultInputs();
            }
            if (device.name.Contains("Xbox"))
            {
                StartCoroutine(showImage(controllerID.XBOX, true));
            }
            else if (device.name.Contains("Switch"))
            {
                StartCoroutine(showImage(controllerID.NINTENDO, true));
            }
            else
            {
                StartCoroutine(showImage(controllerID.PS, true));
            }
        }
        else if (deviceChange == InputDeviceChange.Reconnected)
        {
            _connectedControllers++;

            if (!inputManager.getDeviceConnected())
            {
                inputManager.changeDeviceConnected();
                //Se cargaria un archivo para ver que configuracion hay que usar
                inputManager.createGamepadDefaultInputs();
            }
            if (device.name.Contains("Xbox"))
            {
                StartCoroutine(showImage(controllerID.XBOX, true));
            }
            else if (device.name.Contains("Switch"))
            {
                StartCoroutine(showImage(controllerID.NINTENDO, true));
            }
            else
            {
                StartCoroutine(showImage(controllerID.PS, true));
            }
        }
        else if (deviceChange == InputDeviceChange.Disconnected || deviceChange == InputDeviceChange.Removed)
        {
            _connectedControllers--;
            if (_connectedControllers == 0)
            {
                inputManager.changeDeviceConnected();
            }
            if (device.name.Contains("Xbox"))
            {
                StartCoroutine(showImage(controllerID.XBOX, false));
            }
            else if (device.name.Contains("Switch"))
            {
                StartCoroutine(showImage(controllerID.NINTENDO, false));
            }
            else
            {
                StartCoroutine(showImage(controllerID.PS, false));
            }
        }
    }

    public static int getDevicesConnected()
    {
        return _connectedControllers;
    }
    private IEnumerator showImage(controllerID controller, bool shouldBeBlack)
    {
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
        yield return new WaitForSeconds(_showTime);
        showImage.enabled = false;
    }
}
