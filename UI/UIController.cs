using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
/// <summary>
/// UIController es una clase que controla las distintas interfaces de usuario.
/// </summary>
public class UIController : MonoBehaviour
{
    /// <summary>
    /// Booleano que indica si estamos o no en el menú de pausa.
    /// </summary>
    private static bool _isInPauseUI = false;

    /// <summary>
    /// Booleano que indica si estamos o no subiendo de nivel.
    /// </summary>
    private static bool _isInLevelUpUI = false;

    /// <summary>
    /// Booleano que indica si estamos o no adquiriendo habilidades.
    /// </summary>
    private static bool _IsInAdquireSkillUI = false;

    /// <summary>
    /// Booleano que indica si estamos o no subiendo un arma nivel.
    /// </summary>
    private static bool _isInLevelUpWeaponUI = false;

    /// <summary>
    /// Booleano que indica si estamos o no en el inventario.
    /// </summary>
    private static bool _isInInventoryUI = false;

    /// <summary>
    /// Booleano que indica si estamos o no equipando una habilidad.
    /// </summary>
    private static bool _isInEquippingSkillUI = false;

    /// <summary>
    /// Booleano que indica si estamos o no seleccionado una habilidad.
    /// </summary>
    private static bool _isInSelectingSkillUI = false;
    /// <summary>
    /// Booleano que indica si estamos o no equipando un objeto.
    /// </summary>
    private static bool _isInEquippingItemUI = false;

    /// <summary>
    /// Referencia a la UI del menú de pausa.
    /// </summary>
    [SerializeField] private GameObject _pauseUI;

    /// <summary>
    /// Referencia a la UI de compra de habilidades.
    /// </summary>
    [SerializeField] private GameObject _skillsUI;

    /// <summary>
    /// Referencia a la UI de equipar habilidades.
    /// </summary>
    [SerializeField] private GameObject _equipSkillsUI;

    /// <summary>
    /// Referencia a la UI de seleccionar habilidad.
    /// </summary>
    [SerializeField] private GameObject _selectSkillUI;

    /// <summary>
    /// Referencia a la UI de subir de nivel.
    /// </summary>
    [SerializeField] private GameObject _statsUI;

    /// <summary>
    /// Referencia a la UI de subir de nivel un arma.
    /// </summary>
    [SerializeField] private GameObject _weaponUI;

    /// <summary>
    /// Referencia a la UI general.
    /// </summary>
    [SerializeField] private GameObject _generalUI;
    /// <summary>
    /// Referencia a la UI de equipar un objeto.
    /// </summary>
    [SerializeField] private GameObject _equipItemUI;

    /// <summary>
    /// Referencia a la UI de inventario.
    /// </summary>
    [SerializeField] private GameObject _inventoryUI;

    /// <summary>
    /// Referencia al texto que aparece en <see cref="_pauseUI"/> que es el nombre del perfil.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _name;
    
    /// <summary>
    /// Referencia al botón de continuar partida.
    /// </summary>
    [SerializeField] private Button _resumeButton;

    /// <summary>
    /// Referencia al texto del botón para entrar a <see cref="_inventoryUI"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _inventoryText;

    /// <summary>
    /// Referencia al texto del botón para entrar a <see cref="_equipSkillsUI"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _equipText;

    /// <summary>
    /// Referencia al sprite del objeto seleccionado.
    /// </summary>
    [SerializeField] private Image _selectedObjectSprite;

    /// <summary>
    /// Referencia al nombre del objeto seleccionado.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _selectedObjectName;
    /// <summary>
    /// Referencia a la cantidad del objeto seleccionado.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _selectedObjectQuantity;

    /// <summary>
    /// Objeto que estaba seleccionado antes de entrar a la UI de equipar objeto.
    /// </summary>
    private GameObject _lastSelectedInInventory;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// Asigna la variable estática correspondiente de <see cref="UIConfig"/>.
    /// </summary>
    private void Awake()
    {
        UIConfig.setController(this);
        _name.text = profileSystem.getProfileName();
    }

