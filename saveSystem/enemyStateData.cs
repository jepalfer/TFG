using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemyStateData es una clase que guarda el estado de los enemigos de cada escena.
/// </summary>
[System.Serializable]
public class enemyStateData
{
    /// <summary>
    /// Lista con los estados de los enemigos de cada escena.
    /// </summary>
    [SerializeField] private List<sceneEnemiesState> _enemyStates = new List<sceneEnemiesState>();
    
    /// <summary>
    /// Constructor por defecto de la clase. Crea una lista vacía.
    /// </summary>
    public enemyStateData()
    {
        _enemyStates = new List<sceneEnemiesState>();
    }

    /// <summary>
    /// Constructor con parámetros de la clase. Asigna una lista dada a <see cref="_enemyStates"/>.
    /// </summary>
    /// <param name="data">Lista a asignar.</param>
    public enemyStateData(List<sceneEnemiesState> data)
    {
        _enemyStates = data;
    }

    /// <summary>
    /// Método usado para modificar el estado concreto de un enemigo.
    /// </summary>
    /// <param name="sceneID">ID de la escena en la que se encuentra el enemigo.</param>
    /// <param name="enemyID">ID del enemigo.</param>
    /// <param name="isAlive">Booleano que indice si está vivo o no.</param>
    /// <param name="canRevive">Booleano que indica si puede o no revivir.</param>
    public void modifyEnemyState(int sceneID, int enemyID, int isAlive, int canRevive)
    {
        //Buscamos al enemigo
        sceneEnemiesState enemy = _enemyStates.Find(item => item.getSceneID() == sceneID && item.getEnemyID() == enemyID);
        
        //Modificamos variables
        enemy.setIsAlive(isAlive);
        enemy.setCanRevive(canRevive);
    }

    /// <summary>
    /// Método para aumentar en 1 el tamaño de <see cref="_enemyStates"/>.
    /// </summary>
    /// <param name="sceneState">Nuevo enemigo (estado) a añadir.</param>
    public void incrementSize(sceneEnemiesState sceneState)
    {
        _enemyStates.Add(sceneState);
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemyStates"/>.
    /// </summary>
    /// <returns><see cref="_enemyStates"/>.</returns>
    public List<sceneEnemiesState> getEnemyStates()
    {
        return _enemyStates;
    }

}
