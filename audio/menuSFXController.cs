using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// menuSFXController es una clase que se usa para almacenar las referencias a los SFX relacionados con el men�.
/// </summary>
public class menuSFXController : MonoBehaviour
{
    /// <summary>
    /// SFX para navegar hacia arriba/abajo/derecha/izquierda en un men�.
    /// </summary>
    [SerializeField] private AudioClip _menuNavigationSFX;

    /// <summary>
    /// SFX para los botones de un men�.
    /// </summary>
    [SerializeField] private AudioClip _menuAcceptSFX;

    /// <summary>
    /// SFX para cambiar entre pesta�as de men�.
    /// </summary>
    [SerializeField] private AudioClip _tabSFX;

    /// <summary>
    /// M�todo para reproducir el SFX para navegar por el men�.
    /// </summary>
    public void playMenuNavigationSFX()
    {
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().clip = _menuNavigationSFX;
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// M�todo para reproducir el SFX para aceptar en un men�.
    /// </summary>
    public void playMenuAcceptSFX()
    {
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().clip = _menuAcceptSFX;
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().Play();
    }


    /// <summary>
    /// M�todo para reproducir el SFX para cambiar pesta�as en un men�.
    /// </summary>
    public void playTabSFX()
    {
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().clip = _tabSFX;
        GetComponent<audioManager>().getSFXPlayer().GetComponent<AudioSource>().Play();
    }

}
