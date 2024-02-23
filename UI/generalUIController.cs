using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// generalUIController es una clase que se encarga de manejar la UI general.
/// </summary>
public class generalUIController : MonoBehaviour
{
    /// <summary>
    /// Referencia a la barra de vida.
    /// </summary>
    [SerializeField] private Slider _HPSlider;

    /// <summary>
    /// Referencia a la barra de stamina.
    /// </summary>
    [SerializeField] private Slider _StaminaSlider;

    /// <summary>
    /// Referencia al campo de texto que indica de cuántas almas disponemos.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _souls;

    /// <summary>
    /// Referencia a la zona de la UI donde aparecen los objetos que obtenemos para feedback visual.
    /// </summary>
    [SerializeField] private RectTransform _lootGrid;

    /// <summary>
    /// Referencia al prefab que se instancia cuando obtenemos un objeto para claridad visual.
    /// </summary>
    [SerializeField] private GameObject _lootSlot;

    /// <summary>
    /// Referencia a los datos guardados sobre la vida y stamina.
    /// </summary>
    private statsData _stats;

    /// <summary>
    /// Referencia a los datos guardados sobre las almas.
    /// </summary>
    private soulsData _data;

    /// <summary>
    /// Método que se ejecuta al iniciar el script.
    /// Inicializa la UI.
    /// </summary>
    private void Start()
    {
        //Calculamos la vida y stamina maxima del jugador
        config.getPlayer().GetComponent<statsController>().setMaxHP(statSystem.getVitality().getLevel() * config.getPlayer().GetComponent<statsController>().getMaxHpExp());
        config.getPlayer().GetComponent<statsController>().setMaxStamina(statSystem.getEndurance().getLevel() * config.getPlayer().GetComponent<statsController>().getMaxStaminaExp());

        //Ponemos bien la barra de vida
        RectTransform rectTransform = getHPBar().GetComponent<RectTransform>();
        Vector2 _actual = rectTransform.sizeDelta;

        _actual.x += config.getPlayer().GetComponent<statsController>().getHPExp() * statSystem.getVitality().getLevel();
        rectTransform.sizeDelta = _actual;

        //Ponemos bien la barra de stamina
        rectTransform = getStaminaBar().GetComponent<RectTransform>();
        _actual = rectTransform.sizeDelta;
        
        _actual.x += config.getPlayer().GetComponent<statsController>().getStaminaExp() * statSystem.getEndurance().getLevel();
        rectTransform.sizeDelta = _actual;

        //Cargamos la información de los stats
        _stats = saveSystem.loadStats();

        if (_stats == null)
        {
            //Modificamos la información del jugador
            config.getPlayer().GetComponent<statsController>().setCurrentHP(config.getPlayer().GetComponent<statsController>().getMaxHP());
            config.getPlayer().GetComponent<statsController>().setCurrentStamina(config.getPlayer().GetComponent<statsController>().getMaxStamina());

            saveSystem.saveStats();
        }
        else
        {
            //Modificamos la información del jugador
            config.getPlayer().GetComponent<statsController>().setCurrentHP(_stats.getCurrentHP());
            config.getPlayer().GetComponent<statsController>().setCurrentStamina(_stats.getCurrentStamina());
            //Modificamos la UI con la información guardada
            setHPBarValue(config.getPlayer().GetComponent<statsController>().getCurrentHP() / config.getPlayer().GetComponent<statsController>().getMaxHP());
            setStaminaBarValue(config.getPlayer().GetComponent<statsController>().getCurrentStamina() / config.getPlayer().GetComponent<statsController>().getMaxStamina());
        }

        //Rellenamos la barra de vida como debería estar
        config.getPlayer().GetComponent<statsController>().recalculateBar(statSystem.getVitality().getLevel(), getHPBar().GetComponent<RectTransform>(), config.getPlayer().GetComponent<statsController>().getHPExp());
        config.getPlayer().GetComponent<statsController>().recalculateBar(statSystem.getEndurance().getLevel(), getStaminaBar().GetComponent<RectTransform>(), config.getPlayer().GetComponent<statsController>().getStaminaExp());

        //Cargamos la información de las almas
        _data = saveSystem.loadSouls();
        TextMeshProUGUI _LUAlmas = UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getSoulsValue();

        //Modificamos los campos según corresponda
        if (_data == null)
        {
            _souls.text = "0";
            _LUAlmas.text = "0";
        }
        else
        {
            _souls.text = _data.getSouls().ToString();
            _LUAlmas.text = _data.getSouls().ToString();
        }

        //Establecemos variables estáticas y guardamos datos
        generalUIConfiguration.setSouls(_souls);
        levelUpUIConfiguration.setSoulsValue(_LUAlmas);
        saveSystem.saveSouls();

        //Establecemos el valor de las almas del combatController del jugador
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
    }

