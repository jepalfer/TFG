using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _HPSlider;
    [SerializeField] private Slider _StaminaSlider;
    [SerializeField] private TextMeshProUGUI _souls;


    private void Start()
    {
        /*        RectTransform rectTransform = _HP_slider.GetComponent<RectTransform>(); // Obtener el componente RectTransform

                Vector2 _actual = rectTransform.sizeDelta;
                _actual.x += Config.getPlayer().GetComponent<StatsController>().getHPExp();

                rectTransform.sizeDelta = _actual;

                rectTransform = _Stamina_slider.GetComponent<RectTransform>(); // Obtener el componente RectTransform

                _actual = rectTransform.sizeDelta;
                _actual.x += Config.getPlayer().GetComponent<StatsController>().getStaminaExp();

                rectTransform.sizeDelta = _actual;
        */
        config.getPlayer().GetComponent<statsController>().recalculateBar(statSystem.getVitality().getLevel(), UIConfig.getController().gameObject.GetComponent<UIManager>().getHPBar().GetComponent<RectTransform>(), config.getPlayer().GetComponent<statsController>().getHPExp());

        config.getPlayer().GetComponent<statsController>().recalculateBar(statSystem.getEndurance().getLevel(), UIConfig.getController().gameObject.GetComponent<UIManager>().getStaminaBar().GetComponent<RectTransform>(), config.getPlayer().GetComponent<statsController>().getStaminaExp());

        soulsData data = saveSystem.loadSouls();
        TextMeshProUGUI _LUAlmas = UIConfig.getController().getLevelUpUI().GetComponent<levelUpUI>().getSoulsValue();

        if (data == null)
        {
            _souls.text = "0";
            _LUAlmas.text = "0";
        }
        else
        {
            _souls.text = data.getSouls().ToString();
            _LUAlmas.text = data.getSouls().ToString();
        }

        generalUIConfiguration.setAlmas(_souls);
        levelUPUIConfiguration.setSoulsValue(_LUAlmas);
        saveSystem.saveSouls();
        long souls;
        if (long.TryParse(_souls.text, out souls))
        {
        }
        else
        {
            // No se pudo convertir el texto a int para 'level'
            Debug.LogError("Error al convertir almas a long");
        }
        config.getPlayer().GetComponent<combatController>().setSouls(souls);

        /*
        if (long.TryParse(GeneralUIConfiguration.getAlmas().text, out _souls))
        {
        }
        else
        {
            // No se pudo convertir el texto a long para 'requiredSouls'
            Debug.LogError("Error al convertir almas a long");
        }*/
    }
    public void receiveDMG(float dmg)
    {
        float _received = dmg / config.getPlayer().GetComponent<statsController>().getMaxHP();

        //_received = dmg / _total_hp;

        _HPSlider.value -= _received;
    }

    public void heal(float heal)
    {
        float _healed = heal / config.getPlayer().GetComponent<statsController>().getMaxHP();

        //_healed = heal / _total_hp;

        
        _HPSlider.value += _healed;
    }
    public void useStamina(float stamina)
    {
        float _used = stamina / config.getPlayer().GetComponent<statsController>().getMaxStamina();

        //_received = stamina / _total_stamina;

        _StaminaSlider.value -= _used;
    }

    public void recoverStamina(float stamina)
    {
        float _recovered = stamina / config.getPlayer().GetComponent<statsController>().getMaxStamina();

        //_recovered = stamina / _total_stamina;

        _StaminaSlider.value += _recovered;

    }

    public static void setSouls(ref TextMeshProUGUI field, string souls)
    {
        field.text = souls;
    }

    public Slider getHPBar()
    {
        return _HPSlider;
    }
    public Slider getStaminaBar()
    {
        return _StaminaSlider;
    }

    public TextMeshProUGUI getSouls()
    {
        return _souls;
    }

    public void setHPBarValue(float value)
    {
        _HPSlider.value = value;
    }
    public void setStaminaBarValue(float value)
    {
        _StaminaSlider.value = value;
    }
}
