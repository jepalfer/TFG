using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateMachine : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Animator _animator;
    private enemyState _mainStateType;
    private enemyState _currentState;
    private enemyState _nextState;
    private void Start()
    {
        _currentState = new idleEnemyState();
    }

    void Update()
    {
        if (_nextState != null)
        {
            SetState(_nextState);
        }

        if (_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }
    private void SetState(enemyState _newState)
    {
        _nextState = null;
        if (_currentState != null)
        {
            _currentState.OnExit();
        }
        _currentState = _newState;
        _currentState.OnEnter(this);
    }

    public void SetNextState(enemyState _newState)
    {
        if (_newState != null)
        {
            _nextState = _newState;
        }
    }

    private void LateUpdate()
    {
        if (_currentState != null)
            _currentState.OnLateUpdate();
    }

    private void FixedUpdate()
    {
        if (_currentState != null)
            _currentState.OnFixedUpdate();
    }

    public void SetNextStateToMain()
    {
        _nextState = _mainStateType;
    }

    private void Awake()
    {
        SetNextStateToMain();
    }


    private void OnValidate()
    {
        if (_mainStateType == null)
        {
            _mainStateType = new idleEnemyState();
        }
    }

    public enemyState getMainStateType()
    {
        return _mainStateType;
    }
    public enemyState getCurrentState()
    {
        return _currentState;
    }
    public enemyState getNextState()
    {
        return _nextState;
    }

    public Animator getAnimator()
    {
        return _animator;
    }
}
