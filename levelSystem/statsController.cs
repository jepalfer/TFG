using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// statsController es una clase utilizada para controlar la vida y stamina actual.
/// </summary>
public class statsController : MonoBehaviour
{
    /// <summary>
    /// Valor máximo de los HP del jugador.
    /// </summary>
    private float _maxHP;

    /// <summary>
    /// Valor actual de los HP del jugador.
    /// </summary>
    private float _currentHP;

    /// <summary>
    /// Valor máximo de stamina del jugador.
    /// </summary>
    private float _maxStamina;

    /// <summary>
    /// Valor actual de stamina del jugador.
    /// </summary>
    private float _currentStamina;

    /// <summary>
    /// Cantidad de almas de las que dispone el jugador.
    /// </summary>
    private long _currentSouls;

    /// <summary>
    /// Factor de crecimiento de la barra de HP de la UI.
    /// </summary>
    private int _HPBarExp = 30;

    /// <summary>
    /// Factor de crecimiento de la barra de stamina de la UI.
    /// </summary>
    private int _StaminaBarExp = 30;

    /// <summary>
    /// Factor de crecimiento de los HP por nivel de vitalidad.
    /// </summary>
    private int _maxHPExp = 200;

    /// <summary>
    /// Factor de crecimiento de la stamina por nivel de resistencia.
    /// </summary>
    private int _maxStaminaExp = 20;

    /// <summary>
    /// Flag booleano para saber si nos estamos curando.
    /// </summary>
    private bool _isHealing;

    /// <summary>
    /// Timer para controlar el tiempo de cura.
    /// </summary>
    private float _timeHealing;

    /// <summary>
    /// Valor que controla el tiempo total que nos podemos curar.
    /// </summary>
    private float _totalTimeHealing;

    /// <summary>
    /// Cantidad de cura que vamos a recibir.
    /// </summary>
    private float _healingQuantity;

    /// <summary>
    /// Flag booleano para saber si estamos recuperando stamina.
    /// </summary>
    private bool _isRestoring;

    /// <summary>
    /// Timer para controlar el tiempo que estamos regenerando stamina.
    /// </summary>
    private float _timeRestoring;

    /// <summary>
    /// Valor que controla el tiempo total de regeneración de stamina.
    /// </summary>
    private float _totalTimeRestoring;

