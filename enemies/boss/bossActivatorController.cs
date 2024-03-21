using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bossActivatorController es una clase utilizada para controlar la activación de los bosses.
/// </summary>
public class bossActivatorController : MonoBehaviour
{
    /// <summary>
    /// Referencia al boss que activa.
    /// </summary>
    [SerializeField] private GameObject _boss;

    /// <summary>
    /// Referencia a la pared que se activa detrás nuestra para que no salgamos de la sala de jefe.
    /// </summary>
    [SerializeField] private GameObject _bossWall;

    /// <summary>
    /// Método que se ejecuta al entrar con el collider del objeto <see cref="this"/>.
    /// </summary>
    /// <param name="collision">Colisión que entra en contacto.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si es el jugador
        if (GetComponent<BoxCollider2D>().enabled && collision.gameObject.GetComponent<playerMovement>() != null)
        {
            //desactivamos el trigger
            GetComponent<BoxCollider2D>().enabled = false;

            //activamos la pared
            _bossWall.GetComponent<BoxCollider2D>().enabled = true;

            //activamos todos los scripts del jefe
            foreach (MonoBehaviour script in _boss.GetComponents<MonoBehaviour>())
            {
                script.enabled = true;
            }
        }
    }
}
