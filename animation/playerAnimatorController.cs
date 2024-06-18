using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// playerAnimatorController es una clase que se usa para controlar las animaciones del personaje.
/// </summary>
public class playerAnimatorController : MonoBehaviour
{
    /// <summary>
    /// Referencia al Animator del jugador.
    /// </summary>
    [Header("Animation")]
    private Animator _animator;

    /// <summary>
    /// Referencia a la acción que se está realizando para no cambiar la animación.
    /// </summary>
    private string _currentAction;

    /// <summary>
    /// Método que se ejecuta al inicio.
    /// </summary>
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Método que se encarga de reproducir la animación correspondiente.
    /// </summary>
    /// <param name="action">Acción a reproducir.</param>
    /// <param name="weaponID">ID del arma en caso de que sea animación de ataque.</param>
    /// <param name="animationID">ID de la animación.</param>
    /// <param name="facing">El lado al que está mirando el personaje.</param>
    public void playAnimation(animatorEnum action, int weaponID, int animationID, animatorEnum facing)
    {
        GetComponent<Animator>().speed = 1;
        string newAction = action.ToString() + "_" + weaponID.ToString() + "_" + animationID.ToString() + "_" + facing.ToString();
        
        if (_currentAction == newAction) return;

        _animator.Play(newAction);
        _animator.Update(0);
        _currentAction = newAction;
    }

    /// <summary>
    /// Método que se encarga de reproducir la animación correspondiente.
    /// </summary>
    /// <param name="action">Acción a reproducir.</param>
    /// <param name="animationID">ID de la animación.</param>
    /// <param name="facing">El lado al que está mirando el personaje.</param>
    public void playAnimation(animatorEnum action, int animationID, animatorEnum facing)
    {
        GetComponent<Animator>().speed = 1;
        string newAction = action.ToString() + "_" + animationID.ToString() + "_" + facing.ToString();
        if (_currentAction == newAction) return;
        //Debug.Log(newAction);
        _animator.Play(newAction);
        _animator.Update(0);
        _currentAction = newAction;
    }

    /// <summary>
    /// Método que se encarga de reproducir la animación correspondiente.
    /// </summary>
    /// <param name="action">Acción a reproducir.</param>
    /// <param name="facing">El lado al que está mirando el personaje.</param>
    public void playAnimation(animatorEnum action, animatorEnum facing)
    {
        GetComponent<Animator>().speed = 1;
        if (action == animatorEnum.player_get_up)
        {
            GetComponent<Animator>().speed = 2;
        }
        string newAction = action.ToString() + "_" + facing.ToString();
        if (_currentAction == newAction) return;
        _animator.Play(newAction);
        _animator.Update(0);
        _currentAction = newAction;
    }

    /// <summary>
    /// Método auxiliar para obtener el nombre de la animación.
    /// </summary>
    /// <param name="action">Acción a reproducir.</param>
    /// <param name="weaponID">ID del arma.</param>
    /// <param name="animationID">ID de la animación.</param>
    /// <param name="facing">El lado al que está mirando el personaje.</param>
    /// <returns>Nombre de la animación.</returns>
    public string getAnimationName(animatorEnum action, int weaponID, int animationID, animatorEnum facing)
    {
        return action.ToString() + "_" + weaponID.ToString() + "_" + animationID.ToString() + "_" + facing.ToString();
    }

    /// <summary>
    /// Método auxiliar para obtener el nombre de la animación.
    /// </summary>
    /// <param name="action">Acción a reproducir.</param>
    /// <param name="animationID">ID de la animación.</param>
    /// <param name="facing">El lado al que está mirando el personaje.</param>
    /// <returns>Nombre de la animación.</returns>
    public string getAnimationName(animatorEnum action, int animationID, animatorEnum facing)
    {
        return action.ToString() + "_" + animationID.ToString() + "_" + facing.ToString();
    }

    /// <summary>
    /// Método auxiliar para obtener el nombre de la animación.
    /// </summary>
    /// <param name="action">Acción a reproducir.</param>
    /// <param name="facing">El lado al que está mirando el personaje.</param>
    /// <returns>Nombre de la animación.</returns>
    public string getAnimationName(animatorEnum action, animatorEnum facing)
    {
        return action.ToString() + "_" + facing.ToString();
    }
}
