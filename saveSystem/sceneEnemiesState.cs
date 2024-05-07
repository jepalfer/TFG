using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sceneEnemiesState es una clase auxilair que se usa para guardar los datos de los enemigos de una escena 
/// en particular.
/// </summary>
[System.Serializable]
public class sceneEnemiesState
{
    /// <summary>
    /// ID interno de la escena.
    /// </summary>
    [SerializeField] private int _sceneID;

    /// <summary>
    /// ID interno del enemigo.
    /// </summary>
    [SerializeField] private int _enemyID;

    /// <summary>
    /// Flag que indica si está o no vivo.
    /// </summary>
    [SerializeField] private int _isAlive;

    /// <summary>
    /// Flag que indica si es o no un boss.
    /// </summary>
    [SerializeField] private int _isBoss;

    /// <summary>
    /// Flag que indica si puede o no revivir.
    /// </summary>
    [SerializeField] private int _canRevive;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="sceneID">ID de la escena a la que pertenece el enemigo.</param>
    /// <param name="enemyID">ID del enemigo.</param>
    /// <param name="enemyAlive">Flag que indica si el enemigo está vivo.</param>
    /// <param name="isBoss">Flag que indica si el enemigo es un boss.</param>
    /// <param name="enemyCanRevive">Flag que indica si el enemigo puede revivir.</param>
    public sceneEnemiesState(int sceneID, int enemyID, int enemyAlive, int isBoss, int enemyCanRevive)
    {
        _sceneID = sceneID;
        _enemyID = enemyID;
        _isAlive = enemyAlive;
        _isBoss = isBoss;
        _canRevive = enemyCanRevive;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_sceneID"/>.
    /// </summary>
    /// <returns><see cref="_sceneID"/>.</returns>
    public int getSceneID()
    {
        return _sceneID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemyID"/>.
    /// </summary>
    /// <returns><see cref="_enemyID"/>.</returns>
    public int getEnemyID()
    {
        return _enemyID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isAlive"/>.
    /// </summary>
    /// <returns><see cref="_isAlive"/>.</returns>
    public int getIsAlive()
    {
        return _isAlive;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isBoss"/>.
    /// </summary>
    /// <returns><see cref="_isBoss"/>.</returns>
    public int getIsBoss()
    {
        return _isBoss;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_canRevive"/>.
    /// </summary>
    /// <returns><see cref="_canRevive"/>.</returns>
    public int getCanRevive()
    {
        return _canRevive;
    }

    /// <summary>
    /// Setter que modifica <see cref="_isAlive"/>.
    /// </summary>
    /// <param name="isAlive">Valor a asignar.</param>
    public void setIsAlive(int isAlive)
    {
        _isAlive = isAlive;
    }

    /// <summary>
    /// Setter que modifica <see cref="_canRevive"/> si el enemigo no es un boss.
    /// </summary>
    /// <param name="enemyCanRevive">Valor a asignar.</param>
    public void setCanRevive(int enemyCanRevive)
    {
        if (_isBoss == 0)
        {
            _canRevive = enemyCanRevive;
        }
    }
}
