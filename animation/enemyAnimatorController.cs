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
    /// Referencia a la acci�n que se est� realizando para no cambiar la animaci�n.
    /// </summary>
    private string _currentAction;
    
    /// <summary>
    /// M�todo que se ejecuta al inicio del script.
    /// </summary>
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// M�todo que se encarga de reproducir las animaciones.
    /// </summary>
    /// <param name="action">Acci�n que se anima.</param>
    /// <param name="animationID">ID de la animaci�n.</param>
    /// <param name="facing">El lado hacia el que mira el personaje.</param>
    public void playAnimation(animatorEnum action, int animationID, animatorEnum facing)
    {
        string newAction = "";
        switch (action)
        {
            case animatorEnum.enemy_move:
                newAction = action.ToString() + "_" + facing.ToString();
                //Debug.Log(newAction);
                if (_currentAction == newAction) return;
                _animator.Play(newAction);
                _currentAction = newAction;
                break;

        }
    }
}
