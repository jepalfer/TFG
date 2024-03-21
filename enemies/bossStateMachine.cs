using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bossStateMachine es una clase que se utiliza para controlar los estados de los bosses.
/// </summary>
public class bossStateMachine : enemyStateMachine
{
    /// <summary>
    /// Método que se ejecuta al iniciar el script
    /// </summary>
    protected override void Start()
    {
        base.Start();
        _currentState = new enemyChaseState();
        _mainStateType = new enemyChaseState();
        
        //Para que no nos siga el boss hasta que entremos a su sala y lo activemos
        enabled = false;
    }
}
