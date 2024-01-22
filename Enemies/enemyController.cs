using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private GameObject _player;
    private enemyStateMachine _stateMachine;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _sightDistance;

    private void Start()
    {
        _player = config.getPlayer();
        _stateMachine = GetComponent<enemyStateMachine>();
        _stateMachine.SetNextStateToMain();
        _movementSpeed = GetComponent<enemy>().getSpeed();
        _sightDistance = GetComponent<enemy>().getDetectionRange();
        Physics2D.IgnoreCollision(_player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= _sightDistance && Vector3.Distance(transform.position, _player.transform.position) >= GetComponent<enemy>().getAttackRange())
        {
            if (_stateMachine.getCurrentState().GetType() == typeof(enemyChaseState) || _stateMachine.getCurrentState().GetType() == typeof(idleEnemyState))
            {
                _stateMachine.SetNextState(new enemyChaseState());
            }
        }/*
        else if (Vector3.Distance(transform.position, _player.transform.position) <= ((_player.GetComponent<BoxCollider2D>().size.x / 2.0) + (GetComponent<BoxCollider2D>().size.x / 2.0)))
        {
            Debug.Log("a");
        }*/
        else if (Vector3.Distance(transform.position, _player.transform.position) <= GetComponent<enemy>().getAttackRange())
        {
            if (_stateMachine.getCurrentState().GetType() == typeof(enemyChaseState))
            {
                _stateMachine.SetNextState(new groundEnemyEntryState(GetComponent<enemy>().getTimes()[0], 0));
            }
        }
    }

    public GameObject getPlayer()
    {
        return _player;
    }

    public float getSpeed()
    {
        return _movementSpeed;
    }

    public float getDetectionRange()
    {
        return _sightDistance;
    }
}
