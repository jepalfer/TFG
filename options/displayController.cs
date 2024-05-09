using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        
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
