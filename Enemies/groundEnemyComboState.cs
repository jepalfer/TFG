using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundEnemyComboState : meleeEnemyBaseState
{
    public groundEnemyComboState(float time, int counter) : base(time, counter) { }


    public override void OnEnter(enemyStateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        //Debug.Log("combo" + getAttackCounter().ToString());
        GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
        //_stateMachine.getAnimator().SetTrigger("Attack" + getAttackCounter().ToString());

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        /*
        if (Vector3.Distance(_currentStateMachine.GetComponent<Enemy>().gameObject.transform.position, Config.getPlayer().transform.position) <= _currentStateMachine.GetComponent<Enemy>().getDetectionRange() && Vector3.Distance(_currentStateMachine.GetComponent<Enemy>().gameObject.transform.position, Config.getPlayer().transform.position) >= (Config.getPlayer().GetComponent<BoxCollider2D>().size.x))
        {
            _currentStateMachine.SetNextState(new enemyChaseState());
        }
        else */
        if (_time >= _attackTime)
        {/*
            if (Vector3.Distance(_currentStateMachine.GetComponent<Enemy>().gameObject.transform.position, Config.getPlayer().transform.position) <= _currentStateMachine.GetComponent<Enemy>().getDetectionRange() && Vector3.Distance(_currentStateMachine.GetComponent<Enemy>().gameObject.transform.position, Config.getPlayer().transform.position) >= (Config.getPlayer().GetComponent<BoxCollider2D>().size.x))
            {
                _currentStateMachine.SetNextState(new enemyChaseState());
            }*/
            if (Vector3.Distance(_currentStateMachine.GetComponent<enemy>().gameObject.transform.position, config.getPlayer().transform.position) >= _currentStateMachine.GetComponent<enemy>().getAttackRange())
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.SetNextState(new enemyChaseState());
            }
            else if (_attackCounter == (_currentStateMachine.GetComponent<enemy>().getTimes().Count - 1))    //Ya ha acabado
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.SetNextState(new idleEnemyState());
            }
            else if (_attackCounter == (_currentStateMachine.GetComponent<enemy>().getTimes().Count - 2))   //Es finisher
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.SetNextState(new groundEnemyFinisherState(_currentStateMachine.GetComponent<enemy>().getTimes()[_attackCounter + 1], _attackCounter + 1));
            }
            else
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.SetNextState(new groundEnemyComboState(_currentStateMachine.GetComponent<enemy>().getTimes()[_attackCounter + 1], _attackCounter + 1));
            }
        }
    }
}
