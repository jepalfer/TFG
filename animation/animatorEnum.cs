using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// animatorEnum es un enum utilizado para los distintos nombres de las animaciones que se pueden reproducir.
/// </summary>
public enum animatorEnum
{
    /// <summary>
    /// Animaciones idle del personaje.
    /// </summary>
    player_idle,

    /// <summary>
    /// Animaciones de movimiento del personaje.
    /// </summary>
    player_run,
    
    /// <summary>
    /// Animaci�n de salto del personaje.
    /// </summary>
    player_jump,

    /// <summary>
    /// Animaci�n de dash del personaje.
    /// </summary>
    player_dash,

    /// <summary>
    /// Animaci�n de ca�da del personaje.
    /// </summary>
    player_fall,

    /// <summary>
    /// Animaci�n de aterrizaje del personaje.
    /// </summary>
    player_land,
    
    /// <summary>
    /// Animaci�n de doble salto del personaje
    /// </summary>
    player_double_jump,

    /// <summary>
    /// Animaci�n de moverse en el aire del personaje.
    /// </summary>
    player_move_air,

    /// <summary>
    /// Animaci�n de atacar en el aire del personaje.
    /// </summary>
    player_attack_air,

    /// <summary>
    /// Animaci�n para levantarse del suelo.
    /// </summary>
    player_get_up,

    /// <summary>
    /// Animaci�n de escalar.
    /// </summary>
    player_climb,

    /// <summary>
    /// Si es hacia arriba.
    /// </summary>
    up,

    /// <summary>
    /// Si es hacia abajo.
    /// </summary>
    down,

    /// <summary>
    /// Animaciones de ataque del personaje.
    /// </summary>
    player_attack,

    /// <summary>
    /// Animacion de ataque del personaje.
    /// </summary>
    enemy_attack,

    /// <summary>
    /// Animaciones de movimiento del enemigo.
    /// </summary>
    enemy_move,

    /// <summary>
    /// Modelo de frente.
    /// </summary>
    front,

    /// <summary>
    /// Modelo de espaldas.
    /// </summary>
    back,

    /// <summary>
    /// Animaci�n para la muerte del enemigo.
    /// </summary>
    enemy_death,

    /// <summary>
    /// Animaci�n idle de los enemigos.
    /// </summary>
    enemy_idle,

    /// <summary>
    /// Animaci�n de muerte del jugador.
    /// </summary>
    player_death,

    /// <summary>
    /// Animaci�n de envainado.
    /// </summary>
    enemy_seath,

    /// <summary>
    /// Animaci�n de desenvainado.
    /// </summary>
    enemy_unseath,

}
