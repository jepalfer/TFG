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
    /// Booleano que indica si estamos o no en el men� de pausa.
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
    /// Booleano que indica si estamos o no comprando en una tienda.
    /// </summary>
    private static bool _isInShopUI = false;

    /// <summary>
    /// Booleano que indica si estamos o no mirando el estado del jugador.
    /// </summary>
    private static bool _isInStateUI = false;

    /// <summary>
    /// Booleano que indica si estamos o no comprando objetos.
    /// </summary>
    private static bool _isInBuyingUI = false;

    /// <summary>
    /// Booleano que indica si estamos o no en el men� de opciones.
    /// </summary>
    private static bool _isInOptionsUI = false;


    /// <summary>
    /// Referencia a la UI del men� de pausa.
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
    /// Referencia a la UI de tienda.
    /// </summary>
    [SerializeField] private GameObject _shopUI;
    /// <summary>
    /// Referencia a la UI de estado del jugador.
    /// </summary>
    [SerializeField] private GameObject _stateUI;

    /// <summary>
    /// Referencia a la UI de seleccionar cantidad de objetos a comprar.
    /// </summary>
    [SerializeField] private GameObject _buyItemUI;

    /// <summary>
    /// Referencia a la UI del men� de opciones.
    /// </summary>
    [SerializeField] private GameObject _optionsUI;
    
    /// <summary>
    /// Referencia al bot�n de continuar partida.
    /// </summary>
    [SerializeField] private Button _resumeButton;

    /// <summary>
    /// Referencia al texto del bot�n para entrar a <see cref="_inventoryUI"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _inventoryText;

    /// <summary>
    /// Referencia al texto del bot�n para entrar a <see cref="_equipSkillsUI"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _equipText;

    /// <summary>
    /// Referencia al sprite del objeto obtenido.
    /// </summary>
    [SerializeField] private Image _selectedObjectSprite;

    /// <summary>
    /// Referencia al nombre del objeto obtenido.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _selectedObjectName;
    /// <summary>
    /// Referencia a la cantidad del objeto obtenido.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _selectedObjectQuantity;

    /// <summary>
    /// Objeto que estaba seleccionado antes de entrar a la UI de equipar objeto o de comprar objeto.
    /// </summary>
    private GameObject _lastSelectedInInventory;

    [SerializeField] private Button _optionsButton;

    /// <summary>
    /// Referencia al �ltimo objeto seleccionado.
    /// </summary>
    private GameObject _formerEventSystemSelected = null;

    /// <summary>
    /// M�todo que se ejecuta al inicio del script.
    /// Asigna la variable est�tica correspondiente de <see cref="UIConfig"/>.
    /// </summary>
    private void Awake()
    {
        UIConfig.setController(this);
    }

    /// <summary>
    /// M�todo que se llama cada frame para actualizar la l�gica.
    /// Controla las UIs seg�n la tecla/bot�n que se utiliza.
    /// </summary>
    private void Update()
    {
        //Pausamos si no estamos en pausa
        if (!_isInPauseUI)
        {
            if (inputManager.GetKeyDown(inputEnum.pause) && !bonfireBehaviour.getIsInBonfireMenu() && 
                !_isInLevelUpUI && !_isInEquippingSkillUI && !_IsInAdquireSkillUI && !_isInLevelUpWeaponUI && 
                !_isInInventoryUI && !_isInOptionsUI && !_isInShopUI && !_isInStateUI)
            {
                UIConfig.getController().usePauseUI();
            }
        }
        else //Quitamos la pausa
        {
            if ((!_isInLevelUpUI && !_isInEquippingSkillUI && !_IsInAdquireSkillUI && !_isInLevelUpWeaponUI && 
                 !_isInInventoryUI && !bonfireBehaviour.getIsInBonfireMenu() && !_isInOptionsUI && !_isInShopUI && !_isInStateUI))
            {
                if (inputManager.GetKeyDown(inputEnum.pause))
                {
                    UIConfig.getController().usePauseUI();
                    _formerEventSystemSelected = null;
                    config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
                }

            }
        }

        //Ocultamos la UI de inventario
        if (_isInInventoryUI && !_isInEquippingItemUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useInventoryUI();
                UIConfig.getController().usePauseUI();
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }

        //Ocultamos la UI de la hoguera
        if (bonfireBehaviour.getIsInBonfireMenu() && !bonfireBehaviour.getIsInNoItem() && !bonfireBehaviour.getIsInObtainCharge())
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                Debug.Log("bonf");
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
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }

        //Ocultamos la UI de subir de nivel
        if (_isInLevelUpUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useLevelUpUI();
                UIConfig.getBonfire().useBonfireUI();
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }

        //Ocultamos la UI de subir de nivel un arma
        if (_isInLevelUpWeaponUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useWeaponUI();
                UIConfig.getBonfire().useBonfireUI();
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }

        //Ocultamos la UI de seleccionar habilidad
        if (_isInSelectingSkillUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useSelectSkillUI();
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }

        //Ocultamos la UI de adquirir habilidad
        if (_IsInAdquireSkillUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useSkillsUI();
                UIConfig.getBonfire().useBonfireUI();
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }
        //Ocultamos la UI de equipar un objeto
        if (_isInEquippingItemUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useEquippingObjectUI();
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }

        //Ocultamos la UI de comprar en la tienda
        if (_isInShopUI && !_isInBuyingUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useShopUI(UIConfig.getController().getShopUI().GetComponent<shopUIController>().getShopItemsList());
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }

        //Ocultamos la UI de estado del jugador
        if (_isInStateUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useStateUI();
                UIConfig.getController().usePauseUI();
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }

        if (_isInBuyingUI)
        {
            if (inputManager.GetKeyDown(inputEnum.cancel))
            {
                UIConfig.getController().useBuyItemUI();
                _formerEventSystemSelected = null;
                config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            }
        }

        //if (_isInPauseUI)
        //{

        //    GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        //    if (currentSelected != _formerEventSystemSelected && currentSelected != null)
        //    {
        //        if (_formerEventSystemSelected != null)
        //        {
        //            config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
        //        }
        //        _formerEventSystemSelected = currentSelected;
        //    }

        //}
    }


    /// <summary>
    /// M�todo que se llama al pulsar <see cref="_resumeButton"/>. Quita el men� de pausa.
    /// </summary>
    public void resume()
    {
        _pauseUI.SetActive(false);
        //gameObject.SetActive(false);
        Time.timeScale = 1f;
        _isInPauseUI = false;
        config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
    }

    /// <summary>
    /// M�todo que se llama al pulsar el bot�n para salir de partida. Sale de la partida.
    /// </summary>
    public void quit()
    {
        saveSystem.savePlayer();
        saveSystem.saveLastScene();
        _isInPauseUI = false;
        config.getEnemiesList().Clear();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInPauseUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no en el men� de pausa.</returns>
    public static bool getIsInPauseUI()
    {
        return _isInPauseUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInLevelUpUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no subiendo de nivel.</returns>
    public static bool getIsInLevelUpUI()
    {
        return _isInLevelUpUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_IsInAdquireSkillUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no adquiriendo una habilidad.</returns>
    public static bool getIsInAdquireSkillUI()
    {
        return _IsInAdquireSkillUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInLevelUpWeaponUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no subiendo de nivel un arma.</returns>
    public static bool getIsInLevelUpWeaponUI()
    {
        return _isInLevelUpWeaponUI;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_isInEquippingSkillUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no equipando una habilidad.</returns>
    public static bool getIsInEquippingSkillUI()
    {
        return _isInEquippingSkillUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInInventoryUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no en el men� de inventario.</returns>
    public static bool getIsInInventoryUI()
    {
        return _isInInventoryUI;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_isInSelectingSkillUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no seleccionando una habilidad.</returns>
    public static bool getIsSelectingSkillUI()
    {
        return _isInSelectingSkillUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInEquippingItemUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no seleccionando un objeto.</returns>
    public static bool getIsEquippingObjectUI()
    {
        return _isInEquippingItemUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInShopUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no comprando un objeto en una tienda.</returns>
    public static bool getIsInShopUI()
    {
        return _isInShopUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInStateUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no mirando el estado del jugador.</returns>
    public static bool getIsInStateUI()
    {
        return _isInStateUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInOptionsUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no en el men� de opciones.</returns>
    public static bool getIsInOptionsUI()
    {
        return _isInOptionsUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInBuyingUI"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos o no seleccionando la cantidad de compra.</returns>
    public static bool getIsInBuyingUI()
    {
        return _isInBuyingUI;
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
    /// <returns>Un GameObject que representa la UI del men� de equipar habilidades.</returns>

    public GameObject getEquipSkillsUI()
    {
        return _equipSkillsUI;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_selectSkillUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI del men� de seleccionar habilidades.</returns>

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
    /// <returns>Un GameObject que representa la UI del men� de subir armas de nivel.</returns>
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
    /// Getter que devuelve <see cref="_shopUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI de una tienda.</returns>
    public GameObject getShopUI()
    {
        return _shopUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_stateUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI del estado del jugador.</returns>
    public GameObject getStateUI()
    {
        return _stateUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_buyItemUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI de seleccionar cantidad de compra.</returns>
    public GameObject getBuyObjectUI()
    {
        return _buyItemUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_optionsUI"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la UI del men� de pausa.</returns>
    public GameObject getOptionsUI()
    {
        return _optionsUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_lastSelectedInInventory"/>.
    /// </summary>
    /// <returns>GameObject que contiene una referencia al �ltimo objeto seleccionado para equiparlo.</returns>
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
    /// M�todo que activa o desactiva <see cref="_pauseUI"/>.
    /// </summary>
    public void usePauseUI()
    {
        //Activamos o desactivamos
        _pauseUI.SetActive(!_pauseUI.activeSelf);
        _isInPauseUI = !_isInPauseUI;

        //El elemento seleccionado es el boton de continuar
        if (_isInPauseUI)
        {
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(_resumeButton.gameObject);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    /// <summary>
    /// M�todo que activa o desactiva <see cref="_skillsUI"/>.
    /// </summary>
    public void useSkillsUI()
    {
        //Activamos o desactivamos
        _skillsUI.SetActive(!_skillsUI.activeSelf);
        _IsInAdquireSkillUI = !_IsInAdquireSkillUI;
        if (_IsInAdquireSkillUI)
        {
            //Si entramos inicializamos la UI
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            _skillsUI.GetComponent<skillTreeUIController>().initializeUI();
        }
        else
        {
            //Si no la quitamos
            _skillsUI.GetComponent<skillTreeUIController>().setUIOff();
        }
    }
    /// <summary>
    /// M�todo que activa o desactiva <see cref="_statsUI"/>.
    /// </summary>
    public void useLevelUpUI()
    {
        //Activamos o desactivamos
        _statsUI.SetActive(!_statsUI.activeSelf);
        _isInLevelUpUI = !_isInLevelUpUI;
        //Time.timeScale = _isInLevelUpUI ? 0f : 1f;

        if (_isInLevelUpUI)
        {
            //Si entramos inicializamo la UI
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            _statsUI.GetComponent<levelUpUIController>().initializeUI();
        }
        else
        {
            //Si no la quitamos
            _statsUI.GetComponent<levelUpUIController>().setUIOff();
        }

    }

    /// <summary>
    /// M�todo que activa o desactiva <see cref="_weaponUI"/>.
    /// </summary>
    public void useWeaponUI()
    {
        //Activamos o desactivamos
        _weaponUI.SetActive(!_weaponUI.activeSelf);
        _isInLevelUpWeaponUI = !_isInLevelUpWeaponUI;

        if (_isInLevelUpWeaponUI)
        {
            //Si entramos inicializamos la UI
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            _weaponUI.GetComponent<levelUpWeaponUIController>().initializeUI();
        }
        else
        {
            //Si no la quitamos
            _weaponUI.GetComponent<levelUpWeaponUIController>().setUIOff();
        }
    }

    /// <summary>
    /// M�todo que activa o desactiva <see cref="_equipSkillsUI"/>.
    /// </summary>
    public void useEquipSkillsUI()
    {
        //Activamos o desactivamos
        _equipSkillsUI.SetActive(!_equipSkillsUI.activeSelf);
        _isInEquippingSkillUI = !_isInEquippingSkillUI;

        if (_isInEquippingSkillUI)
        {
            //Si entramos quitamos el men� de pausa y inicializamos algunos datos
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            UIConfig.getController().usePauseUI();
            _equipSkillsUI.GetComponent<skillUIController>().setID(0);
            EventSystem.current.SetSelectedGameObject(_equipSkillsUI.GetComponent<skillUIController>().getSlots()[0]);
            _equipSkillsUI.GetComponent<skillUIController>().getSlots()[0].SetActive(true);
            Time.timeScale = 0f;
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
    /// M�todo que activa o desactiva <see cref="_inventoryUI"/>.
    /// </summary>
    public void useInventoryUI()
    {
        //Activamos o desactivamos
        _inventoryUI.SetActive(!_inventoryUI.activeSelf);
        _isInInventoryUI = !_isInInventoryUI;

        if (_isInInventoryUI)
        {
            //Si entramos inicializamos la UI
            _inventoryText.color = Color.white;
            UIConfig.getController().usePauseUI();
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            _inventoryUI.GetComponent<inventoryUIController>().initializeUI();
            Time.timeScale = 0f;
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
    /// M�todo que activa o desactiva <see cref="_selectSkillUI"/>.
    /// </summary>
    public void useSelectSkillUI()
    {
        //Activamos o desactivamos
        _selectSkillUI.SetActive(!_selectSkillUI.activeSelf);
        _isInSelectingSkillUI = !_isInSelectingSkillUI;

        if (_isInSelectingSkillUI)
        {
            //Si entramos inicializamos la UI
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            unlockedSkillsData data = saveSystem.loadSkillsState();

            if (data != null)
            {
                 _selectSkillUI.GetComponent<selectSkillUIController>().createSkillInventory(data);
            }
        }
        else
        {
            //Si no la quitamos y eliminamos lo necesario
            _selectSkillUI.GetComponent<selectSkillUIController>().destroySkillInventory();
        }
    }

    /// <summary>
    /// M�todo que activa o desactiva <see cref="_equipItemUI"/>.
    /// </summary>
    public void useEquippingObjectUI()
    {
        _equipItemUI.SetActive(!_equipItemUI.activeSelf);
        _isInEquippingItemUI = !_isInEquippingItemUI;

        if (_isInEquippingItemUI)
        {
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
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
    /// M�todo que activa o desactiva <see cref="_shopUI"/>.
    /// </summary>
    public void useShopUI(List<shopItem> shopItemsList)
    {
        _shopUI.SetActive(!_shopUI.activeSelf);
        _isInShopUI = !_isInShopUI;

        if (_isInShopUI)
        {
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            _shopUI.GetComponent<shopUIController>().initializeUI(shopItemsList);
            Time.timeScale = 0f;
        }
        else
        {
            _shopUI.GetComponent<shopUIController>().setUIOff();
            Time.timeScale = 1f;
        }
    }
    /// <summary>
    /// M�todo que activa o desactiva <see cref="_stateUI"/>.
    /// </summary>
    public void useStateUI()
    {
        _stateUI.SetActive(!_stateUI.activeSelf);
        _isInStateUI = !_isInStateUI;

        if (_isInStateUI)
        {
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX(); 
            UIConfig.getController().usePauseUI();
            _stateUI.GetComponent<stateUIController>().initializeUI();
            Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// M�todo que activa o desactiva <see cref="_buyItemUI"/>.
    /// </summary>
    public void useBuyItemUI()
    {
        _buyItemUI.SetActive(!_buyItemUI.activeSelf);
        _isInBuyingUI = !_isInBuyingUI;

        if (_isInBuyingUI)
        {
            _lastSelectedInInventory = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject; 
            int itemIndex = _lastSelectedInInventory.GetComponent<shopItemSlotLogic>().getSlotID();
            int itemPrice = _shopUI.GetComponent<shopUIController>().getShopItemsList()[itemIndex].getPrice();
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();

            GameObject itemToBuy = _shopUI.GetComponent<shopUIController>().getShopItemsList()[_lastSelectedInInventory.GetComponent<shopItemSlotLogic>().getSlotID()].getItem();

            //generalItem itemToBuy = _shopUI.GetComponent<shopUIController>().getShopItemsList()[_lastSelectedInInventory.GetComponent<shopItemSlotLogic>().getSlotID()].getItem().GetComponent<generalItem>();

            _buyItemUI.GetComponent<buyItemUIController>().initializeUI(itemToBuy, itemPrice, itemIndex);
        }
        else
        {
            _buyItemUI.GetComponent<buyItemUIController>().setUIOff();
        }
    }

    /// <summary>
    /// M�todo que activa o desactiva <see cref="_optionsUI"/>.
    /// </summary>
    public void useOptionsUI()
    {
        _optionsUI.SetActive(!_optionsUI.activeSelf);
        _isInOptionsUI = !_isInOptionsUI;

        if (_isInOptionsUI)
        {
            UIConfig.getController().usePauseUI();
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            _optionsUI.GetComponent<optionsManager>().initializeUI();
            Time.timeScale = 0f;
        }
        else
        {
            UIConfig.getController().usePauseUI();
            _formerEventSystemSelected = null;
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
            EventSystem.current.SetSelectedGameObject(_optionsButton.gameObject);
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
