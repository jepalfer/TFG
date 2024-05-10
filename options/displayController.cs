using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
/// <summary>
/// displayController es una clase que se usa para gestionar la visualizaci�n que quiere el jugador.
/// Puede ser pantalla completa o modo ventana.
/// </summary>
public class displayController : MonoBehaviour
{
    /// <summary>
    /// Es el modo de visualizaci�n de pantalla elegido.
    /// </summary>
    private FullScreenMode _selectedScreenMode;

    /// <summary>
    /// Dropdown con todas las opciones de visualizaci�n.
    /// </summary>
    [SerializeField] private TMP_Dropdown _displayDropDown;

    /// <summary>
    /// Diccionario para almacenar el �ndice en la lista junto a su opci�n de visualizado correspondiente.
    /// </summary>
    private Dictionary<int, FullScreenMode> _displayDictionary;

    /// <summary>
    /// M�todo que se ejecuta al hacer visible la UI del men� de opciones.
    /// </summary>
    public void initializeOptions()
    {
        //A�adimos las opciones al diccionario
        _displayDictionary = new Dictionary<int, FullScreenMode>();
        _displayDictionary.Add(0, FullScreenMode.FullScreenWindow);
        _displayDictionary.Add(1, FullScreenMode.Windowed);
        
        //Creamos las opciones del desplegable
        List<string> options = new List<string>();
        options.Add("Pantalla completa");
        options.Add("Modo ventana");

        //A�adimos las opciones y cambiamos el �ndice inicial
        _displayDropDown.ClearOptions();
        _displayDropDown.AddOptions(options);
        _displayDropDown.value = _displayDictionary.FirstOrDefault(display => display.Value == Screen.fullScreenMode).Key;
        _displayDropDown.RefreshShownValue();

    }

    /// <summary>
    /// Setter que modifica <see cref="_selectedScreenMode"/> y modifica el modo de visualizaci�n.
    /// </summary>
    /// <param name="index">�ndice del modo de visualizaci�n de la lista </param>
    public void setDisplayMode(int index)
    {
        if (_displayDictionary[index] == FullScreenMode.FullScreenWindow)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
        //if (_displayDictionary[index] != Screen.fullScreenMode)
        //{
        //    if (_displayDictionary[index] == FullScreenMode.FullScreenWindow)
        //    {
        //        Screen.SetResolution(Screen.width * 2, Screen.height * 2, true);
        //    }
        //    else
        //    {
        //        Screen.SetResolution(Screen.width / 2, Screen.height / 2, false);
        //    }
        //}
    }

    /// <summary>
    /// Getter que devuelve <see cref="_selectedScreenMode"/>.
    /// </summary>
    /// <returns><see cref="_selectedScreenMode"/>.</returns>
    public FullScreenMode getScreenMode()
    {
        return _selectedScreenMode;
    }
}
