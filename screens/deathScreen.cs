using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// deathScreen es una clase que se encarga de controlar la pantalla de muerte.
/// </summary>
public class deathScreen : MonoBehaviour
{
    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    void Start()
    {
        config.setDeathScreen(gameObject);
    }

    /// <summary>
    /// Método que se encarga de reproducir la animación de pantalla de muerte.
    /// </summary>
    public void playAnimation()
    {
        GetComponent<Animator>().Play("deathScreen");
    }

    public void playerDeath()
    {
        config.getPlayer().GetComponent<combatController>().die();
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("deathScreen"))
        {
            if (stateInfo.normalizedTime >= 1.0f)
            {
                config.getPlayer().GetComponent<combatController>().die();
            }
        }
    }
}
