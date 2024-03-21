using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class statsController : MonoBehaviour
{
    private float _maxHP;
    private float _currentHP;

    private float _maxStamina;
    private float _currentStamina;

    private long _currentSouls;

    private int _HPBarExp = 30;
    private int _StaminaBarExp = 30;

    private int _maxHPExp = 200;
    private int _maxStaminaExp = 20;

    private bool _isHealing;
    private float _timeHealing;
    private float _totalTimeHealing;
    private float _healingQuantity;

    private bool _isRestoring;
    private float _timeRestoring;
    private float _totalTimeRestoring;
    private float _restoringQuantity;
    private void Awake()
    {
        config.setPlayer(gameObject);
        _isHealing = false;
        _isRestoring = false;
        _timeHealing = 0f;
        _timeRestoring = 0f;
        _healingQuantity = 0f;
        _restoringQuantity = 0f;
    }

    public void recalculateBar(int levels, RectTransform bar, int exp)
    {
        Vector2 _actual = bar.sizeDelta;
        _actual.x = exp * levels;

        bar.sizeDelta = _actual;
    }


    public void receiveDMG(float dmg)
    {
        if ((_currentHP - dmg) < 0)
        {
            dmg = _currentHP;
        }
        _currentHP -= dmg;
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().receiveDMG(dmg);
        saveSystem.saveStats();
    }

    public void healHP(float heal, float time)
    {
        _healingQuantity = heal;
        _totalTimeHealing = time;
        _isHealing = true;
    }

    public void useStamina(float use)
    {
        if ((_currentStamina - use) < 0)
        {
            use = _currentStamina;
        }
        _currentStamina -= use;
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().useStamina(use);
    }

    public void healHP(float heal)
    {
        //Calculamos la regeneración extra
        float HPUpgrade = 0, staminaUpgrade = 0;
        config.getPlayer().GetComponent<combatController>().calculateRegenUpgrade(ref HPUpgrade, ref staminaUpgrade);

        heal += heal * HPUpgrade;
        if ((heal + _currentHP) > _maxHP)
        {
            heal = _maxHP - _currentHP;
        }
        _currentHP += heal;
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().heal(heal);
        saveSystem.saveStats();
    }

    public void restoreStamina(float restore, float time)
    {
        _restoringQuantity = restore;
        _totalTimeRestoring = time;
        _isRestoring = true;

    }
    public void restoreStamina(float restore)
    {
        //Calculamos la regeneración extra
        float HPUpgrade = 0, staminaUpgrade = 0;
        config.getPlayer().GetComponent<combatController>().calculateRegenUpgrade(ref HPUpgrade, ref staminaUpgrade);
        restore += restore * staminaUpgrade * Time.deltaTime;
        if ((restore + _currentStamina) > _maxStamina)
        {
            restore = _maxStamina - _currentStamina;
        }
        _currentStamina += restore;
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().recoverStamina(restore);
        saveSystem.saveStats();
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
