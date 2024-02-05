using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class levelUpUIController : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _levelValue;
    [SerializeField] private TextMeshProUGUI _soulsValue;
    [SerializeField] private TextMeshProUGUI _requiredSoulsValue;
    [SerializeField] private TextMeshProUGUI _vitalityLevel;
    [SerializeField] private TextMeshProUGUI _enduranceLevel;
    [SerializeField] private TextMeshProUGUI _strengthLevel;
    [SerializeField] private TextMeshProUGUI _dexterityLevel;
    [SerializeField] private TextMeshProUGUI _agilityLevel;
    [SerializeField] private TextMeshProUGUI _precisionLevel;
    [SerializeField] private TextMeshProUGUI _maxHP;
    [SerializeField] private TextMeshProUGUI _maxStamina;
    [SerializeField] private TextMeshProUGUI _primaryDMG;
    [SerializeField] private TextMeshProUGUI _secundaryDMG;

    private int _vitalityLevels = 0;
    private int _enduranceLevels = 0;
    private int _strengthLevels = 0;
    private int _dexterityLevels = 0;
    private int _agilityLevels = 0;
    private int _precisionLevels = 0;

    private int _levelF = 525;
    private float _difF = 1.1f;

    [SerializeField] private bool _hasUpgradedVitality = false;
    [SerializeField] private bool _hasUpgradedEndurance = false;
    [SerializeField] private bool _hasUpgradedStrength = false;
    [SerializeField] private bool _hasUpgradedDexterity = false;
    [SerializeField] private bool _hasUpgradedPrecision = false;
    private int _hp, _stamina;
    private long _souls = 0;

    [SerializeField] private GameObject _vitalityController;
    [SerializeField] private Button _acceptButton;

    public void initializeUI()
    {
        _levelValue.text = statSystem.getLevel().ToString();
        long requiredLevel = _levelF * statSystem.getLevel();
        _requiredSoulsValue.text = requiredLevel.ToString();

        _vitalityLevel.text = statSystem.getVitality().getLevel().ToString();
        _enduranceLevel.text = statSystem.getEndurance().getLevel().ToString();
        _strengthLevel.text = statSystem.getStrength().getLevel().ToString();
        _dexterityLevel.text = statSystem.getDexterity().getLevel().ToString();
        _agilityLevel.text = statSystem.getAgility().getLevel().ToString();
        _precisionLevel.text = statSystem.getPrecision().getLevel().ToString();
        saveSystem.saveAttributes();

        _maxHP.text = config.getPlayer().GetComponent<statsController>().getMaxHP().ToString();
        _maxStamina.text = config.getPlayer().GetComponent<statsController>().getMaxStamina().ToString();

        levelUpUIConfiguration.setLevelValue(_levelValue);
        levelUpUIConfiguration.setRequiredSoulsValue(_requiredSoulsValue);
        levelUpUIConfiguration.setVitalityValue(_vitalityLevel);
        levelUpUIConfiguration.setEnduranceValue(_enduranceLevel);
        levelUpUIConfiguration.setStrengthValue(_strengthLevel);
        levelUpUIConfiguration.setDexterityValue(_dexterityLevel);
        levelUpUIConfiguration.setAgilityValue(_agilityLevel);
        levelUpUIConfiguration.setPrecisionValue(_precisionLevel);
        levelUpUIConfiguration.setMaxHPValue(_maxHP);
        levelUpUIConfiguration.setMaxStaminaValue(_maxStamina);

        if (weaponConfig.getPrimaryWeapon() != null)
        {
            _primaryDMG.text = weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();
        }
        else
        {
            _primaryDMG.text = "0";
        }

        if (weaponConfig.getSecundaryWeapon() != null)
        {
            _secundaryDMG.text = weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();
        }
        else
        {
            _secundaryDMG.text = "0";
        }
        levelUpUIConfiguration.setPrimaryDMGValue(_primaryDMG);
        levelUpUIConfiguration.setSecundaryDMGValue(_secundaryDMG);

        saveSystem.saveStats();
        EventSystem.current.SetSelectedGameObject(_vitalityController);
    }

    public void setUIOff()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void addVitalityLvl()
    {
        attribute atribute = statSystem.getVitality();
        addLvl(ref _vitalityLevels, ref _vitalityLevel, ref atribute);

    }
    public void substractVitalityLvl()
    {
        attribute atribute = statSystem.getVitality();
        substractLvl(ref _vitalityLevels, ref _vitalityLevel, ref atribute);
    }
    public void addEnduranceLvl()
    {
        attribute atribute = statSystem.getEndurance();
        addLvl(ref _enduranceLevels, ref _enduranceLevel, ref atribute);
    }
    public void substractEnduranceLvl()
    {
        attribute atribute = statSystem.getEndurance();
        substractLvl(ref _enduranceLevels, ref _enduranceLevel, ref atribute);
    }
    public void addStrengthLvl()
    {
        attribute atribute = statSystem.getStrength();
        addLvl(ref _strengthLevels, ref _strengthLevel, ref atribute);
    }
    public void substractStrengthLvl()
    {
        attribute atribute = statSystem.getStrength();
        substractLvl(ref _strengthLevels, ref _strengthLevel, ref atribute);
    }
    public void addDexterityLvl()
    {
        attribute atribute = statSystem.getDexterity();
        addLvl(ref _dexterityLevels, ref _dexterityLevel, ref atribute);
    }
    public void substractDexterityLvl()
    {
        attribute atribute = statSystem.getDexterity();
        substractLvl(ref _dexterityLevels, ref _dexterityLevel, ref atribute);
    }
    public void addAgilityLvl()
    {
        attribute atribute = statSystem.getAgility();
        addLvl(ref _agilityLevels, ref _agilityLevel, ref atribute);
    }
    public void substractAgilityLvl()
    {
        attribute atribute = statSystem.getAgility();
        substractLvl(ref _agilityLevels, ref _agilityLevel, ref atribute);
    }
    public void addPrecisionLvl()
    {
        attribute atribute = statSystem.getPrecision();
        addLvl(ref _precisionLevels, ref _precisionLevel, ref atribute);
    }
    public void substractPrecisionLvl()
    {
        attribute atribute = statSystem.getPrecision();
        substractLvl(ref _precisionLevels, ref _precisionLevel, ref atribute);
    }
    public void addLvl(ref int statLevel, ref TextMeshProUGUI textField, ref attribute atribute)
    {
        long requiredTotal;
        if (long.TryParse(_requiredSoulsValue.text, out requiredTotal))
        {
        }
        else
        {
            // No se pudo convertir el texto a long para 'requiredSouls'
            Debug.LogError("Error al convertir requiredSouls a long");
        }
        long souls;
        if (long.TryParse(_soulsValue.text, out souls))
        {
        }
        else
        {
            // No se pudo convertir el texto a long para 'requiredSouls'
            Debug.LogError("Error al convertir requiredSouls a long");
        }
        int upgradedLevels = _vitalityLevels + _enduranceLevels + _strengthLevels + _dexterityLevels + _agilityLevels + _precisionLevels;

        requiredTotal = _levelF * (statSystem.getLevel() + upgradedLevels);

        if (souls >= requiredTotal)
        {
            if ((statLevel + atribute.getLevel()) < atribute.getMaxLevel())
            {
                souls -= requiredTotal;
                _souls += requiredTotal;
                _soulsValue.text = souls.ToString();
                requiredTotal = _levelF * (statSystem.getLevel() + upgradedLevels + 1);
                int level;
                if (int.TryParse(_levelValue.text, out level))
                {
                }
                else
                {
                    // No se pudo convertir el texto a entero para 'level'
                    Debug.LogError("Error al convertir level a entero");
                }
                statLevel++;
                textField.text = (atribute.getLevel() + statLevel).ToString();
                level++;
                _levelValue.text = level.ToString();

                if (textField.name == "VitalityLevel")
                {
                    _hasUpgradedVitality = true;
                    _hp = config.getPlayer().GetComponent<statsController>().recalculateHP(statLevel);
                    //_hp = StatsController.recalculateHP(statLevel);
                    _maxHP.text = _hp.ToString();
                }
                if (textField.name == "EnduranceLevel")
                {
                    _hasUpgradedEndurance = true;

                    _stamina = config.getPlayer().GetComponent<statsController>().recalculateStamina(statLevel);
                    //_stamina = StatsController.recalculateStamina(statLevel);
                    _maxStamina.text = _stamina.ToString();
                }

                if (textField.name == "StrengthLevel")
                {
                    _hasUpgradedStrength = true;
                    GameObject primary = weaponConfig.getPrimaryWeapon();
                    GameObject secundary = weaponConfig.getSecundaryWeapon();

                    if (primary != null)
                    {
                        weapon primaryWeapon = primary.GetComponent<weapon>();
                        _primaryDMG.text = (primaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + statLevel, statSystem.getDexterity().getLevel() + _dexterityLevels, statSystem.getPrecision().getLevel() + _precisionLevels)).ToString();
                    }

                    if (secundary != null)
                    {
                        weapon secundaryWeapon = secundary.GetComponent<weapon>();
                        _secundaryDMG.text = (secundaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + statLevel, statSystem.getDexterity().getLevel() + _dexterityLevels, statSystem.getPrecision().getLevel() + _precisionLevels)).ToString();
                    }

                    if (statLevel == 0)
                    {
                        _hasUpgradedStrength = false;
                    }
                }
                if (textField.name == "DexterityLevel")
                {
                    _hasUpgradedDexterity = true;
                    GameObject primary = weaponConfig.getPrimaryWeapon();
                    GameObject secundary = weaponConfig.getSecundaryWeapon();

                    if (primary != null)
                    {
                        weapon primaryWeapon = primary.GetComponent<weapon>();
                        _primaryDMG.text = (primaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + _strengthLevels, statSystem.getDexterity().getLevel() + statLevel, statSystem.getPrecision().getLevel() + _precisionLevels)).ToString();
                    }

                    if (secundary != null)
                    {
                        weapon secundaryWeapon = secundary.GetComponent<weapon>();
                        _secundaryDMG.text = (secundaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + _strengthLevels, statSystem.getDexterity().getLevel() + statLevel, statSystem.getPrecision().getLevel() + _precisionLevels)).ToString();
                    }
                    if (statLevel == 0)
                    {
                        _hasUpgradedDexterity = false;
                    }
                }
                if (textField.name == "PrecisionLevel")
                {
                    _hasUpgradedPrecision = true;
                    GameObject primary = weaponConfig.getPrimaryWeapon();
                    GameObject secundary = weaponConfig.getSecundaryWeapon();

                    if (primary != null)
                    {
                        weapon primaryWeapon = primary.GetComponent<weapon>();
                        _primaryDMG.text = (primaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + _strengthLevels, statSystem.getDexterity().getLevel() + _dexterityLevels, statSystem.getPrecision().getLevel() + statLevel)).ToString();
                    }

                    if (secundary != null)
                    {
                        weapon secundaryWeapon = secundary.GetComponent<weapon>();
                        _secundaryDMG.text = (secundaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + _strengthLevels, statSystem.getDexterity().getLevel() + _dexterityLevels, statSystem.getPrecision().getLevel() + statLevel)).ToString();
                    }
                    if (statLevel == 0)
                    {
                        _hasUpgradedPrecision = false;
                    }
                }

                _requiredSoulsValue.text = requiredTotal.ToString();
            }
        }

        
    }

    public void substractLvl(ref int statLevel, ref TextMeshProUGUI textField, ref attribute atribute)
    {
        if (statLevel > 0)
        {
            int level;
            if (int.TryParse(_levelValue.text, out level))
            {
            }
            else
            {
                // No se pudo convertir el texto a entero para 'souls'
                Debug.LogError("Error al convertir level a entero");
            }

            long requiredTotal;
            if (long.TryParse(_requiredSoulsValue.text, out requiredTotal))
            {
            }
            else
            {
                // No se pudo convertir el texto a long para 'requiredSouls'
                Debug.LogError("Error al convertir requiredSouls a long");
            }

            long souls;
            if (long.TryParse(_soulsValue.text, out souls))
            {
            }
            else
            {
                // No se pudo convertir el texto a long para 'requiredSouls'
                Debug.LogError("Error al convertir requiredSouls a long");
            }


            //long requiredLevel = _levelF * (atribute.getLevel() + statLevel) + _difF;
            statLevel--;
            int upgradedLevels = _vitalityLevels + _enduranceLevels + _strengthLevels + _dexterityLevels + _agilityLevels + _precisionLevels;

            requiredTotal = _levelF * (statSystem.getLevel() + upgradedLevels);
            souls += requiredTotal;
            _souls -= requiredTotal;
            //requiredTotal -= requiredLevel;

            _soulsValue.text = souls.ToString();
            _requiredSoulsValue.text = requiredTotal.ToString();

            if (textField.name == "VitalityLevel")
            {
                _hp = config.getPlayer().GetComponent<statsController>().recalculateHP(statLevel);
                // _hp = StatsController.recalculateHP(statLevel);
                _maxHP.text = _hp.ToString();
                if (statLevel == 0)
                {
                    _hasUpgradedVitality = false;
                }
            }
            if (textField.name == "EnduranceLevel")
            {
                _stamina = config.getPlayer().GetComponent<statsController>().recalculateStamina(statLevel);
                //_stamina = StatsController.recalculateStamina(statLevel);
                _maxStamina.text = _stamina.ToString();
                if (statLevel == 0)
                {
                    _hasUpgradedEndurance = false;
                }
            }


            if (textField.name == "StrengthLevel")
            {
                GameObject primary = weaponConfig.getPrimaryWeapon();
                GameObject secundary = weaponConfig.getSecundaryWeapon();

                if (primary != null)
                {
                    weapon primaryWeapon = primary.GetComponent<weapon>();
                    _primaryDMG.text = (primaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + statLevel, statSystem.getDexterity().getLevel() + _dexterityLevels, statSystem.getPrecision().getLevel() + _precisionLevels)).ToString();
                }

                if (secundary != null)
                {
                    weapon secundaryWeapon = secundary.GetComponent<weapon>();
                    _secundaryDMG.text = (secundaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + statLevel, statSystem.getDexterity().getLevel() + _dexterityLevels, statSystem.getPrecision().getLevel() + _precisionLevels)).ToString();
                }

                if (statLevel == 0)
                {
                    _hasUpgradedStrength = false;
                }
            }
            if (textField.name == "DexterityLevel")
            {
                GameObject primary = weaponConfig.getPrimaryWeapon();
                GameObject secundary = weaponConfig.getSecundaryWeapon();

                if (primary != null)
                {
                    weapon primaryWeapon = primary.GetComponent<weapon>();
                    _primaryDMG.text = (primaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + _strengthLevels, statSystem.getDexterity().getLevel() + statLevel, statSystem.getPrecision().getLevel() + _precisionLevels)).ToString();
                }

                if (secundary != null)
                {
                    weapon secundaryWeapon = secundary.GetComponent<weapon>();
                    _secundaryDMG.text = (secundaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + _strengthLevels, statSystem.getDexterity().getLevel() + statLevel, statSystem.getPrecision().getLevel() + _precisionLevels)).ToString();
                }
                if (statLevel == 0)
                {
                    _hasUpgradedDexterity = false;
                }
            }
            if (textField.name == "PrecisionLevel")
            {
                GameObject primary = weaponConfig.getPrimaryWeapon();
                GameObject secundary = weaponConfig.getSecundaryWeapon();

                if (primary != null)
                {
                    weapon primaryWeapon = primary.GetComponent<weapon>();
                    _primaryDMG.text = (primaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + _strengthLevels, statSystem.getDexterity().getLevel() + _dexterityLevels, statSystem.getPrecision().getLevel() + statLevel)).ToString();
                }

                if (secundary != null)
                {
                    weapon secundaryWeapon = secundary.GetComponent<weapon>();
                    _secundaryDMG.text = (secundaryWeapon.calculateDMG(statSystem.getStrength().getLevel() + _strengthLevels, statSystem.getDexterity().getLevel() + _dexterityLevels, statSystem.getPrecision().getLevel() + statLevel)).ToString();
                }
                if (statLevel == 0)
                {
                    _hasUpgradedPrecision = false;
                }
            }

            textField.text = (atribute.getLevel() + statLevel).ToString();
            level--;
            _levelValue.text = level.ToString();
        }
    }

    public Button getLevelUpButton()
    {
        return _acceptButton;
    }
    public void levelUp()
    {
        long requiredSouls, souls;
        int level; 
        
        if (int.TryParse(_levelValue.text, out level))
        {
        }
        else
        {
            // No se pudo convertir el texto a int para 'level'
            Debug.LogError("Error al convertir level a int");
        }

        if (long.TryParse(_requiredSoulsValue.text, out requiredSouls))
        {
        }
        else
        {
            // No se pudo convertir el texto a long para 'requiredSouls'
            Debug.LogError("Error al convertir requiredSouls a long");
        }

        if (long.TryParse(_soulsValue.text, out souls))
        {
        }
        else
        {
            // No se pudo convertir el texto a long para 'souls'
            Debug.LogError("Error al convertir souls a long");
        }

        if (_vitalityLevels > 0 || _enduranceLevels > 0 || _strengthLevels > 0 || _dexterityLevels > 0 || _agilityLevels > 0 || _precisionLevels > 0)
        {
            //Si hemos seleccionado subir algun stat y tenemos las almas suficientes
            statSystem.setLevel(level);
            statSystem.getVitality().setLevel(statSystem.getVitality().getLevel() + _vitalityLevels);
            statSystem.getEndurance().setLevel(statSystem.getEndurance().getLevel() + _enduranceLevels);
            statSystem.getStrength().setLevel(statSystem.getStrength().getLevel() + _strengthLevels);
            statSystem.getDexterity().setLevel(statSystem.getDexterity().getLevel() + _dexterityLevels);
            statSystem.getAgility().setLevel(statSystem.getAgility().getLevel() + _agilityLevels);
            statSystem.getPrecision().setLevel(statSystem.getPrecision().getLevel() + _precisionLevels);

            levelUpUIConfiguration.getVitalityValue().text = statSystem.getVitality().getLevel().ToString();
            levelUpUIConfiguration.getEnduranceValue().text = statSystem.getEndurance().getLevel().ToString();
            levelUpUIConfiguration.getStrengthValue().text = statSystem.getStrength().getLevel().ToString();
            levelUpUIConfiguration.getDexterityValue().text = statSystem.getDexterity().getLevel().ToString();
            levelUpUIConfiguration.getAgilityValue().text = statSystem.getAgility().getLevel().ToString();
            levelUpUIConfiguration.getPrecisionValue().text = statSystem.getPrecision().getLevel().ToString();

            config.getPlayer().GetComponent<combatController>().useSouls(_souls);
            _souls = 0;



            _vitalityLevels = 0;
            _enduranceLevels = 0;
            _strengthLevels = 0;
            _dexterityLevels = 0;
            _agilityLevels = 0;
            _precisionLevels = 0;

            if (weaponConfig.getPrimaryWeapon() != null)
            {
                weapon weapon = weaponConfig.getPrimaryWeapon().GetComponent<weapon>();
                weapon.setBaseDMG(weapon.calculateBaseDMG());
                weapon.setTotalDMG(weapon.calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel()));
                _primaryDMG.text = weapon.getTotalDMG().ToString();
                levelUpUIConfiguration.setPrimaryDMGValue(_primaryDMG);
            }

            if (weaponConfig.getSecundaryWeapon() != null)
            {
                weapon weapon = weaponConfig.getSecundaryWeapon().GetComponent<weapon>();
                weapon.setBaseDMG(weapon.calculateBaseDMG());
                weapon.setTotalDMG(weapon.calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel()));
                _secundaryDMG.text = weapon.getTotalDMG().ToString();
                levelUpUIConfiguration.setSecundaryDMGValue(_secundaryDMG);
            }


            /*            GameObject primary = GetComponent<CombatController>().getPrimaryWeapon();
                        GameObject secundary = GetComponent<CombatController>().getSecundaryWeapon();
                        if (primary != null)
                        {
                            primary.GetComponent<Weapon>().setBaseDMG(primary.GetComponent<Weapon>().calculateBaseDMG());
                            primary.GetComponent<Weapon>().setTotalDMG(primary.GetComponent<Weapon>().calculateDMG(statSystem.getStrength()._level, statSystem.getDexterity()._level, statSystem.getPrecision()._level));
                        }

                        if (secundary != null)
                        {
                            secundary.GetComponent<Weapon>().setBaseDMG(secundary.GetComponent<Weapon>().calculateBaseDMG());
                            secundary.GetComponent<Weapon>().setTotalDMG(secundary.GetComponent<Weapon>().calculateDMG(statSystem.getStrength()._level, statSystem.getDexterity()._level, statSystem.getPrecision()._level));
                        }*/
            saveSystem.saveAttributes();
            //Calculamos vida y stamina maximas

            if (_hasUpgradedVitality || _hasUpgradedEndurance)
            {
                if (_hasUpgradedVitality)
                {
                    config.getPlayer().GetComponent<statsController>().setMaxHP(_hp);
                    config.getPlayer().GetComponent<statsController>().setCurrentHP(_hp);
                    //StatsController.setMaxHP(_hp);
                    //StatsController.setCurrentHP(_hp);
                    UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().setHPBarValue(1);
                    config.getPlayer().GetComponent<statsController>().recalculateBar(statSystem.getVitality().getLevel(), UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().getHPBar().GetComponent<RectTransform>(), config.getPlayer().GetComponent<statsController>().getHPExp());
                    //StatsController.recalculateBar(_vitalityLevels, GetComponent<UIManager>().getHPBar().GetComponent<RectTransform>(), StatsController.getHPExp());
                    _hasUpgradedVitality = false;
                }

                if (_hasUpgradedEndurance)
                {
                    config.getPlayer().GetComponent<statsController>().setMaxStamina(_stamina);
                    config.getPlayer().GetComponent<statsController>().setCurrentStamina(_stamina);
                    //StatsController.setMaxStamina(_stamina);
                    //StatsController.setCurrentStamina(_stamina);
                    UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().setStaminaBarValue(1);
                    config.getPlayer().GetComponent<statsController>().recalculateBar(statSystem.getEndurance().getLevel(), UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().getStaminaBar().GetComponent<RectTransform>(), config.getPlayer().GetComponent<statsController>().getStaminaExp());

                    //StatsController.recalculateBar(_enduranceLevels, GetComponent<UIManager>().getStaminaBar().GetComponent<RectTransform>(), StatsController.getStaminaExp());
                    _hasUpgradedEndurance = false;
                }
                saveSystem.saveStats();
            }

        }

    }

    public TextMeshProUGUI getLevelValue()
    {
        return _levelValue;
    }
    public TextMeshProUGUI getSoulsValue()
    {
        return _soulsValue;
    }
    public TextMeshProUGUI getRequiredSoulsValue()
    {
        return _requiredSoulsValue;
    }
    public TextMeshProUGUI getVitalityLevel()
    {
        return _vitalityLevel;
    }
    public TextMeshProUGUI getEnduranceLevel()
    {
        return _enduranceLevel;
    }
    public TextMeshProUGUI getStrengthLevel()
    {
        return _strengthLevel;
    }
    public TextMeshProUGUI getDexterityLevel()
    {
        return _dexterityLevel;
    }
    public TextMeshProUGUI getAgilityLevel()
    {
        return _agilityLevel;
    }
    public TextMeshProUGUI getPrecisionLevel()
    {
        return _precisionLevel;
    }
    public TextMeshProUGUI getMaxHP()
    {
        return _maxHP;
    }
    public TextMeshProUGUI getMaxStamina()
    {
        return _maxStamina;
    }
    public TextMeshProUGUI getPrimaryDMG()
    {
        return _primaryDMG;
    }
    public TextMeshProUGUI getSecundaryDMG()
    {
        return _secundaryDMG;
    }

    public void setLevelValue(string value)
    {
        _levelValue.text = value;
    }
    public void setSoulsValue(string value)
    {
        _soulsValue.text = value;
    }
    public void setRequiredSoulsValue(string value)
    {
        _requiredSoulsValue.text = value;
    }
    public void setVitalityLevel(string value)
    {
        _vitalityLevel.text = value;
    }
    public void setEnduranceLevel(string value)
    {
        _enduranceLevel.text = value;
    }
    public void setStrengthLevel(string value)
    {
        _strengthLevel.text = value;
    }
    public void setDexterityLevel(string value)
    {
        _dexterityLevel.text = value;
    }
    public void setAgilityLevel(string value)
    {
        _agilityLevel.text = value;
    }
    public void setPrecisionLevel(string value)
    {
        _precisionLevel.text = value;
    }
    public void setMaxHP(string value)
    {
        _maxHP.text = value;
    }
    public void setMaxStamina(string value)
    {
        _maxStamina.text = value;
    }
    public void setPrimaryDMG(string value)
    {
        _primaryDMG.text = value;
    }
    public void setSecundaryDMG(string value)
    {
        _secundaryDMG.text = value;
    }

    public static void updateUI(ref TextMeshProUGUI field, string value)
    {
        field.text = value;
    }
}
