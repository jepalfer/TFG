using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// stateMachine es una clase que controla la máquina de estados del combate del jugador.
/// </summary>
public class stateMachine : MonoBehaviour
{
    /// <summary>
    /// Es el nombre de la máquina de estados.
    /// </summary>
    [SerializeField] private string _name;

    /// <summary>
    /// Es el tipo de estado principal.
    /// </summary>
    private state _mainStateType;

    /// <summary>
    /// Es el estado actual.
    /// </summary>
    private state _currentState;

    /// <summary>
    /// Es el estado siguiente.
    /// </summary>
    private state _nextState;

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica del programa.
    /// Cambia <see cref="_currentState"/> usando <see cref="SetState(state)"/> y lo actualiza.
    /// </summary>
    void Update()
    {
        //Si hay siguiente estado
        if (_nextState != null)
        {
            SetState(_nextState);
        }

        //Si hay estado actual
        if (_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }
    /// <summary>
    /// Método que sale de <see cref="_currentState"/> usando <see cref="state.OnExit()"/>, lo actualiza y entra en él con <see cref="state.OnEnter(stateMachine)"/>.
    /// </summary>
    /// <param name="_newState">Es el nuevo estado al que queremos pasar.</param>
    private void SetState(state _newState)
    {
        _nextState = null;
        if (_currentState != null)
        {
            //Si el estado actual no es nulo salimos de él
            _currentState.OnExit();
        }
        //Actualizamos el estado actual
        _currentState = _newState;
        _currentState.OnEnter(this);
    }

    /// <summary>
    /// Método que cambia <see cref="_nextState"/>.
    /// </summary>
    /// <param name="_newState">El estado que queremos que sea el siguiente al actual.</param>
    public void SetNextState(state _newState)
    {
        if (_newState != null)
        {
            _nextState = _newState;
        }
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica (después de todos los update).
    /// Actualiza <see cref="_currentState"/> con <see cref="state.OnLateUpdate()"/>.
    /// </summary>
    private void LateUpdate()
    {
        if (_currentState != null)
            _currentState.OnLateUpdate();
    }

    /// <summary>
    /// Método que se ejecuta cada cierto tiempo para actualizar la lógica.
    /// Actualiza <see cref="_currentState"/> con <see cref="state.OnFixedUpdate()"/>.
    /// </summary>
    private void FixedUpdate()
    {
        if (_currentState != null)
            _currentState.OnFixedUpdate();
    }

    /// <summary>
    /// Cambia el tipo de <see cref="_nextState"/> al de <see cref="_mainStateType"/>
    /// </summary>
    public void SetNextStateToMain()
    {
        _nextState = _mainStateType;
    }

    /// <summary>
    /// Método que se ejecuta el primero de todos al iniciar el script.
    /// Actualiza <see cref="_nextState"/> a <see cref="_mainStateType"/>.
    /// </summary>
    private void Awake()
    {
        SetNextStateToMain();

    }

    /// <summary>
    /// Método que se ejecuta al hacer un cambio en el inspector o al cargarlo.
    /// Establece <see cref="_mainStateType"/> como un <see cref="idleCombatState"/>.
    /// </summary>
    private void OnValidate()
    {
        if (_mainStateType == null)
        {
            _mainStateType = new idleCombatState();
        }
    }

    /// <summary>
    /// Getter que devuelve <see cref="_mainStateType"/>.
    /// </summary>
    /// <returns>Devuelve un estado</returns>
    public state getMainStateType()
    {
        return _mainStateType;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_currentState"/>.
    /// </summary>
    /// <returns>Devuelve un estado</returns>
    public state getCurrentState()
    {
        return _currentState;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_nextState"/>.
    /// </summary>
    /// <returns>Devuelve un estado</returns>
    public state getNextState()
    {
        return _nextState;
    }
    /// <summary>
    /// Getter que devuelve el script <see cref="combatController"/> asociado al jugador.
    /// </summary>
    /// <returns>Devuelve un objeto de tipo <see cref="combatController"/></returns>
    public combatController getPlayerCombat()
    {
        return GetComponent<combatController>();
    }
    /// <summary>
    /// Getter que devuelve el script <see cref="playerMovement"/> asociado al jugador.
    /// </summary>
    /// <returns>Devuelve un objeto de tipo <see cref="playerMovement"/></returns>
    public playerMovement getPlayerMovement()
    {
        return GetComponent<playerMovement>();
    }
}
