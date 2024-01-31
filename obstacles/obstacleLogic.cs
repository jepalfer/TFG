using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// obstacleLogic es una clase que maneja la lógica de los obstáculos (botones, palancas...).
/// </summary>
public class obstacleLogic : MonoBehaviour
{
    /// <summary>
    /// Es el muro asociado que se abre al activar/pulsar el obstáculo.
    /// </summary>
    [SerializeField] private GameObject _associatedWall;
    /// <summary>
    /// Es el ID interno del obstáculo.
    /// </summary>
    [SerializeField] private int _obstacleID;

    /// <summary>
    /// Determina si el obstáculo ha sido activado/pulsado o no.
    /// </summary>
    [SerializeField] private bool _hasBeenActivated = false;

    /// <summary>
    /// Los datos guardados de los obstáculos. Ver <see cref="obstaclesData"/> para más información.
    /// </summary>
    [SerializeField] private obstaclesData _data;

    /// <summary>
    /// Getter que devuelve <see cref="_associatedWall"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene una referencia al muro.</returns>
    public GameObject getAssociatedWall()
    {
        return _associatedWall;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_obstacleID"/>.
    /// </summary>
    /// <returns>Un valor entero.</returns>
    public int getObstacleID()
    {
        return _obstacleID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_hasBeenActivated"/>.
    /// </summary>
    /// <returns>Un valor booleano.</returns>
    public bool getHasBeenActivated()
    {
        return _hasBeenActivated;
    }

    /// <summary>
    /// Método que se ejecuta el primero de todos al iniciar el script.
    /// Carga los datos de los obstáculos. Si no existen (primera vez que entramos a la escena) los guarda usando <see cref="saveSystem.saveObstaclesData(List{sceneObstaclesData})"/>.
    /// Si ya existen los carga y busca este objeto para modificarlo según coresponde.
    /// </summary>
    private void Awake()
    {
        //Obtenemos los datos guardados
        _data = saveSystem.loadObstaclesData();
        sceneObstaclesData obstacle = new sceneObstaclesData(false, _obstacleID, SceneManager.GetActiveScene().buildIndex);

        //Comprobamos que existan
        if (_data == null)
        {
            //Guardamos el obstáculo
            obstaclesData state = new obstaclesData();
            state.incrementSize(obstacle);
            saveSystem.saveObstaclesData(state.getStoredData());
        }
        else
        {
            sceneObstaclesData searchedObstacle = _data.getStoredData().Find(obst => obst.getObstacleID() == _obstacleID && obst.getSceneID() == SceneManager.GetActiveScene().buildIndex);
        
            if (searchedObstacle == null)
            {
                //Guardamos el obstáculo si no lo está
                _data.incrementSize(searchedObstacle);
                saveSystem.saveObstaclesData(_data.getStoredData());
            }
            else
            {
                //Modificamos si el obstáculo se activó
                if (searchedObstacle.getIsActivated())
                {
                    //_associatedWall.GetComponent<BoxCollider2D>().enabled = false;
                    _hasBeenActivated = true;
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<Animator>().SetTrigger("pressed");
                }
            }
        }
    }

    /// <summary>
    /// Método auxiliar para activar el obstáculo.
    /// </summary>
    public void activateObstacle()
    {
        _data = saveSystem.loadObstaclesData();
        _hasBeenActivated = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("pressed");
        _data.modifyObstacle(true, _obstacleID, SceneManager.GetActiveScene().buildIndex);
        saveSystem.saveObstaclesData(_data.getStoredData());
    }

}
