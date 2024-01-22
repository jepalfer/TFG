using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class profile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _userName;
    [SerializeField] private TextMeshProUGUI _nivel;
    [SerializeField] private Button _loadBtn;
    [SerializeField] private Button _removeBtn;
    private int _vitalityLevel;
    private int _enduranceLevel;
    private int _strengthLevel;
    private int _dexterityLevel;
    private int _agilityLevel;
    private int _precisionLevel;
    public void setLevel(int level)
    {
        _nivel.text = level.ToString();
    }

    public void setUserName(string name)
    {
        _userName.text = name;
    }
    
    public void setVitality(int level)
    {
        _vitalityLevel = level;
    }
    public void setEndurance(int level)
    {
        _enduranceLevel = level;
    }
    public void setStrength(int level)
    {
        _strengthLevel = level;
    }
    public void setDexterity(int level)
    {
        _dexterityLevel = level;
    }
    public void setAgility(int level)
    {
        _agilityLevel = level;
    }
    public void setPrecision(int level)
    {
        _precisionLevel = level;
    }
    
    public string getName()
    {
        return _userName.text;
    }

    public Button getLoadBtn()
    {
        return _loadBtn;
    }

    public Button getRemoveBtn()
    {
        return _removeBtn;
    }

    public int getLevel()
    {
        int level;

        if (int.TryParse(_nivel.text, out level))
        {

        }
        else
        {
            Debug.LogError("Fallo al convertir level a int");
        }

        return level;
    }
    
    public int getVitality()
    {
        return _vitalityLevel;
    }
    public int getEndurance()
    {
        return _enduranceLevel;
    }
    public int getStrength()
    {
        return _strengthLevel;
    }
    public int getDexterity()
    {
        return _dexterityLevel;
    }
    public int getAgility()
    {
        return _agilityLevel;
    }
    public int getPrecision()
    {
        return _precisionLevel;
    }
}
