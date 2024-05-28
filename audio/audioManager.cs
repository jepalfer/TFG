using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// audioManager es una clase que permite manejar los niveles de audio del mezclador.
/// </summary>
public class audioManager : MonoBehaviour
{
    /// <summary>
    /// Referencia a la configuración de audio guardada.
    /// </summary>
    private audioSettingsData _audioSettings;

    /// <summary>
    /// Referencia al mezclador de sonido.
    /// </summary>
    [SerializeField] private AudioMixer _mixer;

    /// <summary>
    /// Referencia al clip de audio para la OST del menú.
    /// </summary>
    [SerializeField] private AudioClip _initialAudio;

    /// <summary>
    /// Referencia al GameObject que contiene el <see cref="audioSource"/> para ost.
    /// </summary>
    [SerializeField] private GameObject _ost;

    /// <summary>
    /// Referencia al GameObject que contiene el <see cref="audioSource"/> para sfx generales.
    /// </summary>
    [SerializeField] private GameObject _sfx;

    /// <summary>
    /// Referencia a la instancia del <see cref="audioManager"/> para manejarlo entre escenas.
    /// </summary>
    public static audioManager _instance;

    /// <summary>
    /// Método que se ejecuta al iniciar el script.
    /// </summary>
    private void Awake()
    {
        //Comprobamos que la referencia a la instancia no sea nula para gestionar el manager
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Actualizamos la variable static
        config.setAudioManager(_instance.gameObject);

        //Comprobamos en caso de que no sea la OST del menú
        if (_instance._ost.GetComponent<AudioSource>().clip != _initialAudio)
        {
            _instance._ost.GetComponent<AudioSource>().clip = _initialAudio;
            _instance._ost.GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Método que se ejecuta al iniciar el script tras el <see cref="Awake()"/>
    /// </summary>
    private void Start()
    {
        //Cargamos la configuración de audio
        _audioSettings = saveSystem.loadAudioSettings();

        if (_audioSettings == null) //Primera vez que entramos al juego
        {
            _audioSettings = new audioSettingsData();
            saveSystem.saveAudioSettings(_audioSettings.getMasterVolume(), _audioSettings.getOSTVolume(), _audioSettings.getSFXVolume());
        }

        //Iniciamos la OST
        _ost.GetComponent<AudioSource>().Play();

        //Modificamos los volúmenes de audio
        setAudio(audioSettingsEnum.masterVolume.ToString(), _audioSettings.getMasterVolume());
        setAudio(audioSettingsEnum.OSTVolume.ToString(), _audioSettings.getOSTVolume());
        setAudio(audioSettingsEnum.SFXVolume.ToString(), _audioSettings.getSFXVolume());
    }

    /// <summary>
    /// Método auxiliar para modificar los niveles de audio del mezclador.
    /// </summary>
    /// <param name="name">Nombre del grupo del mezclador.</param>
    /// <param name="value">Valor a asignar.</param>
    public void setAudio(string name, float value)
    {
        _mixer.SetFloat(name, 20 * Mathf.Log10(value));
    }

    /// <summary>
    /// Getter que devuelve <see cref="_ost"/>.
    /// </summary>
    /// <returns><see cref="_ost"/>.</returns>
    public GameObject getOSTPlayer()
    {
        return _ost;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_sfx"/>.
    /// </summary>
    /// <returns><see cref="_sfx"/>.</returns>
    public GameObject getSFXPlayer()
    {
        return _sfx;
    }
}
