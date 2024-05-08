using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// breakableWallBehaviour es una clase que se encarga de manejar la l�gica de las paredes que se
/// pueden romper
/// </summary>
public class breakableWallBehaviour : MonoBehaviour
{
    /// <summary>
    /// ID interno de la pared.
    /// </summary>
    [SerializeField] private int _wallID;

    /// <summary>
    /// Referencia a los datos serializados de los obst�culos.
    /// </summary>
    private obstaclesData _obstaclesData;

    /// <summary>
    /// M�todo que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        //Cargamos los datos de los obst�culos
        _obstaclesData = saveSystem.loadObstaclesData();

        //Si no es el primer obst�culo
        if (_obstaclesData != null)
        {
            //Comprobamos haya sido a�adido
            sceneObstaclesData currentWall = _obstaclesData.getStoredData().Find(wall => wall.getSceneID() == SceneManager.GetActiveScene().buildIndex && wall.getObstacleID() == _wallID);
            if (currentWall == null)
            {
                _obstaclesData.incrementSize(new sceneObstaclesData(true, _wallID, SceneManager.GetActiveScene().buildIndex));
            }
            else
            {
                //Si ya ha sido destruida destruimos el gameObject
                if (!currentWall.getIsActivated())
                {
                    Destroy(gameObject);
                }
            }
            //Serializamos
            saveSystem.saveObstaclesData(_obstaclesData.getStoredData());
        }
        else //Es el primer obst�culo
        {
            List<sceneObstaclesData> newData = new List<sceneObstaclesData>();
            newData.Add(new sceneObstaclesData(true, _wallID, SceneManager.GetActiveScene().buildIndex));

            saveSystem.saveObstaclesData(newData);
        }
    }

    /// <summary>
    /// M�todo usado para destruir el muro.
    /// </summary>
    public void destroyWall()
    {
        //Cargamos los datos de obst�culos
        _obstaclesData = saveSystem.loadObstaclesData();

        //Modificamos el obst�culo y lo destruimos
        _obstaclesData.modifyObstacle(false, _wallID, SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);

        //Serializamos
        saveSystem.saveObstaclesData(_obstaclesData.getStoredData());
    }
}
