using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// laderInteraction es una clase que se ocupa de la lógica de las escaleras.
/// </summary>
public class ladderInteraction : MonoBehaviour
{
    /// <summary>
    /// Gravedad del jugador.
    /// </summary>
    private float _gravity;

    /// <summary>
    /// Velocidad del rigidBody.
    /// </summary>
    private Vector2 _newVelocity;

    /// <summary>
    /// Método que se ejecuta cuando el jugador entra en contacto con la escalera.
    /// </summary>
    /// <param name="collision">Colisión que entra en contacto.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprobamos que sea del jugador
        if (collision.gameObject.GetComponent<playerMovement>() != null)
        {
            //Si no estamos en medio de una esquiva podemos escalar
            if (!config.getPlayer().GetComponent<playerMovement>().getIsDodging())
            {
                config.getPlayer().GetComponent<playerMovement>().setCanClimb(true);
                config.getPlayer().GetComponent<playerMovement>().setCouldClimb(true);

            }
        }
    }

    /// <summary>
    /// Método que se ejecuta cuando el jugador deja de estar en contacto con la escalera.
    /// </summary>
    /// <param name="collision">Colisión que deja de estar en contacto.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Comprobamos que sea del jugador
        if (collision.gameObject.GetComponent<playerMovement>() != null)
        {
            //Obtenemos la velocidad y gravedad del rigidBody
            _newVelocity = config.getPlayer().GetComponent<playerMovement>().getRigidBodyVelocity();
            _gravity = config.getPlayer().GetComponent<playerMovement>().getGravity();

            //Si no estamos esquivando asignamos la nueva gravedad y velocidad
            if (!config.getPlayer().GetComponent<playerMovement>().getIsDodging())
            {
                config.getPlayer().GetComponent<playerMovement>().setGravity(_gravity);

                config.getPlayer().GetComponent<playerMovement>().setRigidBodyVelocity(_newVelocity);
            }

            //Asignamos variables
            config.getPlayer().GetComponent<playerMovement>().setCanClimb(false);
            config.getPlayer().GetComponent<playerMovement>().setCouldClimb(false);

            if (!config.getPlayer().GetComponent<playerMovement>().getHasRolled())
            {
                config.getPlayer().GetComponent<playerMovement>().setCanRoll(true);
            }
        }
    }

    /// <summary>
    /// Método que se ejecuta cuando el jugador está en contacto con la escalera.
    /// </summary>
    /// <param name="collision">Colisión que está en contacto.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Comprobamos que sea del jugador
        if (collision.gameObject.GetComponent<playerMovement>() != null)
        {
            //Si el jugador no está esquivando
            if (!config.getPlayer().GetComponent<playerMovement>().getIsDodging())
            {
                config.getPlayer().GetComponent<playerMovement>().setCanClimb(true);
                config.getPlayer().GetComponent<playerMovement>().setCouldClimb(true);
            }

            //Si ha atacado
            if (config.getPlayer().GetComponent<combatController>().getIsAttacking())
            {
                config.getPlayer().GetComponent<playerMovement>().setCanClimb(false);
                config.getPlayer().GetComponent<playerMovement>().setCouldClimb(false);
            }
        }
    }
}
