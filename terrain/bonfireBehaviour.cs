using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class bonfireBehaviour : MonoBehaviour
{
    [SerializeField] private bool _playerIsOn = false;
    [SerializeField] private GameObject _player;
    private static bool _isInBonfireMenu = false;

    [SerializeField] private GameObject _bonfireUI;
    [SerializeField] private Button _restButton;
    [SerializeField] private TextMeshProUGUI _restText;
    [SerializeField] private Button _levelUpButton;
    [SerializeField] private TextMeshProUGUI _levelUpText;
    [SerializeField] private Button _levelUpWeaponButton;
    [SerializeField] private TextMeshProUGUI _levelUpWeaponText;
    [SerializeField] private Button _unlockSkillsButton;
    [SerializeField] private TextMeshProUGUI _unlockSkillsText;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private TextMeshProUGUI _resumeText;


    private void Start()
    {
        _player = config.getPlayer();
        UIConfig.setBonfire(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == _player.transform.tag)
        {
            _playerIsOn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == _player.transform.tag)
        {
            _playerIsOn = false;
        }
    }

    private void Update()
    {
        if ((inputManager.GetKeyDown(inputEnum.interact) && _playerIsOn && !UIController.getIsPaused() && !UIController.getIsLevelingUp() && !UIController.getIsAdquiringSkills() && !UIController.getIsLevelingUpWeapon() && !UIController.getIsInInventory()))
        {
            useBonfireUI();
        }
    }

    public void rest()
    {
        /*
        enemyStateData data = saveSystem.loadEnemyData();

        if (data != null)
        {
            for (int i = 0; i < data.getEnemyStates().Count; ++i)
            {
                data.getEnemyStates()[i].setCanRevive(1);
            }

        }
        saveSystem.saveEnemyData(data.getEnemyStates());*/

        restoreHealth();
        restoreStamina();
        saveSystem.savePlayer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        useBonfireUI();
    }

    public void levelUp()
    {
        useBonfireUI();
        UIConfig.getController().useLevelUpUI();
    }

    public void levelUpWeapon()
    {
        useBonfireUI();
        UIConfig.getController().useWeaponUI();
    }
    private void restoreHealth()
    {
        _player.GetComponent<statsController>().healHP(_player.GetComponent<statsController>().getMaxHP());
    }

    private void restoreStamina()
    {
        _player.GetComponent<statsController>().restoreStamina(_player.GetComponent<statsController>().getMaxStamina());
    }

    public void unlockSkills()
    {
        useBonfireUI();
        UIConfig.getController().useSkillsUI();
    }
    public static bool getIsInBonfireMenu()
    {
        return _isInBonfireMenu;
    }


    public void useBonfireUI()
    {
        _bonfireUI.SetActive(!_bonfireUI.activeSelf);
        _isInBonfireMenu = !_isInBonfireMenu;
        Time.timeScale = _isInBonfireMenu ? 0f : 1f;

        if (_isInBonfireMenu)
        {
            EventSystem.current.SetSelectedGameObject(_restButton.gameObject);
        }
        else
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
