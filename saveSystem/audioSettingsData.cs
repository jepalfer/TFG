using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// audioSettingsData es una clase que permite guardar la configuración de audio.
/// </summary>
[System.Serializable]
public class audioSettingsData
{
    /// <summary>
    /// Atributo para guardar el volumen master.
    /// </summary>
    [SerializeField] private float _masterVolume;

    /// <summary>
    /// Atributo para guardar el volumen de la OST.
    /// </summary>
    [SerializeField] private float _OSTVolume;

    /// <summary>
    /// Atributo para guardar el volumen de los SFX.
    /// </summary>
    [SerializeField] private float _SFXVolume;
    
    /// <summary>
    /// Atributo para el valor por defecto del volumen.
    /// </summary>

    private float _defaultVolume = 0.2f;
    
    /// <summary>
    /// Constructor que pone por defecto todos los volúmenes. 
    /// </summary>
    public audioSettingsData()
    {
        setDefault();
    }

    /// <summary>
    /// Constructor con parámetros de la clase.
    /// </summary>
    /// <param name="master">Volumen master.</param>
    /// <param name="ost">Volumen de la OST.</param>
    /// <param name="sfx">Volumen de los SFX.</param>
    public audioSettingsData(float master, float ost, float sfx)
    {
        setMasterVolume(master);
        setOSTVolume(ost);
        setSFXVolume(sfx);
    }

    /// <summary>
    /// Setter que modifica <see cref="_masterVolume"/>.
    /// </summary>
    /// <param name="volume">Volumen a asignar.</param>
    public void setMasterVolume(float volume)
    {
        _masterVolume = volume;
    }

    /// <summary>
    /// Setter que modifica <see cref="_OSTVolume"/>.
    /// </summary>
    /// <param name="volume">Volumen a asignar.</param>
    public void setOSTVolume(float volume)
    {
        _OSTVolume = volume;
    }

    /// <summary>
    /// Setter que modifica <see cref="_SFXVolume"/>.
    /// </summary>
    /// <param name="volume">Volumen a asignar.</param>
    public void setSFXVolume(float volume)
    {
        _SFXVolume = volume;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_masterVolume"/>.
    /// </summary>
    /// <returns>Un float que almacena el volumen principal.</returns>
    public float getMasterVolume()
    {
        return _masterVolume;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_OSTVolume"/>.
    /// </summary>
    /// <returns>Un float que almacena el volumen de la OST.</returns>
    public float getOSTVolume()
    {
        return _OSTVolume;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_SFXVolume"/>.
    /// </summary>
    /// <returns>Un float que almacena el volumen de los SFX.</returns>
    public float getSFXVolume()
    {
        return _SFXVolume;
    }

    /// <summary>
    /// Método que pone los volúmenes a sus valores por defecto.
    /// </summary>
    public void setDefault()
    {
        setMasterVolume(_defaultVolume);
        setOSTVolume(_defaultVolume);
        setSFXVolume(_defaultVolume);
    }
}
