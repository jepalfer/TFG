using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
/// <summary>
/// displayController es una clase que se usa para gestionar la visualización que quiere el jugador.
/// Puede ser pantalla completa o modo ventana.
/// </summary>
public class displayController : MonoBehaviour
{
    /// <summary>
    /// Es el modo de visualización de pantalla elegido.
    /// </summary>
    private FullScreenMode _selectedScreenMode;

    /// <summary>
    /// Dropdown con todas las opciones de visualización.
    /// </summary>
    [SerializeField] private TMP_Dropdown _displayDropDown;

    /// <summary>
    /// Diccionario para almacenar el índice en la lista junto a su opción de visualizado correspondiente.
    /// </summary>
    private Dictionary<int, FullScreenMode> _displayDictionary;

    /// <summary>
    /// Método que se ejecuta al hacer visible la UI del menú de opciones.
    /// </summary>
    public void initializeOptions()
    {
        //Añadimos las opciones al diccionario
        _displayDictionary = new Dictionary<int, FullScreenMode>();
        _displayDictionary.Add(0, FullScreenMode.FullScreenWindow);
        _displayDictionary.Add(1, FullScreenMode.Windowed);
        
        //Creamos las opciones del desplegable
        List<string> options = new List<string>();
        options.Add("Pantalla completa");
        options.Add("Modo ventana");

        //Añadimos las opciones y cambiamos el índice inicial
        _displayDropDown.ClearOptions();
        _displayDropDown.AddOptions(options);
        _displayDropDown.value = _displayDictionary.FirstOrDefault(display => display.Value == Screen.fullScreenMode).Key;
        _displayDropDown.RefreshShownValue();

    }

    /// <summary>
    /// Setter que modifica <see cref="_selectedScreenMode"/> y modifica el modo de visualización.
    /// </summary>
    /// <param name="index">Índice del modo de visualización de la lista </param>
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
