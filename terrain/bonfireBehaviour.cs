using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// bonfireBehaviour es una clase que se usa para manejar la lógica de las hogueras.
/// </summary>
public class bonfireBehaviour : MonoBehaviour
{
    /// <summary>
    /// Booleano que indica si el jugador está encima para poder interactuar.
    /// </summary>
    [SerializeField] private bool _playerIsOn = false;

    /// <summary>
    /// Referencia al jugador.
    /// </summary>
    [SerializeField] private GameObject _player;

    /// <summary>
    /// Booleano que indica si estamos o no en el menú de la hoguera.
    /// </summary>
    private static bool _isInBonfireMenu = false;

    /// <summary>
    /// Referencia a la UI de la hoguera.
    /// </summary>
    [SerializeField] private GameObject _bonfireUI;

    /// <summary>
    /// Referencia al botón de descansar.
    /// </summary>
    [SerializeField] private Button _restButton;

    /// <summary>
    /// Referencia al texto del botón <see cref="_restButton"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _restText;

    /// <summary>
    /// Referencia al botón de subir de nivel.
    /// </summary>
    [SerializeField] private Button _levelUpButton;

    /// <summary>
    /// Referencia al texto del botón <see cref="_levelUpButton"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _levelUpText;

    /// <summary>
    /// Referencia al botón de subir de nivel un arma.
    /// </summary>
    [SerializeField] private Button _levelUpWeaponButton;

    /// <summary>
    /// Referencia al texto del botón <see cref="_levelUpWeaponButton"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _levelUpWeaponText;
    
    /// <summary>
    /// Referencia al botón de desbloquar habilidades.
    /// </summary>
    [SerializeField] private Button _unlockSkillsButton;

    /// <summary>
    /// Referencia al texto del botón <see cref="_unlockSkillsButton"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _unlockSkillsText;

    /// <summary>
    /// Referencia al botón de volver al juego.
    /// </summary>
    [SerializeField] private Button _resumeButton;

    /// <summary>
    /// Referencia al texto del botón <see cref="_resumeButton"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _resumeText;

    /// <summary>
    /// Referencia al texto del botón para usar un objeto clave.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _useKeyItemText;

    /// <summary>
    /// Referencia a los datos de los enemigos.
    /// </summary>
    private enemyStateData _enemiesData;

    /// <summary>
    /// Referencia al GameObject para tener 1 consumible recargable más.
    /// </summary>
    [SerializeField] private GameObject _levelUpRefillable;

    /// <summary>
    /// Referencia al GameObject que contiene la UI que indica que no tienes objetos clave para obtener 1 carga extra.
    /// </summary>
    [SerializeField] private GameObject _noItemUI;

    /// <summary>
    /// Referencia al GameObject que contiene la UI que indica que tienes objetos clave para obtener 1 carga.
    /// </summary>
    [SerializeField] private GameObject _obtainChargeUI;

    /// <summary>
    /// Booleano que indica si estamos en la interfaz que indica que no tenemos objeto clave.
    /// </summary>
    private static bool _isInNoItemUI;

    /// <summary>
    /// Booleano que indica si estamos en la interfaz que indica que tenemos objeto clave.
    /// </summary>
    private static bool _isInObtainChargeUI;

    /// <summary>
    /// Referencia al último objeto seleccionado.
    /// </summary>
    private GameObject _formerEventSystemSelected = null;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// Asigna algunas variables, entre ellas la estática correspondiente.
    /// </summary>
    private void Start()
    {
        _player = config.getPlayer();
        UIConfig.setBonfire(this);
    }

