using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stateUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _HPValue;
    [SerializeField] private TextMeshProUGUI _staminaValue;
    [SerializeField] private TextMeshProUGUI _primaryDamageValue;
    [SerializeField] private TextMeshProUGUI _secundaryDamageValue;
    [SerializeField] private TextMeshProUGUI _vitalityValue;
    [SerializeField] private TextMeshProUGUI _enduranceValue;
    [SerializeField] private TextMeshProUGUI _strengthValue;
    [SerializeField] private TextMeshProUGUI _dexterityValue;
    [SerializeField] private TextMeshProUGUI _agilityValue;
    [SerializeField] private TextMeshProUGUI _precisionValue;
    [SerializeField] private TextMeshProUGUI _armorPenValue;
    [SerializeField] private TextMeshProUGUI _bleedDamageValue;
    [SerializeField] private TextMeshProUGUI _bleedProbabilityValue;
    [SerializeField] private TextMeshProUGUI _critDamageValue;
    [SerializeField] private TextMeshProUGUI _critProbabilityValue;

    public void initializeUI()
    {
        _HPValue.text = (int)config.getPlayer().GetComponent<statsController>().getCurrentHP() + "/" + config.getPlayer().GetComponent<statsController>().recalculateHP(0).ToString();
        _staminaValue.text = (int)config.getPlayer().GetComponent<statsController>().getCurrentStamina() + "/" + config.getPlayer().GetComponent<statsController>().recalculateStamina(0).ToString();
        _primaryDamageValue.text = weaponConfig.getPrimaryWeapon() == null ? "0" : weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();
        _secundaryDamageValue.text = weaponConfig.getSecundaryWeapon() == null ? "0" : weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();
        int strength = 0, dexterity = 0, precision = 0;
        
        config.getPlayer().GetComponent<combatController>().calculateAttributesLevelUp(ref strength, ref dexterity, ref precision);

        _vitalityValue.text = statSystem.getVitality().getLevel().ToString();
        _enduranceValue.text = statSystem.getEndurance().getLevel().ToString();
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
        _armorPenValue.text = penetrating.ToString() + "%";
        _bleedDamageValue.text = bleedDMG.ToString() + "%";
        _bleedProbabilityValue.text = bleedingProbability.ToString() + "%";
        _critDamageValue.text = critDMG.ToString() + "%";
        _critProbabilityValue.text = critProbability.ToString() + "%";
    }
}
