using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// state es una clase que representa cada estado de la máquina de estados de combate.
/// </summary>
public abstract class state
{
    /// <summary>
    /// Es la máquina de estados actual.
    /// </summary>
    protected stateMachine _currentStateMachine;

    /// <summary>
    /// Es el tiempo que se modifica en <see cref="onUpdate()"/>.
    /// </summary>
    protected float _time;

    /// <summary>
    /// Es el tiempo que se modifica en <see cref="onFixedUpdate()"/>.
    /// </summary>
    protected float _fixedTime;

    /// <summary>
    /// Es el tiempo que se modifica en <see cref="onLateUpdate()"/>.
    /// </summary>
    protected float _lateTime;

    /// <summary>
    /// Es un booleano que indica si hemos golpeado con el arma primaria o no.
    /// </summary>
    protected bool _isPrimary;

    /// <summary>
    /// Método que se usa cuando entramos a un estado.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados que se asocia.</param>
    public virtual void onEnter(stateMachine _stateMachine)
    {
        _currentStateMachine = _stateMachine;
        //GetComponent<statsController>().useStamina(GetComponent<combatController>().getDashStaminaUse());
    }

    /// <summary>
    /// Método que se usa cuando salimos de un estado.
    /// </summary>
    public virtual void onExit()
    {
        GetComponent<playerMovement>().setCanMove(true);
        GetComponent<playerMovement>().setCanRoll(GetComponent<playerMovement>().getCouldRoll());
        GetComponent<combatController>().setIsAttacking(false);
        GetComponent<combatController>().setCanAttack(false);
        GetComponent<playerMovement>().setDistanceJumped(GetComponent<playerMovement>().getJumpHeight());


        if (GetComponent<combatController>().getPrimaryWeapon() != null)
        {
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCurrentAttack(0);
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setIsAttacking(false);
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCanAttack(false);
        }
        if (GetComponent<combatController>().getSecundaryWeapon() != null)
        {
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCurrentAttack(0);
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setIsAttacking(false);
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCanAttack(false);
        }
        GetComponent<playerMovement>().getRigidBody().gravityScale = GetComponent<playerMovement>().getInitialGravity();
    }

    /// <summary>
    /// Método que suma a <see cref="_time"/> el valor proporcionado por <see cref="Time.deltaTime"/>.
    /// </summary>
    public virtual void onUpdate()
    {
        _time += Time.deltaTime;
    }
    /// <summary>
    /// Método que suma a <see cref="_fixedTime"/> el valor proporcionado por <see cref="Time.fixedDeltaTime"/>.
    /// </summary>
    public virtual void onFixedUpdate()
    {
        _fixedTime += Time.fixedDeltaTime;
    }
    /// <summary>
    /// Método que suma a <see cref="_lateTime"/> el valor proporcionado por <see cref="Time.deltaTime"/>.
    /// </summary>
    public virtual void onLateUpdate()
    {
        _lateTime += Time.deltaTime;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_time"/>.
    /// </summary>
    /// <returns>Un float que es el tiempo transcurrido.</returns>
    public float getTime()
    {
        return _time;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_fixedTime"/>.
    /// </summary>
    /// <returns>Un float que es el tiempo transcurrido.</returns>
    public float getFixedTime()
    {
        return _fixedTime;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_lateTime"/>.
    /// </summary>
    /// <returns>Un float que es el tiempo transcurrido.</returns>
    public float getLateTime()
    {
        return _lateTime;
    }
    /// <summary>
    /// Setter que modifica el atributo <see cref="_time"/> asignándole el parámetro.
    /// </summary>
    /// <param name="time">El tiempo a asignar.</param>
    public void setTime(float time)
    {
        _time = time;
    }
    /// <summary>
    /// Setter que modifica el atributo <see cref="_fixedTime"/> asignándole el parámetro.
    /// </summary>
    /// <param name="time">El tiempo a asignar.</param>
    public void setFixedTime(float time)
    {
        _fixedTime = time;
    }
    /// <summary>
    /// Setter que modifica el atributo <see cref="_lateTime"/> asignándole el parámetro.
    /// </summary>
    /// <param name="time">El tiempo a asignar.</param>
    public void setLateTime(float time)
    {
        _lateTime = time;
    }
    /// <summary>
    /// Destruye un objeto.
    /// </summary>
    /// <param name="obj">El objeto a destruir.</param>
    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }
    /// <summary>
    /// Devuelve un componente de tipo T.
    /// </summary>
    /// <typeparam name="T">El tipo a devolver.</typeparam>
    /// <returns></returns>
    protected T GetComponent<T>() where T : Component { return _currentStateMachine.GetComponent<T>(); }
    
    /*
    protected Component GetComponent(System.Type type) { return _currentStateMachine.GetComponent(type); }
    protected Component GetComponent(string type) { return _currentStateMachine.GetComponent(type); }*/
}
