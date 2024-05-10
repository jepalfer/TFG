using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// resolutionController es una clase que se usa para gestionar las distintas resoluciones para el juego.
/// </summary>
public class resolutionController : MonoBehaviour
{
    /// <summary>
    /// Lista con todas las resoluciones posibles para la pantalla del jugador.
    /// </summary>
    private Resolution[] _resolutions;

    /// <summary>
    /// Lista con las resoluciones posibles para la tasa de refresco del jugador.
    /// </summary>
    private List<Resolution> _filteredResolutions;

    /// <summary>
    /// Tasa de refresco a la cual está jugando el jugador.
    /// </summary>
    private int _currentRefreshRate;

    /// <summary>
    /// Índice en la lista <see cref="_filteredResolutions"/> de la resolución a la que está jugando el jugador.
    /// </summary>
    private int _currentResolutionIndex = 0;

    /// <summary>
    /// Dropdown para visualizar la lista de resoluciones.
    /// </summary>
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    /// <summary>
    /// Método que se ejecuta al hacer visible la UI del menú de opciones.
    /// </summary>
    public void initializeOptions()
    {
        //Obtenemos todas las resoluciones
        _resolutions = Screen.resolutions;
        _filteredResolutions = new List<Resolution>();

        _resolutionDropdown.ClearOptions();

        //Obtenemos la tasa de refresco a la que está la pantalla
        _currentRefreshRate = Screen.currentResolution.refreshRate;

        //Obtenemos las resoluciones con los mismos Hz
        for (int i = 0; i < _resolutions.Length; ++i)
        {
            if (_resolutions[i].refreshRate == _currentRefreshRate)
            {
                _filteredResolutions.Add(_resolutions[i]);
            }
        }

        //Creamos las opciones
        List<string> options = new List<string>();

        for (int i = 0; i < _filteredResolutions.Count; ++i)
        {
            string resolutionOption = _filteredResolutions[i].width + "x" + _filteredResolutions[i].height + " - " + _filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if (_filteredResolutions[i].width == Screen.width && _filteredResolutions[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
            }
        }

        //Creamos el dropdown
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Setter que modifica la resolución de la pantalla.
    /// </summary>
    /// <param name="resolutionIndex">Índice de la resolución seleccionada en la lista.</param>
    public void setResolution(int resolutionIndex)
    {
        Resolution res = _filteredResolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
    }

    /// <summary>
    /// Getter que devuelve <see cref="_resolutionDropdown"/>.
    /// </summary>
    /// <returns><see cref="_resolutionDropdown"/>.</returns>
    public TMP_Dropdown getResolutionDropdown()
    {
        return _resolutionDropdown;
    }
}