    /// <summary>
    /// Método que modifica <see cref="_HPSlider"/>.
    /// Ver <see cref="statsController.receiveDMG(float)"/> para más información.
    /// </summary>
    /// <param name="dmg">El daño recibido.</param>
    public void receiveDMG(float dmg)
    {
        //Calculamos cuánto debe disminuir el slider
        float _received = dmg / config.getPlayer().GetComponent<statsController>().getMaxHP();
        _HPSlider.value -= _received;
    }

    /// <summary>
    /// Método que modifica <see cref="_HPSlider"/>.
    /// Ver <see cref="statsController.healHP(float)"/> para más información.
    /// </summary>
    /// <param name="heal">El daño curado.</param>
    public void heal(float heal)
    {
        //Calculamos cuánto debe aumentar el slider
        float _healed = heal / config.getPlayer().GetComponent<statsController>().getMaxHP();
        _HPSlider.value += _healed;
    }
    /// <summary>
    /// Método que modifica <see cref="_StaminaSlider"/>.
    /// Ver <see cref="statsController.useStamina(float)"/> para más información.
    /// </summary>
    /// <param name="stamina">La stamina gastada.</param>
    public void useStamina(float stamina)
    {
        //Calculamos cuánto debe disminuir el slider
        float _used = stamina / config.getPlayer().GetComponent<statsController>().getMaxStamina();
        _StaminaSlider.value -= _used;
    }

    /// <summary>
    /// Método que modifica <see cref="_StaminaSlider"/>.
    /// Ver <see cref="statsController.restoreStamina(float)"/> para más información.
    /// </summary>
    /// <param name="stamina">La stamina recuperada.</param>
    public void recoverStamina(float stamina)
    {
        //Calculamos cuánto debe aumentar el slider
        float _recovered = stamina / config.getPlayer().GetComponent<statsController>().getMaxStamina();
        _StaminaSlider.value += _recovered;

    }

    /// <summary>
    /// Método auxiliar para cambiar el campo <see cref="_souls"/>.
    /// </summary>
    /// <param name="field">El campo a cambiar.</param>
    /// <param name="souls">El valor a asignar.</param>
    public static void updateSoulsUI(ref TextMeshProUGUI field, string souls)
    {
        field.text = souls;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_HPSlider"/>.
    /// </summary>
    /// <returns>Un Slider que representa la barra de vida.</returns>
    public Slider getHPBar()
    {
        return _HPSlider;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_StaminaSlider"/>.
    /// </summary>
    /// <returns>Un Slider que representa la barra de stamina.</returns>
    public Slider getStaminaBar()
    {
        return _StaminaSlider;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_souls"/>.
    /// </summary>
    /// <returns>Un TextMeshProUGUI que representa el campo de texto de las almas.</returns>

    public TextMeshProUGUI getSouls()
    {
        return _souls;
    }

    /// <summary>
    /// Setter que modifica el valor de <see cref="_HPSlider"/>.
    /// </summary>
    /// <param name="value">El valor a asignar.</param>
    public void setHPBarValue(float value)
    {
        _HPSlider.value = value;
    }
    /// <summary>
    /// Setter que modifica el valor de <see cref="_StaminaSlider"/>.
    /// </summary>
    /// <param name="value">El valor a asignar.</param>
    public void setStaminaBarValue(float value)
    {
        _StaminaSlider.value = value;
    }

    /// <summary>
    /// Método auxiliar utilizado para mostrar los objetos que obtenemos en <see cref="_lootGrid"/>.
    /// </summary>
    /// <param name="item">El objeto a mostrar.</param>
    public void showItemAdded(lootItem item)
    {
        //Instanciamos y modificamos
        GameObject newSlot = Instantiate(_lootSlot);
        newSlot.GetComponent<lootSlot>().setSprite(item.getIcon());
        newSlot.GetComponent<lootSlot>().setText(item.getName());
        newSlot.GetComponent<lootSlot>().setLootQuantity(item.getQuantity());

        newSlot.transform.SetParent(_lootGrid);
    }
}
