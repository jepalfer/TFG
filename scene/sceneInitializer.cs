using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// sceneInitializer es una clase que se usa para inicializar distintos elementos necesarios en una escena.
/// </summary>
public class sceneInitializer : MonoBehaviour
{
    /// <summary>
    /// Referencia al prefab que contiene las UI.
    /// </summary>
    [SerializeField] private GameObject _UIPrefab;

    /// <summary>
    /// Referencia al prefab del jugador.
    /// </summary>
    [SerializeField] private GameObject _playerPrefab;

    /// <summary>
    /// Referencia al prefab que contiene el controlador de inputs.
    /// </summary>
    [SerializeField] private GameObject _inputControllerPrefab;

    /// <summary>
    /// Referencia al prefab del sistema de eventos.
    /// </summary>
    [SerializeField] private GameObject _eventSystemPrefab;

    /// <summary>
    /// Referencia al prefab que contiene el inventario.
    /// </summary>
    [SerializeField] private GameObject _inventoryPrefab;

    /// <summary>
    /// Referencia a la OST que se usa en esa escena.
    /// </summary>
    [SerializeField] private AudioClip _ost;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        Instantiate(_eventSystemPrefab);
        Instantiate(_inventoryPrefab);
        Instantiate(_inputControllerPrefab);
        Instantiate(_playerPrefab);
        Instantiate(_UIPrefab);
    }

    /// <summary>
    /// Método que se ejecuta al inicio del script tras el <see cref="Awake()"/>.
    /// </summary>
    private void Start()
    {
        //Cargamos los datos del jugador
        playerData data = saveSystem.loadPlayer();

        if (data != null) //Hemos cargado partida
        {
            //Se hace una comprobación de la última escena visitada
            if (saveSystem.loadLastSceneData().getSceneID() == SceneManager.GetActiveScene().buildIndex)
            {
                Vector3 pos = new Vector3(data.getX(), data.getY(), data.getZ());
                config.getPlayer().GetComponent<playerMovement>().setFacingRight(data.getIsFacingRight());

                if (!config.getPlayer().GetComponent<playerMovement>().getIsFacingRight())
                {
                    Vector3 currentScale = config.getPlayer().gameObject.transform.localScale;
                    currentScale.x *= -1;
                    config.getPlayer().gameObject.transform.localScale = currentScale;
                }

                config.getPlayer().transform.position = pos;
            }
            else
            {
                if (saveSystem.loadLastSceneData().getSceneID() == GetComponent<sceneSwitch>().getTopID())
                {
                    List<float> coordinates = GetComponent<sceneSwitch>().getTopCoordinates();

                    Vector3 pos = new Vector3(coordinates[0], coordinates[1], coordinates[2]);
                    config.getPlayer().GetComponent<playerMovement>().setFacingRight(true);
                    config.getPlayer().transform.position = pos;
                }
                else if (saveSystem.loadLastSceneData().getSceneID() == GetComponent<sceneSwitch>().getRightID())
                {
                    List<float> coordinates = GetComponent<sceneSwitch>().getRightCoordinates();

                    Vector3 pos = new Vector3(coordinates[0], coordinates[1], coordinates[2]);
                    config.getPlayer().GetComponent<playerMovement>().setFacingRight(false);

                    Vector3 currentScale = config.getPlayer().gameObject.transform.localScale;
                    currentScale.x *= -1;
                    config.getPlayer().gameObject.transform.localScale = currentScale;

                    config.getPlayer().transform.position = pos;
                }
                else if (saveSystem.loadLastSceneData().getSceneID() == GetComponent<sceneSwitch>().getBottomID())
                {
                    List<float> coordinates = GetComponent<sceneSwitch>().getBottomCoordinates();

                    Vector3 pos = new Vector3(coordinates[0], coordinates[1], coordinates[2]);
                    config.getPlayer().GetComponent<playerMovement>().setFacingRight(true);
                    config.getPlayer().transform.position = pos;
                }
                else if (saveSystem.loadLastSceneData().getSceneID() == GetComponent<sceneSwitch>().getLeftID())
                {
                    List<float> coordinates = GetComponent<sceneSwitch>().getLeftCoordinates();

                    Vector3 pos = new Vector3(coordinates[0], coordinates[1], coordinates[2]);
                    config.getPlayer().GetComponent<playerMovement>().setFacingRight(true);
                    config.getPlayer().transform.position = pos;
                }
            }
        }
        else //Hemos empezado partida
        {
            if (GetComponent<sceneSwitch>().getSpawnPoint() != null)
            {
                Vector3 spawnPos = new Vector3(GetComponent<sceneSwitch>().getSpawnPoint()[0], GetComponent<sceneSwitch>().getSpawnPoint()[1], GetComponent<sceneSwitch>().getSpawnPoint()[2]);
                config.getPlayer().transform.position = spawnPos;
                saveSystem.saveLastScene();
            }
        }

        //Guardamos esta escena como la última y la posición del jugador
        saveSystem.saveLastScene();
        saveSystem.savePlayer();

        //Comprobación de la ost para que si es distinta se reproduzca desde el inicio
        if (config.getAudioManager().GetComponent<AudioSource>().clip != _ost)
        {
            config.getAudioManager().GetComponent<AudioSource>().clip = _ost;
            config.getAudioManager().GetComponent<AudioSource>().Play();
        }
    }
}
