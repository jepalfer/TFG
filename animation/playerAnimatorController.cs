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
    /// Referencia a la acci�n que se est� realizando para no cambiar la animaci�n.
    /// </summary>
    private string _currentAction;

    /// <summary>
    /// M�todo que se ejecuta al inicio.
    /// </summary>
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// M�todo que se encarga de reproducir la animaci�n correspondiente.
    /// </summary>
    /// <param name="action">Acci�n a reproducir.</param>
    /// <param name="weaponID">ID del arma en caso de que sea animaci�n de ataque.</param>
    /// <param name="animationID">ID de la animaci�n.</param>
    /// <param name="facing">El lado al que est� mirando el personaje.</param>
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
    /// M�todo que se encarga de reproducir la animaci�n correspondiente.
    /// </summary>
    /// <param name="action">Acci�n a reproducir.</param>
    /// <param name="animationID">ID de la animaci�n.</param>
    /// <param name="facing">El lado al que est� mirando el personaje.</param>
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
    /// M�todo que se encarga de reproducir la animaci�n correspondiente.
    /// </summary>
    /// <param name="action">Acci�n a reproducir.</param>
    /// <param name="facing">El lado al que est� mirando el personaje.</param>
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
    /// M�todo auxiliar para obtener el nombre de la animaci�n.
    /// </summary>
    /// <param name="action">Acci�n a reproducir.</param>
    /// <param name="weaponID">ID del arma.</param>
    /// <param name="animationID">ID de la animaci�n.</param>
    /// <param name="facing">El lado al que est� mirando el personaje.</param>
    /// <returns>Nombre de la animaci�n.</returns>
    public string getAnimationName(animatorEnum action, int weaponID, int animationID, animatorEnum facing)
    {
        return action.ToString() + "_" + weaponID.ToString() + "_" + animationID.ToString() + "_" + facing.ToString();
    }

    /// <summary>
    /// M�todo auxiliar para obtener el nombre de la animaci�n.
    /// </summary>
    /// <param name="action">Acci�n a reproducir.</param>
    /// <param name="animationID">ID de la animaci�n.</param>
    /// <param name="facing">El lado al que est� mirando el personaje.</param>
    /// <returns>Nombre de la animaci�n.</returns>
    public string getAnimationName(animatorEnum action, int animationID, animatorEnum facing)
    {
        return action.ToString() + "_" + animationID.ToString() + "_" + facing.ToString();
    }

    /// <summary>
    /// M�todo auxiliar para obtener el nombre de la animaci�n.
    /// </summary>
    /// <param name="action">Acci�n a reproducir.</param>
    /// <param name="facing">El lado al que est� mirando el personaje.</param>
    /// <returns>Nombre de la animaci�n.</returns>
    public string getAnimationName(animatorEnum action, animatorEnum facing)
    {
        return action.ToString() + "_" + facing.ToString();
    }
}
