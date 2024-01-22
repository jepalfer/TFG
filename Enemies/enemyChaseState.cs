using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChaseState : enemyState
{
    public override void OnEnter(enemyStateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        //_stateMachine.getAnimator().SetTrigger("Chase");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Vector3.Distance(_currentStateMachine.GetComponent<enemyController>().gameObject.transform.position, config.getPlayer().transform.position) >= _currentStateMachine.GetComponent<enemyController>().getDetectionRange())
        {
            _currentStateMachine.SetNextStateToMain();
        }
        else
        {
            //Movemos al enemigo hacia el jugador
            if (_currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.x < config.getPlayer().transform.position.x)
            {
                if (!GetComponent<enemy>().getIsLookingRight())
                {
                    GetComponent<enemy>().flip();
                }
                _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position = new Vector3(_currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.x + (_currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime), _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.y, _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.z);
            }
            else
            {
                if (GetComponent<enemy>().getIsLookingRight())
                {
                    GetComponent<enemy>().flip();
                }
                _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position = new Vector3(_currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.x + (-_currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime), _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.y, _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.z);
            }
        }
    }
}