    /// <summary>
    /// Método que sirve para actualizar <see cref="_playerIsOn"/>-
    /// </summary>
    /// <param name="collision">El collider que ha entrado en contacto.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == _player.transform.tag)
        {
            _playerIsOn = true;
        }
    }
    /// <summary>
    /// Método que sirve para actualizar <see cref="_playerIsOn"/>-
    /// </summary>
    /// <param name="collision">El collider que ha salido.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == _player.transform.tag)
        {
            _playerIsOn = false;
        }
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        if ((inputManager.GetKeyDown(inputEnum.interact) && _playerIsOn && !UIController.getIsInPauseUI() && 
            !UIController.getIsInLevelUpUI() && !UIController.getIsInAdquireSkillUI() && !UIController.getIsInLevelUpWeaponUI() && 
            !UIController.getIsInInventoryUI() && config.getPlayer().GetComponent<collisionController>().getIsOnPlatform()))
        {
            useBonfireUI();
        }

        if (_isInBonfireMenu)
        {
            GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
            //Debug.Log(currentSelected);
            if (currentSelected != _formerEventSystemSelected && currentSelected != null)
            {
                if (_formerEventSystemSelected != null)
                {
                    //config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
                }
            }
            _formerEventSystemSelected = currentSelected;

        }
    }

    /// <summary>
    /// Método que se ejecuta al pulsar <see cref="_restButton"/>.
    /// Recarga la escena cambiando algunos datos.
    /// </summary>
    public void rest()
    {
        saveSystem.saveLastBonfireData(true);
        //Cargamos los datos de los enemigos
        _enemiesData = saveSystem.loadEnemyData();

        if (_enemiesData != null)
        {
            //Modificamos que puedan revivir
            for (int i = 0; i < _enemiesData.getEnemyStates().Count; ++i)
            {
                _enemiesData.getEnemyStates()[i].setCanRevive(1);
            }

        }

        //Guardamos la modificación
        saveSystem.saveEnemyData(_enemiesData.getEnemyStates());

        //Rellenamos los objetos recargables
        config.getInventory().GetComponent<inventoryManager>().refill();

        //Restauramos toda la salud y stamina
        restoreHealth();
        restoreStamina();

        //Guardamos la posición del jugador
        saveSystem.savePlayer();
        saveSystem.saveLastScene();

        //Cargamos la escena en la que nos encontramos
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
        //Salimos de la UI
        useBonfireUI();
    }

    /// <summary>
    /// Método que se ejecuta al pulsar <see cref="_levelUpButton"/>.
    /// Entra en la UI de subir de nivel.
    /// </summary>
    public void levelUp()
    {
        UIConfig.getController().useLevelUpUI();
        useBonfireUI();
    }

    /// <summary>
    /// Método que se ejecuta al pulsar <see cref="_levelUpWeaponButton"/>.
    /// Entra en la UI de subir de nivel un arma.
    /// </summary>
    public void levelUpWeapon()
    {
        UIConfig.getController().useWeaponUI();
        useBonfireUI();
    }

    /// <summary>
    /// Método que se ejecuta al pulsar <see cref="_unlockSkillsButton"/>.
    /// Entra en la UI de subir de desbloquar habilidades.
    /// </summary>
    public void unlockSkills()
    {
        UIConfig.getController().useSkillsUI();
        useBonfireUI();
    }

    /// <summary>
    /// Método auxiliar que se llama al descansar. Recupera la salud.
    /// </summary>
    private void restoreHealth()
    {
        _player.GetComponent<statsController>().healHP(_player.GetComponent<statsController>().getMaxHP());
    }
    /// <summary>
    /// Método auxiliar que se llama al descansar. Recupera la stamina.
    /// </summary>

    private void restoreStamina()
    {
        _player.GetComponent<statsController>().restoreStamina(_player.GetComponent<statsController>().getMaxStamina());
    }

    /// <summary>
    /// Getter que devuelve <see cref="_levelUpRefillable"/>.
    /// </summary>
    /// <returns>Objeto para obtener 1 carga extra.</returns>
    public GameObject getChargeItem()
    {
        return _levelUpRefillable;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_noItemUI"/>.
    /// </summary>
    /// <returns>GameObject que contiene la UI que indica que no tienes objeto clave para obtener 1 carga más.</returns>
    public GameObject getNoItemUI()
    {
        return _noItemUI;
    }


    /// <summary>
    /// Getter que devuelve <see cref="_obtainChargeUI"/>.
    /// </summary>
    /// <returns>GameObject que contiene la UI que indica que tienes objeto clave para obtener 1 carga más.</returns>
    public GameObject getObtainChargeUI()
    {
        return _obtainChargeUI;
    }

    /// <summary>
    /// Método que se ejecuta al pulsar el botón de usar infusión del támesis.
    /// </summary>
    public void useKeyItem()
    {
        config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
        inventoryData loadedInventory = saveSystem.loadInventory();

        if (loadedInventory != null)
        {
            serializedItemData item = loadedInventory.getInventory().Find(item => item.getData().getID() == _levelUpRefillable.GetComponent<generalItem>().getID());
            if (item != null)
            {
                //Interfaz de confirmación de uso de objeto
                _obtainChargeUI.SetActive(true);
                _isInObtainChargeUI = true;
            }
            else
            {
                //Interfaz de aviso de que no tienes objeto
                _noItemUI.SetActive(true);
                _isInNoItemUI = true;
            }
        }
        else
        {
            //Interfaz de aviso de que no tienes objeto
            _noItemUI.SetActive(true);
            _isInNoItemUI = true;
        }
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInBonfireMenu"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos o no en el menú de la hoguera.</returns>
    public static bool getIsInBonfireMenu()
    {
        return _isInBonfireMenu;
    }

    /// <summary>
    /// Setter que modifica <see cref="_isInNoItemUI"/>.
    /// </summary>
    /// <param name="val">El valor a asignar.</param>
    public static void setIsInNoItem(bool val)
    {
        _isInNoItemUI = val;
    }

    /// <summary>
    /// Setter que modifica <see cref="_isInObtainChargeUI"/>.
    /// </summary>
    /// <param name="val">El valor a asignar.</param>
    public static void setIsInObtainCharge(bool val)
    {
        _isInObtainChargeUI = val;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInNoItemUI"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos en la UI que indica que no tenemos objeto clave.</returns>
    public static bool getIsInNoItem()
    {
        return _isInNoItemUI;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInObtainChargeUI"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos en la UI que indica que tenemos objeto clave.</returns>
    public static bool getIsInObtainCharge()
    {
        return _isInObtainChargeUI;
    }


    /// <summary>
    /// Método que activa o desactiva la UI de la hoguera.
    /// </summary>
    public void useBonfireUI()
    {
        //Activamos o desactivamos la UI
        _bonfireUI.SetActive(!_bonfireUI.activeSelf);
        _isInBonfireMenu = !_isInBonfireMenu;

        //Hacemos que no pase el tiempo
        //Time.timeScale = _isInBonfireMenu ? 0f : 1f;

        //Si entramos modificamos el objeto seleccionado en el EventSystem
        animatorEnum direction = config.getPlayer().GetComponent<playerMovement>().getIsFacingLeft() ? animatorEnum.front : animatorEnum.back;

        if (_isInBonfireMenu)
        {
            if (config.getPlayer().GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != 
                config.getPlayer().GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_idle, 1, direction) && 
                config.getPlayer().GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name !=
                config.getPlayer().GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_idle, 2, direction))
            {
                config.getPlayer().GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 1, direction);
            }
            _formerEventSystemSelected = null;
            EventSystem.current.SetSelectedGameObject(_restButton.gameObject);
        }
        else //Si no la ponemos a default con todos los textos a blanco para dar coherencia visual
        {
            if (!UIController.getIsInAdquireSkillUI() && !UIController.getIsInLevelUpUI() && !UIController.getIsInLevelUpWeaponUI())
            {
                config.getPlayer().GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_get_up, direction);
            }
            _restText.color = Color.white;
            _levelUpText.color = Color.white;
            _resumeText.color = Color.white;
            _levelUpWeaponText.color = Color.white;
            _unlockSkillsText.color = Color.white;
            _useKeyItemText.color = Color.white;
            
            //EventSystem.current.SetSelectedGameObject(null);
            config.getAudioManager().GetComponent<menuSFXController>().playMenuAcceptSFX();
        }
    }
}
