using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// config es una clase utilizada para almacenar las referencias al jugador 
/// y el inventario para que se pueda acceder desde otra clase.
/// </summary>
public static class config
{
    /// <summary>
    /// Referencia al jugador.
    /// </summary>
    private static GameObject _player;

    /// <summary>
    /// Referencia al inventario.
    /// </summary>
    private static GameObject _inventory;

    /// <summary>
    /// Referencia al gestor de audio.
    /// </summary>
    private static GameObject _audioManager;

    /// <summary>
    /// Referencia a la pantalla de muerte.
    /// </summary>
    private static GameObject _deathScreen;

    /// <summary>
    /// Referencia a la pantalla de carga.
    /// </summary>
    private static GameObject _loadingScreen;

    /// <summary>
    /// Referencia a la lista de enemigos.
    /// </summary>
    private static List<GameObject> _enemiesList;

    /// <summary>
    /// Setter que modifica <see cref="_player"/>.
    /// </summary>
    /// <param name="player">El GameObject a asignar.</param>
    public static void setPlayer(GameObject player)
    {
        _player = player;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_player"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene la referencia al jugador.</returns>
    public static GameObject getPlayer()
    {
        return _player;
    }

    /// <summary>
    /// Setter que modifica <see cref="_inventory"/>.
    /// </summary>
    /// <param name="inventory">El GameObject a asignar.</param>
    public static void setInventory(GameObject inventory)
    {
        _inventory = inventory;
    }


    /// <summary>
    /// Getter que devuelve <see cref="_inventory"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene la referencia al inventario.</returns>
    public static GameObject getInventory()
    {
        return _inventory;
    }


    /// <summary>
    /// Setter que modifica <see cref="_inventory"/>.
    /// </summary>
    /// <param name="audioManager">El GameObject a asignar.</param>
    public static void setAudioManager(GameObject audioManager)
    {
        _audioManager = audioManager;
    }


    /// <summary>
    /// Getter que devuelve <see cref="_audioManager"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene la referencia al gestor de audio.</returns>
    public static GameObject getAudioManager()
    {
        return _audioManager;
    }

    /// <summary>
    /// Setter que modifica <see cref="_deathScreen"/>.
    /// </summary>
    /// <param name="deathScreen">Gameobject a asignar.</param>
    public static void setDeathScreen(GameObject deathScreen)
    {
        _deathScreen = deathScreen;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_deathScreen"/>.
    /// </summary>
    /// <returns><see cref="_deathScreen"/>.</returns>
    public static GameObject getDeathScreen()
    {
        return _deathScreen;
    }


    /// <summary>
    /// Setter que modifica <see cref="_loadingScreen"/>.
    /// </summary>
    /// <param name="loadingScreen">Gameobject a asignar.</param>
    public static void setLoadingScreen(GameObject loadingScreen)
    {
        _loadingScreen = loadingScreen;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_loadingScreen"/>.
    /// </summary>
    /// <returns><see cref="_loadingScreen"/>.</returns>
    public static GameObject getLoadingScreen()
    {
        return _loadingScreen;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemiesList"/>.
    /// </summary>
    /// <returns><see cref="_enemiesList"/>.</returns>
    public static List<GameObject> getEnemiesList()
    {
        return _enemiesList;
    }

    /// <summary>
    /// Método que añade un enemigo a la lista <see cref="_enemiesList"/>.
    /// </summary>
    /// <param name="enemy">Enemigo a añadir.</param>
    public static void addEnemy(GameObject enemy)
    {
        if (_enemiesList == null)
        {
            _enemiesList = new List<GameObject>();
        }

        if (!_enemiesList.Contains(enemy))
        {
            _enemiesList.Add(enemy);
        }
    }

}