    /// <summary>
    /// Método que se llama cada frame para actualizar la lógica.
    /// Controla las UIs según la tecla/botón que se utiliza.
    /// </summary>
    private void Update()
    {
        //Pausamos si no estamos en pausa
        if (!_isInPauseUI)
        {
            if (inputManager.GetKeyDown(inputEnum.pause) && !bonfireBehaviour.getIsInBonfireMenu() && !_isInLevelUpUI && !_isInEquippingSkillUI && !_IsInAdquireSkillUI && !_isInLevelUpWeaponUI && !_isInInventoryUI)
            {
                UIConfig.getController().usePauseUI();
            }
        }
        else //Quitamos la pausa
        {
            if ((!_isInLevelUpUI && !_isInEquippingSkillUI && !_IsInAdquireSkillUI && !_isInLevelUpWeaponUI && !_isInInventoryUI && !bonfireBehaviour.getIsInBonfireMenu()))
            {
                if (inputManager.GetKeyDown(inputEnum.pause))
                {
                    UIConfig.getController().usePauseUI();
                }

            }
        }

        //Mostramos la UI de la hoguera
        if (bonfireBehaviour.getIsInBonfireMenu())
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getBonfire().useBonfireUI();
             
            }
        }

        //Ocultamos la UI de inventario
        if (_isInInventoryUI && !_isInEquippingItemUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useInventoryUI();
                UIConfig.getController().usePauseUI();
            }
        }

        //Ocultamos la UI de la hoguera
        if (bonfireBehaviour.getIsInBonfireMenu())
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getBonfire().useBonfireUI();
            }
        }

        //Ocultamos la UI de equipar habilidades
        if (_isInEquippingSkillUI && !_isInSelectingSkillUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useEquipSkillsUI();
                UIConfig.getController().usePauseUI();
            }
        }

        //Ocultamos la UI de subir de nivel
        if (_isInLevelUpUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useLevelUpUI();
                UIConfig.getBonfire().useBonfireUI();
            }
        }

        //Ocultamos la UI de subir de nivel un arma
        if (_isInLevelUpWeaponUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useWeaponUI();
                UIConfig.getBonfire().useBonfireUI();
            }
        }

        //Ocultamos la UI de seleccionar habilidad
        if (_isInSelectingSkillUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useSelectSkillUI();
            }
        }

        //Ocultamos la UI de adquirir habilidad
        if (_IsInAdquireSkillUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useSkillsUI();
                UIConfig.getBonfire().useBonfireUI();
            }
        }
        //Ocultamos la UI de equipar un objeto
        if (_isInEquippingItemUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useEquippingObjectUI();
            }
        }

    }


    /// <summary>
    /// Método que se llama al pulsar <see cref="_resumeButton"/>. Quita el menú de pausa.
    /// </summary>
    public void resume()
    {
        _pauseUI.SetActive(false);
        //gameObject.SetActive(false);
        Time.timeScale = 1f;
        _isInPauseUI = false;
    }

    /// <summary>
    /// Método que se llama al pulsar el botón para salir de partida. Sale de la partida.
    /// </summary>
    public void quit()
    {
        saveSystem.savePlayer();
        _isInPauseUI = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInPauseUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no en el menú de pausa.</returns>
    public static bool getIsPaused()
    {
        return _isInPauseUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInLevelUpUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no subiendo de nivel.</returns>
    public static bool getIsLevelingUp()
    {
        return _isInLevelUpUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_IsInAdquireSkillUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no adquiriendo una habilidad.</returns>
    public static bool getIsAdquiringSkills()
    {
        return _IsInAdquireSkillUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInLevelUpWeaponUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no subiendo de nivel un arma.</returns>
    public static bool getIsLevelingUpWeapon()
    {
        return _isInLevelUpWeaponUI;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_isInEquippingSkillUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no equipando una habilidad.</returns>
    public static bool getIsInEquipUI()
    {
        return _isInEquippingSkillUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInInventoryUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no en el menú de inventario.</returns>
    public static bool getIsInInventory()
    {
        return _isInInventoryUI;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_isInSelectingSkillUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no seleccionando una habilidad.</returns>
    public static bool getIsEquippingSkill()
    {
        return _isInSelectingSkillUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInEquippingItemUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no seleccionando un objeto.</returns>
    public static bool getIsEquippingObject()
    {
        return _isInEquippingItemUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_skillsUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI de adquirir habilidades.</returns>
    public GameObject getSkillTreesUI()
    {
        return _skillsUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_equipSkillsUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI del menú de equipar habilidades.</returns>

    public GameObject getEquipSkillsUI()
    {
        return _equipSkillsUI;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_selectSkillUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI del menú de seleccionar habilidades.</returns>

    public GameObject getSelectSkillsUI()
    {
        return _selectSkillUI;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_statsUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI de subir nivel.</returns>

    public GameObject getLevelUpUI()
    {
        return _statsUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_weaponUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI del menú de subir armas de nivel.</returns>
    public GameObject getWeaponLevelUPUI()
    {
        return _weaponUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_generalUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI general.</returns>
    public GameObject getGeneralUI()
    {
        return _generalUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_inventoryUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI del inventario.</returns>
    public GameObject getInventoryUI()
    {
        return _inventoryUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_lastSelectedInInventory"/>.
    /// </summary>
    /// <returns>GameObject que contiene una referencia al último objeto seleccionado para equiparlo.</returns>
    public GameObject getLastSelected()
    {
        return _lastSelectedInInventory;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_selectedObjectSprite"/>.
    /// </summary>
    /// <returns>Un elemento de tipo Image que contiene la referencia a la imagen.</returns>
    public Image getSprite()
    {
        return _selectedObjectSprite;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_selectedObjectName"/>.
    /// </summary>
    /// <returns>Un elemento de tipo TextMeshProUGUI que contiene la referencia al nombre.</returns>
    public TextMeshProUGUI getText()
    {
        return _selectedObjectName;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_selectedObjectQuantity"/>.
    /// </summary>
    /// <returns>Un elemento de tipo TextMeshProUGUI que contiene la referencia a la cantidad del objeto.</returns>
    public TextMeshProUGUI getQuantity()
    {
        return _selectedObjectQuantity;
    }

    /// <summary>
    /// Método que activa o desactiva <see cref="_pauseUI"/>.
    /// </summary>
    public void usePauseUI()
    {
        //Activamos o desactivamos
        _pauseUI.SetActive(!_pauseUI.activeSelf);
        _isInPauseUI = !_isInPauseUI;
        Time.timeScale = _isInPauseUI ? 0f : 1f;

        //El elemento seleccionado es el boton de continuar
        if (_isInPauseUI)
        {
            EventSystem.current.SetSelectedGameObject(_resumeButton.gameObject);
        }
    }
    /// <summary>
    /// Método que activa o desactiva <see cref="_skillsUI"/>.
    /// </summary>
    public void useSkillsUI()
    {
        //Activamos o desactivamos
        _skillsUI.SetActive(!_skillsUI.activeSelf);
        _IsInAdquireSkillUI = !_IsInAdquireSkillUI;
        Time.timeScale = _IsInAdquireSkillUI ? 0f : 1f;
        if (_IsInAdquireSkillUI)
        {
            //Si entramos inicializamos la UI
            _skillsUI.GetComponent<skillTreeUIController>().initializeUI();
        }
        else
        {
            //Si no la quitamos
            _skillsUI.GetComponent<skillTreeUIController>().setUIOff();
        }
    }
    /// <summary>
    /// Método que activa o desactiva <see cref="_statsUI"/>.
    /// </summary>
    public void useLevelUpUI()
    {
        //Activamos o desactivamos
        _statsUI.SetActive(!_statsUI.activeSelf);
        _isInLevelUpUI = !_isInLevelUpUI;
        Time.timeScale = _isInLevelUpUI ? 0f : 1f;

        if (_isInLevelUpUI)
        {
            //Si entramos inicializamo la UI
            _statsUI.GetComponent<levelUpUIController>().initializeUI();
        }
        else
        {
            //Si no la quitamos
            _statsUI.GetComponent<levelUpUIController>().setUIOff();
        }

    }

    /// <summary>
    /// Método que activa o desactiva <see cref="_weaponUI"/>.
    /// </summary>
    public void useWeaponUI()
    {
        //Activamos o desactivamos
        _weaponUI.SetActive(!_weaponUI.activeSelf);
        _isInLevelUpWeaponUI = !_isInLevelUpWeaponUI;
        Time.timeScale = _isInLevelUpWeaponUI ? 0f : 1f;

        if (_isInLevelUpWeaponUI)
        {
            //Si entramos inicializamos la UI
            _weaponUI.GetComponent<levelUpWeaponUIController>().initializeUI();
        }
        else
        {
            //Si no la quitamos
            _weaponUI.GetComponent<levelUpWeaponUIController>().setUIOff();
        }
    }

    /// <summary>
    /// Método que activa o desactiva <see cref="_equipSkillsUI"/>.
    /// </summary>
    public void useEquipSkillsUI()
    {
        //Activamos o desactivamos
        _equipSkillsUI.SetActive(!_equipSkillsUI.activeSelf);
        _isInEquippingSkillUI = !_isInEquippingSkillUI;
        Time.timeScale = _isInEquippingSkillUI ? 0f : 1f;

        if (_isInEquippingSkillUI)
        {
            //Si entramos quitamos el menú de pausa y inicializamos algunos datos
            UIConfig.getController().usePauseUI();
            _equipSkillsUI.GetComponent<skillUIController>().setID(0);
            EventSystem.current.SetSelectedGameObject(_equipSkillsUI.GetComponent<skillUIController>().getSlots()[0]);
            _equipSkillsUI.GetComponent<skillUIController>().getSlots()[0].SetActive(true); 
        }
        else
        {
            _equipText.color = Color.white;
            for (int i = 0; i < _equipSkillsUI.GetComponent<skillUIController>().getSlots().Count; ++i)
            {
                _equipSkillsUI.GetComponent<skillUIController>().getSlots()[i].SetActive(false);
            }
        }
    }
    /// <summary>
    /// Método que activa o desactiva <see cref="_inventoryUI"/>.
    /// </summary>
    public void useInventoryUI()
    {
        //Activamos o desactivamos
        _inventoryUI.SetActive(!_inventoryUI.activeSelf);
        _isInInventoryUI = !_isInInventoryUI;
        Time.timeScale = _isInInventoryUI ? 0f : 1f;

        if (_isInInventoryUI)
        {
            //Si entramos inicializamos la UI
            _inventoryText.color = Color.white;
            UIConfig.getController().usePauseUI();
            _inventoryUI.GetComponent<inventoryUIController>().initializeUI();
        }
        else
        {
            //Si no la quitamos y eliminamos lo necesario
            _inventoryUI.GetComponent<inventoryUIController>().setInformationPanelOff();
            _inventoryUI.GetComponent<inventoryUIController>().destroyItems();
            _inventoryUI.GetComponent<inventoryUIController>().setUIOff();
        }
    }

    /// <summary>
    /// Método que activa o desactiva <see cref="_selectSkillUI"/>.
    /// </summary>
    public void useSelectSkillUI()
    {
        //Activamos o desactivamos
        _selectSkillUI.SetActive(!_selectSkillUI.activeSelf);
        _isInSelectingSkillUI = !_isInSelectingSkillUI;

        if (_isInSelectingSkillUI)
        {
            //Si entramos inicializamos la UI
            unlockedSkillsData data = saveSystem.loadSkillsState();

            if (data != null)
            {
                 _selectSkillUI.GetComponent<equipSkillController>().createSkillInventory(data);
            }
        }
        else
        {
            //Si no la quitamos y eliminamos lo necesario
            _selectSkillUI.GetComponent<equipSkillController>().destroySkillInventory();
        }
    }

    /// <summary>
    /// Método que activa o desactiva <see cref="_equipItemUI"/>.
    /// </summary>
    public void useEquippingObjectUI()
    {
        _equipItemUI.SetActive(!_equipItemUI.activeSelf);
        _isInEquippingItemUI = !_isInEquippingItemUI;

        if (_isInEquippingItemUI)
        {
            _lastSelectedInInventory = EventSystem.current.currentSelectedGameObject;
            _equipItemUI.GetComponent<equipItemsUIController>().initializeUI();
        }
        else
        {
            _equipItemUI.GetComponent<equipItemsUIController>().setUIOff();
            EventSystem.current.SetSelectedGameObject(_lastSelectedInInventory);
        }
    }

    /// <summary>
    /// Setter que modifica el sprite de <see cref="_selectedObjectSprite"/>.
    /// </summary>
    /// <param name="sprite">El sprite.</param>
    public void setSprite(Sprite sprite)
    {
        _selectedObjectSprite.sprite = sprite;
    }

    /// <summary>
    /// Setter que modifica el texto de <see cref="_selectedObjectName"/>.
    /// </summary>
    /// <param name="text">Un string que contiene el nombre.</param>
    public void setText(string text)
    {
        _selectedObjectName.text = text;
    }

    /// <summary>
    /// Setter que modifica el texto de <see cref="_selectedObjectQuantity"/>.
    /// </summary>
    /// <param name="quantity">Un entero que representa la cantidad de la que disponemos del objeto.</param>
    public void setQuantity(int quantity)
    {
        _selectedObjectQuantity.text = quantity.ToString();
    }
}
