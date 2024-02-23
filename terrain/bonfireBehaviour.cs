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
    /// Referencia a los datos de los enemigos.
    /// </summary>
    private enemyStateData _enemiesData;

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
        if ((inputManager.GetKeyDown(inputEnum.interact) && _playerIsOn && !UIController.getIsInPauseUI() && !UIController.getIsInLevelUpUI() && !UIController.getIsInAdquireSkillUI() && !UIController.getIsInLevelUpWeaponUI() && !UIController.getIsInInventoryUI()))
        {
            useBonfireUI();
        }
    }

    /// <summary>
    /// Método que se ejecuta al pulsar <see cref="_restButton"/>.
    /// Recarga la escena cambiando algunos datos.
    /// </summary>
    public void rest()
    {
        saveSystem.saveLastBonfireData();
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

        //Cargamos la escena en la que nos encontramos
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //Salimos de la UI
        useBonfireUI();
    }

    /// <summary>
    /// Método que se ejecuta al pulsar <see cref="_levelUpButton"/>.
    /// Entra en la UI de subir de nivel.
    /// </summary>
    public void levelUp()
    {
        useBonfireUI();
        UIConfig.getController().useLevelUpUI();
    }

    /// <summary>
    /// Método que se ejecuta al pulsar <see cref="_levelUpWeaponButton"/>.
    /// Entra en la UI de subir de nivel un arma.
    /// </summary>
    public void levelUpWeapon()
    {
        useBonfireUI();
        UIConfig.getController().useWeaponUI();
    }

    /// <summary>
    /// Método que se ejecuta al pulsar <see cref="_unlockSkillsButton"/>.
    /// Entra en la UI de subir de desbloquar habilidades.
    /// </summary>
    public void unlockSkills()
    {
        useBonfireUI();
        UIConfig.getController().useSkillsUI();
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
    /// Getter que devuelve <see cref="_isInBonfireMenu"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos o no en el menú de la hoguera.</returns>
    public static bool getIsInBonfireMenu()
    {
        return _isInBonfireMenu;
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
        Time.timeScale = _isInBonfireMenu ? 0f : 1f;

        //Si entramos modificamos el objeto seleccionado en el EventSystem
        if (_isInBonfireMenu)
        {
            EventSystem.current.SetSelectedGameObject(_restButton.gameObject);
        }
        else //Si no la ponemos a defaul con todos los textos a blanco para dar coherencia visual
        {
            _restText.color = Color.white;
            _levelUpText.color = Color.white;
            _resumeText.color = Color.white;
            _levelUpWeaponText.color = Color.white;
            _unlockSkillsText.color = Color.white;
            
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
