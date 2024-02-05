using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// enemyState es una clase que representa los estados en la máquina de estados de combate del enemigo.
/// </summary>
public abstract class enemyState
{
    /// <summary>
    /// La máquina de estados asociada.
    /// </summary>
    protected enemyStateMachine _currentStateMachine;
    /// <summary>
    /// El time que se actualiza en <see cref="onUpdate()"/>
    /// </summary>
    protected float _time;

    /// <summary>
    /// El tiempo que se actualiza en <see cref="onFixedUpdate()"/>
    /// </summary>
    protected float _fixedTime;

    /// <summary>
    /// El tiempo que se actualiza en <see cref="onLateUpdate()"/>
    /// </summary>
    protected float _lateTime;

    /// <summary>
    /// El constructor con parámetros de la clase que asigna una máquina de estados a <see cref="_currentStateMachine"/>.
    /// </summary>
    /// <param name="_stateMachine"></param>
    public virtual void onEnter(enemyStateMachine _stateMachine)
    {
        Debug.Log("hey");
        _currentStateMachine = _stateMachine;
    }

    /// <summary>
    /// Método que se llama al salir de un estado.
    /// </summary>
    public virtual void onExit()
    {
    }

    /// <summary>
    /// Método que se llama al actualizar un estado. Actualiza <see cref="_time"/>.
    /// </summary>
    public virtual void onUpdate()
    {
        _time += Time.deltaTime;
    }

    /// <summary>
    /// Método que se llama al actualizar un estado. Actualiza <see cref="_fixedTime"/>.
    /// </summary>
    public virtual void onFixedUpdate()
    {
        _fixedTime += Time.deltaTime;
    }

    /// <summary>
    /// Método que se llama al actualizar un estado. Actualiza <see cref="_lateTime"/>.
    /// </summary>
    public virtual void onLateUpdate()
    {
        _lateTime += Time.deltaTime;
    }

    /// <summary>
    /// Getter que devuelve el tiempo que se actualiza en <see cref="onUpdate()"/>
    /// </summary>
    /// <returns>Un float que representa el tiempo dentro del estado.</returns>
    public float getTime()
    {
        return _time;
    }

    /// <summary>
    /// Getter que devuelve el tiempo que se actualiza en <see cref="onFixedUpdate()"/>
    /// </summary>
    /// <returns>Un float que representa el tiempo dentro del estado.</returns>
    public float getFixedTime()
    {
        return _fixedTime;
    }

    /// <summary>
    /// Getter que devuelve el tiempo que se actualiza en <see cref="onLateUpdate()"/>
    /// </summary>
    /// <returns>Un float que representa el tiempo dentro del estado.</returns>
    public float getLateTime()
    {
        return _lateTime;
    }

    /// <summary>
    /// Setter que asigna un valor a <see cref="_time"/>
    /// </summary>
    /// <param name="time">El valor asignado.</param>
    public void setTime(float time)
    {
        _time = time;
    }
    /// <summary>
    /// Setter que asigna un valor a <see cref="_fixedTime"/>
    /// </summary>
    /// <param name="time">El valor asignado.</param>
    public void setFixedTime(float time)
    {
        _fixedTime = time;
    }
    /// <summary>
    /// Setter que asigna un valor a <see cref="_lateTime"/>
    /// </summary>
    /// <param name="time">El valor asignado.</param>
    public void setLateTime(float time)
    {
        _lateTime = time;
    }

    /// <summary>
    /// Método que destruye un objeto.
    /// </summary>
    /// <param name="obj">El objeto a destruir.</param>
    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }
    /// <summary>
    /// Método que devuelve un componente de tipo T.
    /// </summary>
    /// <typeparam name="T">El tipo del componente a devolver.</typeparam>
    /// <returns></returns>
    protected T GetComponent<T>() where T : Component { return _currentStateMachine.GetComponent<T>(); }
/*    protected Component GetComponent(System.Type type) { return _currentStateMachine.GetComponent(type); }
    protected Component GetComponent(string type) { return _currentStateMachine.GetComponent(type); }*/
}
