using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// levelUpUIController es una clase que se usa para controlar la UI de subida de nivel.
/// </summary>
public class levelUpUIController : MonoBehaviour
{
    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel del jugador.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _levelValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece la cantidad de almas del jugador.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _soulsValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece la cantidad de almas requeridas para el próximo nivel.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _requiredSoulsValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de vitalidad.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _vitalityLevel;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de resistencia.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _enduranceLevel;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de fuerza.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _strengthLevel;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de destreza.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _dexterityLevel;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de agilidad.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _agilityLevel;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de precisión.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _precisionLevel;

    /// <summary>
    /// Referencia al campo de texto donde aparece la vida máxima.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _maxHP;

    /// <summary>
    /// Referencia al campo de texto donde aparece la stamina máxima.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _maxStamina;

    /// <summary>
    /// Referencia al campo de texto donde aparece el daño de arma primaria.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _primaryDMG;

    /// <summary>
    /// Referencia al campo de texto donde aparece el daño de arma secundaria.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _secundaryDMG;

    /// <summary>
    /// Referencia al texto del botón de aceptar.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _buttonText;

    /// <summary>
    /// Entero que almacena los niveles que se ha ido subiendo de vitalidad.
    /// </summary>
    private int _vitalityLevels = 0;

    /// <summary>
    /// Entero que almacena los niveles que se ha ido subiendo de resistencia.
    /// </summary>
    private int _enduranceLevels = 0;

    /// <summary>
    /// Entero que almacena los niveles que se ha ido subiendo de fuerza.
    /// </summary>
    private int _strengthLevels = 0;

    /// <summary>
    /// Entero que almacena los niveles que se ha ido subiendo de destreza.
    /// </summary>
    private int _dexterityLevels = 0;

    /// <summary>
    /// Entero que almacena los niveles que se ha ido subiendo de agilidad.
    /// </summary>
    private int _agilityLevels = 0;

    /// <summary>
    /// Entero que almacena los niveles que se ha ido subiendo de precisión.
    /// </summary>
    private int _precisionLevels = 0;

    /// <summary>
    /// Factor de crecimiento de almas por nivel.
    /// </summary>
    private int _levelF = 525;

    /// <summary>
    /// Flag booleano que indica si hemos mejorado vitalidad o no.
    /// </summary>
    [SerializeField] private bool _hasUpgradedVitality = false;

    /// <summary>
    /// Flag booleano que indica si hemos mejorado resistencia o no.
    /// </summary>
    [SerializeField] private bool _hasUpgradedEndurance = false;

    /// <summary>
    /// hp de los que dispone el jugador.
    /// </summary>
    private int _hp;

    /// <summary>
    /// stamina de la que dispone el jugador.
    /// </summary>
    private int _stamina;

    /// <summary>
    /// Cantidad de almas de las que dispone el jugador.
    /// </summary>
    private long _souls = 0;

    /// <summary>
    /// Referencia al controlador de vitalidad.
    /// </summary>
    [SerializeField] private GameObject _vitalityController;

    /// <summary>
    /// Referencia al botón de aceptar.
    /// </summary>
    [SerializeField] private Button _acceptButton;

    /// <summary>
    /// Método que se ejecuta al mostrar la UI y la inicializa.
    /// </summary>
    public void initializeUI()
    {
        //Inicialización de textos
        _levelValue.text = statSystem.getLevel().ToString();
        long requiredLevel = _levelF * statSystem.getLevel();
        _requiredSoulsValue.text = requiredLevel.ToString();

        _vitalityLevel.text = statSystem.getVitality().getLevel().ToString();
        _enduranceLevel.text = statSystem.getEndurance().getLevel().ToString();
        _strengthLevel.text = statSystem.getStrength().getLevel().ToString();
        _dexterityLevel.text = statSystem.getDexterity().getLevel().ToString();
        _agilityLevel.text = statSystem.getAgility().getLevel().ToString();
        _precisionLevel.text = statSystem.getPrecision().getLevel().ToString();
        _buttonText.color = Color.white;

        //Guardamos atributos
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

    /// <summary>
    /// Método que se ejecuta al ocultar la UI.
    /// </summary>
    public void setUIOff()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }


