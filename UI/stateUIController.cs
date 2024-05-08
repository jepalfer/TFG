using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// stateUIController es una clase que se usa para controlar la UI que muestra el estado del jugador.
/// </summary>
public class stateUIController : MonoBehaviour
{
    /// <summary>
    /// Referencia al campo de texto donde aparece el valor actual y máximo de HP.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _HPValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el valor actual y máximo de stamina.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _staminaValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el daño de arma primaria.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _primaryDamageValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el daño de arma secundaria.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _secundaryDamageValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de vitalidad.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _vitalityValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de resistencia.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _enduranceValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de fuerza.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _strengthValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de destreza.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _dexterityValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de agilidad.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _agilityValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el nivel de precisión.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _precisionValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el daño de penetración.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _armorPenValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el daño de sangrado.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _bleedDamageValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece la probabilidad de sangrado.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _bleedProbabilityValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece el daño crítico.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _critDamageValue;

    /// <summary>
    /// Referencia al campo de texto donde aparece la probabilidad crítica.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _critProbabilityValue;

    /// <summary>
    /// Método que se ejecuta al hacer visible la UI y la inicializa.
    /// </summary>
    public void initializeUI()
    {
        //Asignación de textos
        _HPValue.text = (int)config.getPlayer().GetComponent<statsController>().getCurrentHP() + "/" + config.getPlayer().GetComponent<statsController>().recalculateHP(0).ToString();
        _staminaValue.text = (int)config.getPlayer().GetComponent<statsController>().getCurrentStamina() + "/" + config.getPlayer().GetComponent<statsController>().recalculateStamina(0).ToString();
        _primaryDamageValue.text = weaponConfig.getPrimaryWeapon() == null ? "0" : weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();
        _secundaryDamageValue.text = weaponConfig.getSecundaryWeapon() == null ? "0" : weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();
        
        //Obtención de puntos de fuerza, destreza y precisión
        int strength = 0, dexterity = 0, precision = 0;
        
        config.getPlayer().GetComponent<combatController>().calculateAttributesLevelUp(ref strength, ref dexterity, ref precision);

        //Asignación de textos
        _vitalityValue.text = statSystem.getVitality().getLevel().ToString();
        _enduranceValue.text = statSystem.getEndurance().getLevel().ToString();
        
        //Asignación de niveles extra
        if (strength == 0)
        {
            _strengthValue.text = statSystem.getStrength().getLevel().ToString() + " (+0)";
        }
        else
        {
            string colorStrength = "<color=green> (+" + strength + ")</color>";
            _strengthValue.text = statSystem.getStrength().getLevel().ToString() + colorStrength;
        }

        if (dexterity == 0)
        {
            _dexterityValue.text = statSystem.getDexterity().getLevel().ToString() + " (+0)";
        }
        else
        {
            string colorDexterity = "<color=green> (+" + dexterity + ")</color>";
            _dexterityValue.text = statSystem.getDexterity().getLevel().ToString() + colorDexterity;
        }

        if (precision == 0)
        {
            _precisionValue.text = statSystem.getPrecision().getLevel().ToString() + " (+0)";
        }
        else
        {
            string colorPrecision = "<color=green> (+" + precision + ")</color>";
            _precisionValue.text = statSystem.getPrecision().getLevel().ToString() + colorPrecision;
        }
        _agilityValue.text = statSystem.getAgility().getLevel().ToString();

        //Obtención de daños penetrante, sangrado y crítico
        //Obtención de probabilidades
        float penetrating = 0, 
              bleedDMG = 0, 
              critDMG = 0, 
              bleedingProbability = config.getPlayer().GetComponent<combatController>().getBleedProbability() + 
                                    config.getPlayer().GetComponent<combatController>().getExtraBleedingProbability(), 
              critProbability = config.getPlayer().GetComponent<combatController>().getCritProbability() +
                                config.getPlayer().GetComponent<combatController>().getExtraCritProbability();
        config.getPlayer().GetComponent<combatController>().calculateExtraDamages(ref penetrating, ref bleedDMG, ref critDMG);
        config.getPlayer().GetComponent<combatController>().calculateSkillCritDamageProbability(ref critProbability);
        config.getPlayer().GetComponent<combatController>().calculateSkillBleedingProbability(ref bleedingProbability);

        //Asignación de textos
        _armorPenValue.text = penetrating.ToString() + "%";
        _bleedDamageValue.text = bleedDMG.ToString() + "%";
        _bleedProbabilityValue.text = bleedingProbability.ToString() + "%";
        _critDamageValue.text = critDMG.ToString() + "%";
        _critProbabilityValue.text = critProbability.ToString() + "%";
    }
}
