using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class enemyState
{
    protected enemyStateMachine _currentStateMachine;
    protected float _time;
    protected float _fixedTime;
    protected float _lateTime;

    public virtual void OnEnter(enemyStateMachine _stateMachine)
    {
        _currentStateMachine = _stateMachine;
    }

    public virtual void OnExit()
    {
    }
    public virtual void OnUpdate()
    {
        _time += Time.deltaTime;
    }
    public virtual void OnFixedUpdate()
    {
        _fixedTime += Time.deltaTime;
    }
    public virtual void OnLateUpdate()
    {
        _lateTime += Time.deltaTime;
    }
    public float getTime()
    {
        return _time;
    }
    public float getFixedTime()
    {
        return _fixedTime;
    }
    public float getLateTime()
    {
        return _lateTime;
    }
    public void setTime(float time)
    {
        _time = time;
    }
    public void setFixedTime(float time)
    {
        _fixedTime = time;
    }
    public void setLateTime(float time)
    {
        _lateTime = time;
    }
    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }
    protected T GetComponent<T>() where T : Component { return _currentStateMachine.GetComponent<T>(); }
    protected Component GetComponent(System.Type type) { return _currentStateMachine.GetComponent(type); }
    protected Component GetComponent(string type) { return _currentStateMachine.GetComponent(type); }
}
