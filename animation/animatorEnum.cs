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
    /// Animación de salto del personaje.
    /// </summary>
    player_jump,

    /// <summary>
    /// Animación de dash del personaje.
    /// </summary>
    player_dash,

    /// <summary>
    /// Animación de caída del personaje.
    /// </summary>
    player_fall,

    /// <summary>
    /// Animación de aterrizaje del personaje.
    /// </summary>
    player_land,
    
    /// <summary>
    /// Animación de doble salto del personaje
    /// </summary>
    player_double_jump,

    /// <summary>
    /// Animación de moverse en el aire del personaje.
    /// </summary>
    player_move_air,

    /// <summary>
    /// Animación de atacar en el aire del personaje.
    /// </summary>
    player_attack_air,

    /// <summary>
    /// Animación para levantarse del suelo.
    /// </summary>
    player_get_up,

    /// <summary>
    /// Animación de escalar.
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
    /// Animación para la muerte del enemigo.
    /// </summary>
    enemy_death,

    /// <summary>
    /// Animación idle de los enemigos.
    /// </summary>
    enemy_idle,

    /// <summary>
    /// Animación de muerte del jugador.
    /// </summary>
    player_death,

    /// <summary>
    /// Animación de envainado.
    /// </summary>
    enemy_seath,

    /// <summary>
    /// Animación de desenvainado.
    /// </summary>
    enemy_unseath,

}