    /// <summary>
    /// Método que se ejecuta al añadir un nivel de vitalidad.
    /// </summary>
    public void addVitalityLvl()
    {
        attribute atribute = statSystem.getVitality();
        addLvl(ref _vitalityLevels, ref _vitalityLevel, ref atribute);

    }

    /// <summary>
    /// Método que se ejecuta al restar un nivel de vitalidad.
    /// </summary>
    public void substractVitalityLvl()
    {
        attribute atribute = statSystem.getVitality();
        substractLvl(ref _vitalityLevels, ref _vitalityLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al añadir un nivel de resistencia.
    /// </summary>
    public void addEnduranceLvl()
    {
        attribute atribute = statSystem.getEndurance();
        addLvl(ref _enduranceLevels, ref _enduranceLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al restar un nivel de resistencia.
    /// </summary>
    public void substractEnduranceLvl()
    {
        attribute atribute = statSystem.getEndurance();
        substractLvl(ref _enduranceLevels, ref _enduranceLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al añadir un nivel de fuerza.
    /// </summary>
    public void addStrengthLvl()
    {
        attribute atribute = statSystem.getStrength();
        addLvl(ref _strengthLevels, ref _strengthLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al restar un nivel de fuerza.
    /// </summary>
    public void substractStrengthLvl()
    {
        attribute atribute = statSystem.getStrength();
        substractLvl(ref _strengthLevels, ref _strengthLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al añadir un nivel de destreza.
    /// </summary>
    public void addDexterityLvl()
    {
        attribute atribute = statSystem.getDexterity();
        addLvl(ref _dexterityLevels, ref _dexterityLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al restar un nivel de destreza.
    /// </summary>
    public void substractDexterityLvl()
    {
        attribute atribute = statSystem.getDexterity();
        substractLvl(ref _dexterityLevels, ref _dexterityLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al añadir un nivel de agilidad.
    /// </summary>
    public void addAgilityLvl()
    {
        attribute atribute = statSystem.getAgility();
        addLvl(ref _agilityLevels, ref _agilityLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al restar un nivel de agilidad.
    /// </summary>
    public void substractAgilityLvl()
    {
        attribute atribute = statSystem.getAgility();
        substractLvl(ref _agilityLevels, ref _agilityLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al añadir un nivel de precisión.
    /// </summary>
    public void addPrecisionLvl()
    {
        attribute atribute = statSystem.getPrecision();
        addLvl(ref _precisionLevels, ref _precisionLevel, ref atribute);
    }

    /// <summary>
    /// Método que se ejecuta al restar un nivel de precisión.
    /// </summary>
    public void substractPrecisionLvl()
    {
        attribute atribute = statSystem.getPrecision();
        substractLvl(ref _precisionLevels, ref _precisionLevel, ref atribute);
    }
    /// <summary>
    /// Método general que se ejecuta al añadir un nivel de cualquier atributo.
    /// </summary>
    /// <param name="statLevel">Nivel del atributo que se está aumentando.</param>
    /// <param name="textField">Texto del atributo que se está aumentando.</param>
    /// <param name="attribute">Atributo al que se está aumentando el nivel.</param>
    public void addLvl(ref int statLevel, ref TextMeshProUGUI textField, ref attribute attribute)
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
            if ((statLevel + attribute.getLevel()) < attribute.getMaxLevel())
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
                textField.text = (attribute.getLevel() + statLevel).ToString();
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
                    //_hasUpgradedStrength = true;
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

                    //if (statLevel == 0)
                    //{
                    //    _hasUpgradedStrength = false;
                    //}
                }
                if (textField.name == "DexterityLevel")
                {
                    //_hasUpgradedDexterity = true;
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
                    //if (statLevel == 0)
                    //{
                    //    _hasUpgradedDexterity = false;
                    //}
                }
                if (textField.name == "PrecisionLevel")
                {
                    //_hasUpgradedPrecision = true;
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
                    //if (statLevel == 0)
                    //{
                    //    _hasUpgradedPrecision = false;
                    //}
                }

                _requiredSoulsValue.text = requiredTotal.ToString();
            }
        }

        
    }

    /// <summary>
    /// Método general que se ejecuta al restar un nivel de cualquier atributo.
    /// </summary>
    /// <param name="statLevel">Nivel del atributo que se está aumentando.</param>
    /// <param name="textField">Texto del atributo que se está aumentando.</param>
    /// <param name="attribute">Atributo al que se está aumentando el nivel.</param>
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

                //if (statLevel == 0)
                //{
                //    _hasUpgradedStrength = false;
                //}
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
                //if (statLevel == 0)
                //{
                //    _hasUpgradedDexterity = false;
                //}
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
                //if (statLevel == 0)
                //{
                //    _hasUpgradedPrecision = false;
                //}
            }

            textField.text = (atribute.getLevel() + statLevel).ToString();
            level--;
            _levelValue.text = level.ToString();
        }
    }

    /// <summary>
    /// Getter que devuelve <see cref="_acceptButton"/>.
    /// </summary>
    /// <returns><see cref="_acceptButton"/>.</returns>
    public Button getLevelUpButton()
    {
        return _acceptButton;
    }

    /// <summary>
    /// Método que se ejecuta al hacer efectiva la subidad de nivel.
    /// </summary>
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
            _levelValue.color = Color.white;
            _vitalityLevel.color = Color.white;
            _enduranceLevel.color = Color.white;
            _strengthLevel.color = Color.white;
            _dexterityLevel.color = Color.white;
            _precisionLevel.color = Color.white;
            _agilityLevel.color = Color.white;
            _primaryDMG.color = Color.white;
            _secundaryDMG.color = Color.white;
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

    /// <summary>
    /// Método auxiliar para comprobar que todos los niveles estén a 0 para cambiar ciertos aspectos de la UI.
    /// </summary>
    /// <returns>Booleano que indica si todos los niveles están a 0 (true) o no (false).</returns>
    public bool allLevelsEqual0()
    {
        return _vitalityLevels == 0 && _enduranceLevels == 0 && _strengthLevels == 0 && _dexterityLevels == 0 && _precisionLevels == 0 && _agilityLevels == 0;
    }

    /// <summary>
    /// Método auxiliar para comprobar que todos los niveles de atributos relacionados con el daño estén a 0.
    /// </summary>
    /// <returns>Booleano que indica si todos los niveles están a 0 (true) o no (false).</returns>
    public bool allDamageStatsEqual0()
    {
        return _strengthLevels == 0 && _dexterityLevels == 0 && _precisionLevels == 0;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_vitalityLevels"/>.
    /// </summary>
    /// <returns><see cref="_vitalityLevels"/>.</returns>
    public int getVitalityLevels()
    {
        return _vitalityLevels;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enduranceLevels"/>.
    /// </summary>
    /// <returns><see cref="_enduranceLevels"/>.</returns>
    public int getEnduranceLevels()
    {
        return _enduranceLevels;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_strengthLevels"/>.
    /// </summary>
    /// <returns><see cref="_strengthLevels"/>.</returns>
    public int getStrengthLevels()
    {
        return _strengthLevels;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_dexterityLevels"/>.
    /// </summary>
    /// <returns><see cref="_dexterityLevels"/>.</returns>
    public int getDexterityLevels()
    {
        return _dexterityLevels;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_precisionLevels"/>.
    /// </summary>
    /// <returns><see cref="_precisionLevels"/>.</returns>
    public int getPrecisionLevels()
    {
        return _precisionLevels;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_agilityLevels"/>.
    /// </summary>
    /// <returns><see cref="_agilityLevels"/>.</returns>
    public int getAgilityLevels()
    {
        return _agilityLevels;
    }


    /// <summary>
    /// Getter que devuelve <see cref="_levelValue"/>.
    /// </summary>
    /// <returns><see cref="_levelValue"/>.</returns>
    public TextMeshProUGUI getLevelValue()
    {
        return _levelValue;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_soulsValue"/>.
    /// </summary>
    /// <returns><see cref="_soulsValue"/>.</returns>
    public TextMeshProUGUI getSoulsValue()
    {
        return _soulsValue;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_requiredSoulsValue"/>.
    /// </summary>
    /// <returns><see cref="_requiredSoulsValue"/>.</returns>
    public TextMeshProUGUI getRequiredSoulsValue()
    {
        return _requiredSoulsValue;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_vitalityLevel"/>.
    /// </summary>
    /// <returns><see cref="_vitalityLevel"/>.</returns>
    public TextMeshProUGUI getVitalityLevel()
    {
        return _vitalityLevel;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enduranceLevel"/>.
    /// </summary>
    /// <returns><see cref="_enduranceLevel"/>.</returns>
    public TextMeshProUGUI getEnduranceLevel()
    {
        return _enduranceLevel;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_strengthLevel"/>.
    /// </summary>
    /// <returns><see cref="_strengthLevel"/>.</returns>
    public TextMeshProUGUI getStrengthLevel()
    {
        return _strengthLevel;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_dexterityLevel"/>.
    /// </summary>
    /// <returns><see cref="_dexterityLevel"/>.</returns>
    public TextMeshProUGUI getDexterityLevel()
    {
        return _dexterityLevel;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_agilityLevel"/>.
    /// </summary>
    /// <returns><see cref="_agilityLevel"/>.</returns>
    public TextMeshProUGUI getAgilityLevel()
    {
        return _agilityLevel;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_precisionLevel"/>.
    /// </summary>
    /// <returns><see cref="_precisionLevel"/>.</returns>
    public TextMeshProUGUI getPrecisionLevel()
    {
        return _precisionLevel;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_maxHP"/>.
    /// </summary>
    /// <returns><see cref="_maxHP"/>.</returns>
    public TextMeshProUGUI getMaxHP()
    {
        return _maxHP;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_maxStamina"/>.
    /// </summary>
    /// <returns><see cref="_maxStamina"/>.</returns>
    public TextMeshProUGUI getMaxStamina()
    {
        return _maxStamina;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_primaryDMG"/>.
    /// </summary>
    /// <returns><see cref="_primaryDMG"/>.</returns>
    public TextMeshProUGUI getPrimaryDMG()
    {
        return _primaryDMG;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_secundaryDMG"/>.
    /// </summary>
    /// <returns><see cref="_secundaryDMG"/>.</returns>
    public TextMeshProUGUI getSecundaryDMG()
    {
        return _secundaryDMG;
    }

    /// <summary>
    /// Setter que modifica el texto de <see cref="_levelValue"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setLevelValue(string value)
    {
        _levelValue.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_soulsValue"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setSoulsValue(string value)
    {
        _soulsValue.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_requiredSoulsValue"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setRequiredSoulsValue(string value)
    {
        _requiredSoulsValue.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_vitalityLevel"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setVitalityLevel(string value)
    {
        _vitalityLevel.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_enduranceLevel"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setEnduranceLevel(string value)
    {
        _enduranceLevel.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_strengthLevel"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setStrengthLevel(string value)
    {
        _strengthLevel.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_dexterityLevel"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setDexterityLevel(string value)
    {
        _dexterityLevel.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_agilityLevel"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setAgilityLevel(string value)
    {
        _agilityLevel.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_precisionLevel"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setPrecisionLevel(string value)
    {
        _precisionLevel.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_maxHP"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setMaxHP(string value)
    {
        _maxHP.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_maxStamina"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setMaxStamina(string value)
    {
        _maxStamina.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_primaryDMG"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setPrimaryDMG(string value)
    {
        _primaryDMG.text = value;
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_secundaryDMG"/>.
    /// </summary>
    /// <param name="value">Texto a asignar.</param>
    public void setSecundaryDMG(string value)
    {
        _secundaryDMG.text = value;
    }

    /// <summary>
    /// Método que actualiza un campo concreto de la UI.
    /// </summary>
    /// <param name="field">Campo a actualizar.</param>
    /// <param name="value">Valor a asignar.</param>
    public static void updateUI(ref TextMeshProUGUI field, string value)
    {
        field.text = value;
    }
}
