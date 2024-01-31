using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// obstacleLogic es una clase que maneja la l�gica de los obst�culos (botones, palancas...).
/// </summary>
public class obstacleLogic : MonoBehaviour
{
    /// <summary>
    /// Es el muro asociado que se abre al activar/pulsar el obst�culo.
    /// </summary>
    [SerializeField] private GameObject _associatedWall;
    /// <summary>
    /// Es el ID interno del obst�culo.
    /// </summary>
    [SerializeField] private int _obstacleID;

    /// <summary>
    /// Determina si el obst�culo ha sido activado/pulsado o no.
    /// </summary>
    [SerializeField] private bool _hasBeenActivated = false;

    /// <summary>
    /// Los datos guardados de los obst�culos. Ver <see cref="obstaclesData"/> para m�s informaci�n.
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
    /// M�todo que se ejecuta el primero de todos al iniciar el script.
    /// Carga los datos de los obst�culos. Si no existen (primera vez que entramos a la escena) los guarda usando <see cref="saveSystem.saveObstaclesData(List{sceneObstaclesData})"/>.
    /// Si ya existen los carga y busca este objeto para modificarlo seg�n coresponde.
    /// </summary>
    private void Awake()
    {
        //Obtenemos los datos guardados
        _data = saveSystem.loadObstaclesData();
        sceneObstaclesData obstacle = new sceneObstaclesData(false, _obstacleID, SceneManager.GetActiveScene().buildIndex);

        //Comprobamos que existan
        if (_data == null)
        {
            //Guardamos el obst�culo
            obstaclesData state = new obstaclesData();
            state.incrementSize(obstacle);
            saveSystem.saveObstaclesData(state.getStoredData());
        }
        else
        {
            sceneObstaclesData searchedObstacle = _data.getStoredData().Find(obst => obst.getObstacleID() == _obstacleID && obst.getSceneID() == SceneManager.GetActiveScene().buildIndex);
        
            if (searchedObstacle == null)
            {
                //Guardamos el obst�culo si no lo est�
                _data.incrementSize(searchedObstacle);
                saveSystem.saveObstaclesData(_data.getStoredData());
            }
            else
            {
                //Modificamos si el obst�culo se activ�
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
    /// M�todo auxiliar para activar el obst�culo.
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
