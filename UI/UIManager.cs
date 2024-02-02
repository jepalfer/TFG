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
    [SerializeField] private RectTransform _lootGrid;
    [SerializeField] private GameObject _lootSlot;


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


        //Calculamos la vida y stamina maxima del jugador

        config.getPlayer().GetComponent<statsController>().setMaxHP(statSystem.getVitality().getLevel() * config.getPlayer().GetComponent<statsController>().getMaxHpExp());
        config.getPlayer().GetComponent<statsController>().setMaxStamina(statSystem.getEndurance().getLevel() * config.getPlayer().GetComponent<statsController>().getMaxStaminaExp());

        //StatsController.setMaxHP(statSystem.getVitality().getLevel() * StatsController.getMaxHpExp());
        //StatsController.setMaxStamina(statSystem.getEndurance().getLevel() * StatsController.getMaxStaminaExp());


        //Ponemos bien la barra de vida

        RectTransform rectTransform = UIConfig.getController().gameObject.GetComponent<UIManager>().getHPBar().GetComponent<RectTransform>();
        Vector2 _actual = rectTransform.sizeDelta;
        //_actual.x += StatsController.getHPExp() * statSystem.getVitality().getLevel();
        _actual.x += config.getPlayer().GetComponent<statsController>().getHPExp() * statSystem.getVitality().getLevel();
        rectTransform.sizeDelta = _actual;

        //Ponemos bien la barra de stamina
        rectTransform = UIConfig.getController().gameObject.GetComponent<UIManager>().getStaminaBar().GetComponent<RectTransform>();
        _actual = rectTransform.sizeDelta;
        _actual.x += config.getPlayer().GetComponent<statsController>().getStaminaExp() * statSystem.getEndurance().getLevel();
        //_actual.x += StatsController.getStaminaExp() * statSystem.getEndurance().getLevel();
        rectTransform.sizeDelta = _actual;

        statsData stats = saveSystem.loadStats();

        if (stats == null)
        {
            //StatsController.setCurrentHP(StatsController.getMaxHP());
            //StatsController.setCurrentStamina(StatsController.getMaxStamina());
            config.getPlayer().GetComponent<statsController>().setCurrentHP(config.getPlayer().GetComponent<statsController>().getMaxHP());
            config.getPlayer().GetComponent<statsController>().setCurrentStamina(config.getPlayer().GetComponent<statsController>().getMaxStamina());

            saveSystem.saveStats();
        }
        else
        {
            //StatsController.setCurrentHP(stats._currentHP);
            //StatsController.setCurrentStamina(stats._currentStamina);

            //GetComponent<UIManager>().setHPBarValue(StatsController.getCurrentHP() / StatsController.getMaxHP());
            //GetComponent<UIManager>().setStaminaBarValue(StatsController.getCurrentStamina() / StatsController.getMaxStamina());

            config.getPlayer().GetComponent<statsController>().setCurrentHP(stats.getCurrentHP());
            config.getPlayer().GetComponent<statsController>().setCurrentStamina(stats.getCurrentStamina());

            UIConfig.getController().gameObject.GetComponent<UIManager>().setHPBarValue(config.getPlayer().GetComponent<statsController>().getCurrentHP() / config.getPlayer().GetComponent<statsController>().getMaxHP());
            UIConfig.getController().gameObject.GetComponent<UIManager>().setStaminaBarValue(config.getPlayer().GetComponent<statsController>().getCurrentStamina() / config.getPlayer().GetComponent<statsController>().getMaxStamina());
        }

        config.getPlayer().GetComponent<statsController>().recalculateBar(statSystem.getVitality().getLevel(), UIConfig.getController().gameObject.GetComponent<UIManager>().getHPBar().GetComponent<RectTransform>(), config.getPlayer().GetComponent<statsController>().getHPExp());

        config.getPlayer().GetComponent<statsController>().recalculateBar(statSystem.getEndurance().getLevel(), UIConfig.getController().gameObject.GetComponent<UIManager>().getStaminaBar().GetComponent<RectTransform>(), config.getPlayer().GetComponent<statsController>().getStaminaExp());

        soulsData data = saveSystem.loadSouls();
        TextMeshProUGUI _LUAlmas = UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getSoulsValue();

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
        levelUpUIConfiguration.setSoulsValue(_LUAlmas);
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

    public void showItemAdded(lootItem item)
    {
        GameObject newSlot = Instantiate(_lootSlot);
        newSlot.GetComponent<lootSlot>().setSprite(item.getIcon());
        newSlot.GetComponent<lootSlot>().setText(item.getName());
        newSlot.GetComponent<lootSlot>().setLootQuantity(item.getQuantity());

        newSlot.transform.SetParent(_lootGrid);
    }
}
