using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// menuSFXController es una clase que se usa para almacenar las referencias a los SFX relacionados con el menú.
/// </summary>
public class menuSFXController : MonoBehaviour
{
    /// <summary>
    /// SFX para navegar hacia arriba/abajo/derecha/izquierda en un menú.
    /// </summary>
    [SerializeField] private AudioClip _menuNavigationSFX;

    /// <summary>
    /// SFX para los botones de un menú.
    /// </summary>
    [SerializeField] private AudioClip _menuAcceptSFX;

    /// <summary>
    /// SFX para cambiar entre pestañas de menú.
    /// </summary>
    [SerializeField] private AudioClip _tabSFX;

    /// <summary>
    /// Método para reproducir el SFX para navegar por el menú.
    /// </summary>
    public void playMenuNavigationSFX()
    {
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().clip = _menuNavigationSFX;
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// Método para reproducir el SFX para aceptar en un menú.
    /// </summary>
    public void playMenuAcceptSFX()
    {
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().clip = _menuAcceptSFX;
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().Play();
    }


    /// <summary>
    /// Método para reproducir el SFX para cambiar pestañas en un menú.
    /// </summary>
    public void playTabSFX()
    {
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().clip = _tabSFX;
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().Play();
    }

}
