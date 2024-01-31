using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// reviveEnemy es una clase que maneja la lógica para revivir a un enemigo.
/// </summary>
public class reviveEnemy : MonoBehaviour
{
    /// <summary>
    /// Es el enemigo que revive.
    /// </summary>
    [SerializeField] private GameObject _enemy;

    /// <summary>
    /// Es el contenedor del enemigo y que usa el script.
    /// </summary>
    [SerializeField] private GameObject _enemyPrefab;

    /// <summary>
    /// Las coordenadas donde aparece el enemigo.
    /// </summary>
    [SerializeField] private float[] _coordinates = new float[3];

    /// <summary>
    /// El ID interno del enemigo.
    /// </summary>
    [SerializeField] private int _ID;

    /// <summary>
    /// Método que se ejecuta al iniciar el script.
    /// Carga los datos de los enemigos y activa o no <see cref="_enemy"/> según lo que corresponda.
    /// </summary>
    private void Start()
    {
        //Obtenemos los datos
        _ID = _enemy.GetComponent<enemy>().getEnemyID();
        enemyStateData _data = saveSystem.loadEnemyData();

        //Buscamos el enemigo que se ha instanciado
        sceneEnemiesState foundEnemy = _data.getEnemyStates().Find(enemy => enemy.getSceneID() == SceneManager.GetActiveScene().buildIndex && enemy.getEnemyID() == _ID);

        //Si lo ha encontrado
        if (foundEnemy != null)
        {
            //Obtenemos el índice en el que está guardado
            int index = _data.getEnemyStates().IndexOf(foundEnemy);
            
            //Hacemos comprobaciones
            if (foundEnemy.getIsAlive() == 0)
            {
                if (foundEnemy.getCanRevive() == 0)
                {
                    _enemy.SetActive(false);
                }
                else
                {
                    foundEnemy.setCanRevive(0);
                    foundEnemy.setIsAlive(1);

                    _data.getEnemyStates()[index] = foundEnemy;

                    //Guardamos la modificación
                    saveSystem.saveEnemyData(_data.getEnemyStates());
                    _enemy.SetActive(true);
                }
            }
            else
            {
                _enemy.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemyPrefab"/>.
    /// </summary>
    /// <returns>Un GameObject que es una referencia al contenedor del enemigo.</returns>
    private GameObject getPrefab()
    {
        return _enemyPrefab;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemy"/>.
    /// </summary>
    /// <returns>Un GameObject que es una referencia al enemigo.</returns>
    private GameObject getEnemy()
    {
        return _enemy;
    }

    /// <summary>
    /// Setter para establecer las coordenadas del enemigo.
    /// </summary>
    /// <param name="coords">Array de floats que contiene las coordenadas.</param>
    private void setCoordinates(float[] coords)
    {
        _coordinates[0] = coords[0];
        _coordinates[1] = coords[1];
        _coordinates[2] = coords[2];

        _enemy.transform.position = new Vector3(_coordinates[0], _coordinates[1], _coordinates[2]);
    }
}
