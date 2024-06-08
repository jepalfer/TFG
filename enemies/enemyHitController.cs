using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemyHitController es una clase que controla si un enemigo recibe da�o cuando entra en contacto con la hurtbox del jugador.
/// </summary>
public class enemyHitController : MonoBehaviour
{
    /// <summary>
    /// La referencia al enemigo.
    /// </summary>
    [SerializeField] private GameObject _enemy;

    /// <summary>
    /// M�todo que se ejecuta cuando un trigger entra en contacto y hace que el enemigo reciba da�o.
    /// </summary>
    /// <param name="collision">Collider que ha entrado en contacto.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerHurtbox"))
        {
            config.getPlayer().GetComponent<statsController>().receiveDMG(_enemy.GetComponent<enemy>().getDamage());
            config.getPlayer().GetComponent<characterSFXController>().playHurtSFX();
        }
    }

}
