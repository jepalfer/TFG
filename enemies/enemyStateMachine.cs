using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemyStateMachine es una clase que representa la máquina de estados para el combate de los enemigos.
/// </summary>
public class enemyStateMachine : MonoBehaviour
{
    /// <summary>
    /// El nombre de la máquina de estados.
    /// </summary>
    [SerializeField] protected string _name;

    /// <summary>
    /// El animator para las animaciones de ataques.
    /// </summary>
    [SerializeField] protected Animator _animator;
    /// <summary>
    /// El estado principal.
    /// </summary>
    protected enemyState _mainStateType;

    /// <summary>
    /// El estado actual.
    /// </summary>
    protected enemyState _currentState;

    /// <summary>
    /// El próximo estado.
    /// </summary>
    protected enemyState _nextState;

    /// <summary>
    /// Método que se ejecuta al inicio del script e inicializa <see cref="_currentState"/>.
    /// </summary>
    protected virtual void Start()
    {
        _currentState = new idleEnemyState();
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// Actualiza el estado actual.
    /// </summary>
    void Update()
    {
        if (_nextState != null)
        {
            setState(_nextState);
        }

        if (_currentState != null)
        {
            _currentState.onUpdate();
        }
    }

    /// <summary>
    /// Método que si ya estamos en un estado sale de él y después lo actualiza.
    /// </summary>
    /// <param name="_newState">El nuevo estado al que pasamos.</param>
    private void setState(enemyState _newState)
    {
        _nextState = null;
        if (_currentState != null)
        {
            _currentState.onExit();
        }
        _currentState = _newState;
        _currentState.onEnter(this);
    }

    /// <summary>
    /// Método que modifica <see cref="_nextState"/>.
    /// </summary>
    /// <param name="_newState"></param>
    public void setNextState(enemyState _newState)
    {
        if (_newState != null)
        {
            _nextState = _newState;
        }
    }

    /// <summary>
    /// Método que actualiza <see cref="_currentState"/> con el método <see cref="enemyState.onLateUpdate()"/>.
    /// </summary>
    private void LateUpdate()
    {
        if (_currentState != null)
            _currentState.onLateUpdate();
    }

    /// <summary>
    /// Método que actualiza <see cref="_currentState"/> con el método <see cref="enemyState.onFixedUpdate()"/>.
    /// </summary>
    private void FixedUpdate()
    {
        if (_currentState != null)
            _currentState.onFixedUpdate();
    }

    /// <summary>
    /// Método que modficia <see cref="_nextState"/>.
    /// </summary>
    public void setNextStateToMain()
    {
        _nextState = _mainStateType;
    }

    /// <summary>
    /// Método que se ejecuta al iniciar el script y modifica <see cref="_nextState"/>.
    /// </summary>
    private void Awake()
    {
        if (_mainStateType == null)
        {
            _mainStateType = new idleEnemyState();
        }
        setNextStateToMain();
    }

    /// <summary>
    /// Método que se ejecuta al asignar el script o modificar algún atributo en el editor. Asigna a <see cref="_mainStateType"/> una instancia de tipo <see cref="idleEnemyState"/>.
    /// </summary>
    private void OnValidate()
    {
        if (_mainStateType == null)
        {
            _mainStateType = new idleEnemyState();
        }
    }

    /// <summary>
    /// Getter que devuelve <see cref="_mainStateType"/>.
    /// </summary>
    /// <returns>Un estado que representa el estado principal.</returns>
    public enemyState getMainStateType()
    {
        return _mainStateType;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_currentState"/>.
    /// </summary>
    /// <returns>Un estado que representa el estado actual.</returns>
    public enemyState getCurrentState()
    {
        return _currentState;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_nextState"/>.
    /// </summary>
    /// <returns>Un estado que representa el estado siguiente.</returns>
    public enemyState getNextState()
    {
        return _nextState;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_animator"/>.
    /// </summary>
    /// <returns>El animator de la máquina de estados.</returns>
    public Animator getAnimator()
    {
        return _animator;
    }
}
