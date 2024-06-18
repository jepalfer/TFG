using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemyAnimatorController es una clase que se usa para controlar las animaciones de los enemigos.
/// </summary>
public class enemyAnimatorController : MonoBehaviour
{
    /// <summary>
    /// El animator para las animaciones de ataques.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Referencia a la acción que se está realizando para no cambiar la animación.
    /// </summary>
    private string _currentAction;
    
    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Método que se encarga de reproducir las animaciones.
    /// </summary>
    /// <param name="action">Acción que se anima.</param>
    /// <param name="animationID">ID de la animación.</param>
    /// <param name="facing">El lado hacia el que mira el personaje.</param>
    public void playAnimation(animatorEnum action, string enemyName, int animationID, animatorEnum facing)
    {
        string newAction = action.ToString() + "_" + enemyName + "_" + animationID.ToString() + "_" + facing.ToString();
        //Debug.Log(newAction);
        if (_currentAction == newAction) return;
        _animator.Play(newAction);
        _animator.Update(0);
        _currentAction = newAction;
    }

    /// <summary>
    /// Método que se encarga de reproducir la animación correspondiente.
    /// </summary>
    /// <param name="action">Acción a reproducir.</param>
    /// <param name="facing">El lado al que está mirando el personaje.</param>
    public void playAnimation(animatorEnum action, string enemyName, animatorEnum facing)
    {
        //Debug.Log(enemyName);
        GetComponent<Animator>().speed = 1;
        string newAction = action.ToString() + "_" + enemyName + "_" + facing.ToString();
        //Debug.Log(newAction);
        if (_currentAction == newAction) return;
        _animator.Play(newAction);
        _animator.Update(0);
        _currentAction = newAction;
    }


    /// <summary>
    /// Método auxiliar para obtener el nombre de la animación.
    /// </summary>
    /// <param name="action">Acción a reproducir.</param>
    /// <param name="animationID">ID de la animación.</param>
    /// <param name="facing">El lado al que está mirando el personaje.</param>
    /// <returns>Nombre de la animación.</returns>
    public string getAnimationName(animatorEnum action, string enemyName, int animationID, animatorEnum facing)
    {
        return action.ToString() + "_" + enemyName + "_" + animationID.ToString() + "_" + facing.ToString();
    }

    /// <summary>
    /// Método auxiliar para obtener el nombre de la animación.
    /// </summary>
    /// <param name="action">Acción a reproducir.</param>
    /// <param name="facing">El lado al que está mirando el personaje.</param>
    /// <returns>Nombre de la animación.</returns>
    public string getAnimationName(animatorEnum action, string enemyName, animatorEnum facing)
    {
        return action.ToString() + "_" + enemyName + "_" + facing.ToString();
    }
}