    /// <summary>
    /// Cantidad de stamina a regenerar.
    /// </summary>
    private float _restoringQuantity;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        //Actualizamos variable estática e inicializamos demás variables
        config.setPlayer(gameObject);
        _isHealing = false;
        _isRestoring = false;
        _timeHealing = 0f;
        _timeRestoring = 0f;
        _healingQuantity = 0f;
        _restoringQuantity = 0f;
    }

    /// <summary>
    /// Método para recalcular las barras de la UI general y aumentar su tamaño.
    /// </summary>
    /// <param name="levels">Nivel del atributo de la barra.</param>
    /// <param name="bar">Barra a modificar.</param>
    /// <param name="exp">Factor con el que cambia la barra.</param>
    public void recalculateBar(int levels, RectTransform bar, int exp)
    {
        Vector2 _actual = bar.sizeDelta;
        _actual.x = exp * levels;

        bar.sizeDelta = _actual;
    }

    /// <summary>
    /// Método para recibir daño.
    /// </summary>
    /// <param name="dmg">Daño entrante a recibir.</param>
    public void receiveDMG(float dmg)
    {
        //Calculamos el daño que realmente recibimos
        if ((_currentHP - dmg) < 0)
        {
            dmg = _currentHP;
        }
        _currentHP -= dmg;

        //Modificamos la vida y guardamos cambios
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().receiveDMG(dmg);
        saveSystem.saveStats();
    }

    /// <summary>
    /// Método para recibir una curación que dura un tiempo.
    /// </summary>
    /// <param name="heal">Cura a recibir.</param>
    /// <param name="time">Tiempo que dura esta cura</param>
    public void healHP(float heal, float time)
    {
        //Asignamos las variables correspondientes
        _healingQuantity = heal;
        _totalTimeHealing = time;
        _isHealing = true;
    }

    /// <summary>
    /// Método para utilizar stamina.
    /// </summary>
    /// <param name="use">Uso de stamina.</param>
    public void useStamina(float use)
    {
        //Calculamos el uso real
        if ((_currentStamina - use) < 0)
        {
            use = _currentStamina;
        }
        _currentStamina -= use;

        //Modificamos la stamina
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().useStamina(use);
    }

    /// <summary>
    /// Método para recibir una cura.
    /// </summary>
    /// <param name="heal">Cura a recibir.</param>
    public void healHP(float heal)
    {
        //Calculamos la regeneración extra
        float HPUpgrade = 0, staminaUpgrade = 0;
        config.getPlayer().GetComponent<combatController>().calculateRegenUpgrade(ref HPUpgrade, ref staminaUpgrade);

        //Curamos según la mejora
        heal += heal * HPUpgrade;
        if ((heal + _currentHP) > _maxHP)
        {
            heal = _maxHP - _currentHP;
        }
        _currentHP += heal;
        //Curamos y guardamos cambios
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().heal(heal);
        saveSystem.saveStats();
    }

    public void restoreStamina(float restore, float time)
    {
        _restoringQuantity = restore;
        _totalTimeRestoring = time;
        _isRestoring = true;

    }

    /// <summary>
    /// Método para restaurar stamina.
    /// </summary>
    /// <param name="restore">Restauración de stamina.</param>
    public void restoreStamina(float restore)
    {
        //Calculamos la regeneración extra
        float HPUpgrade = 0, staminaUpgrade = 0;
        config.getPlayer().GetComponent<combatController>().calculateRegenUpgrade(ref HPUpgrade, ref staminaUpgrade);
        
        //Restauramos la cantidad correspondiente
        restore += restore * staminaUpgrade * Time.deltaTime;
        if ((restore + _currentStamina) > _maxStamina)
        {
            restore = _maxStamina - _currentStamina;
        }
        _currentStamina += restore;

        //Restauramos y guardamos cambios
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().recoverStamina(restore);
        saveSystem.saveStats();
    }


    public void setMaxHP(float hp)
    {
        _maxHP = hp;
    }

    public void setMaxStamina(float stamina)
    {
        _maxStamina = stamina;
    }
    public void setCurrentHP(float hp)
    {
        _currentHP = hp;
    }

    public void setCurrentStamina(float stamina)
    {
        _currentStamina = stamina;
    }

    public int getHPExp()
    {
        return _HPBarExp;
    }
    public int getStaminaExp()
    {
        return _StaminaBarExp;
    }

    public float getMaxHP()
    {
        return _maxHP;
    }
    public float getMaxStamina()
    {
        return _maxStamina;
    }

    public float getCurrentHP()
    {
        return _currentHP;
    }
    public float getCurrentStamina()
    {
        return _currentStamina;
    }

    public long getCurrentSouls()
    {
        return _currentSouls;
    }

    public int getMaxHpExp()
    {
        return _maxHPExp;
    }
    public int getMaxStaminaExp()
    {
        return _maxStaminaExp;
    }
    public int recalculateHP(int levels)
    {
        return (statSystem.getVitality().getLevel() + levels) * _maxHPExp;

    }
    public int recalculateStamina(int levels)
    {
        return (statSystem.getEndurance().getLevel() + levels) * _maxStaminaExp;

    }

    private void Update()
    {
        if (_isHealing)
        {
            if (_timeHealing >= _totalTimeHealing)
            {
                _timeHealing = 0f;
                _isHealing = false;
            }
            else
            {
                healHP((_healingQuantity / _totalTimeHealing) * Time.deltaTime);
                _timeHealing += Time.deltaTime;
            }

        }

        if (_isRestoring)
        {
            if (_timeRestoring >= _totalTimeRestoring)
            {
                _timeRestoring = 0f;
                _isRestoring = false;
            }
            else
            {
                restoreStamina((_restoringQuantity / _totalTimeRestoring) * Time.deltaTime);
                _timeRestoring += Time.deltaTime;
            }

        }
    }
}
