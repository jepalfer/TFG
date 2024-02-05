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

    private int _HPBarExp = 20;
    private int _StaminaBarExp = 5;

    private int _maxHPExp = 200;
    private int _maxStaminaExp = 20;

    private void Awake()
    {
        config.setPlayer(gameObject);
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
        if ((heal + _currentHP) > _maxHP)
        {
            heal = _maxHP - _currentHP;
        }
        _currentHP += heal;
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().heal(heal);
        saveSystem.saveStats();
    }

    public void restoreStamina(float stamina)
    {
        if ((stamina + _currentStamina) > _maxStamina)
        {
            stamina = _maxStamina - _currentStamina;
        }
        _currentStamina += stamina;
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().recoverStamina(stamina);
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
}
