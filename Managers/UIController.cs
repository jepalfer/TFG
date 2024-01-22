using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
public class UIController : MonoBehaviour
{
    private static bool _isPaused = false;
    private static bool _isLevelingUp = false;
    private static bool _isAdquiringSkills = false;
    private static bool _isLevelingUpWeapon = false;
    private static bool _isInInventory = false;
    private static bool _isInEquipUI = false;
    private static bool _isEquippingSkill = false;

    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _skillsUI;
    [SerializeField] private GameObject _equipSkillsUI;
    [SerializeField] private GameObject _selectSkillUI;
    [SerializeField] private GameObject _statsUI;
    [SerializeField] private GameObject _weaponUI;
    [SerializeField] private GameObject _generalUI;
    [SerializeField] private GameObject _inventoryUI;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private TextMeshProUGUI _inventoryValue;
    [SerializeField] private TextMeshProUGUI _inventoryText;
    [SerializeField] private TextMeshProUGUI _backupValue;
    [SerializeField] private GameObject _render;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _equipText;

    private void Start()
    {
        UIConfig.setController(this);
        _name.text = profileSystem.getProfileName();
    }

    private void Update()
    {
        if (!_isPaused)
        {
            if (inputManager.getKeyDown(inputEnum.Pause.ToString()) && !bonfireBehaviour.getIsInBonfireMenu() && !_isLevelingUp && !_isInEquipUI && !_isAdquiringSkills && !_isLevelingUpWeapon && !_isInInventory)
            {
                UIConfig.getController().usePauseUI();
            }
        }
        else
        {
            if ((!_isLevelingUp && !_isInEquipUI && !_isAdquiringSkills && !_isLevelingUpWeapon && !_isInInventory && !bonfireBehaviour.getIsInBonfireMenu()))
            {
                if (inputManager.getKeyDown(inputEnum.Pause.ToString()))
                {
                    UIConfig.getController().usePauseUI();
                }

            }
        }


        if (bonfireBehaviour.getIsInBonfireMenu())
        {
            if (inputManager.getKeyDown(inputEnum.Cancel.ToString()))
            {
                UIConfig.getBonfire().useBonfireUI();
             
            }
        }
        if (_isInInventory)
        {
            if (inputManager.getKeyDown(inputEnum.Cancel.ToString()))
            {
                UIConfig.getController().useInventoryUI();
                UIConfig.getController().usePauseUI();
            }
        }

        if (bonfireBehaviour.getIsInBonfireMenu())
        {
            if (inputManager.getKeyDown(inputEnum.Cancel.ToString()))
            {
                UIConfig.getBonfire().useBonfireUI();
            }
        }

        if (_isInEquipUI && !_isEquippingSkill)
        {
            if (inputManager.getKeyDown(inputEnum.Cancel.ToString()))
            {
                UIConfig.getController().useEquipSkillsUI();
                UIConfig.getController().usePauseUI();
            }
        }

        if (_isLevelingUp)
        {
            if (inputManager.getKeyDown(inputEnum.Cancel.ToString()))
            {
                UIConfig.getController().useLevelUpUI();
                UIConfig.getBonfire().useBonfireUI();
            }
        }

        if (_isLevelingUpWeapon)
        {
            if (inputManager.getKeyDown(inputEnum.Cancel.ToString()))
            {
                UIConfig.getController().useWeaponUI();
                UIConfig.getBonfire().useBonfireUI();
            }
        }

        if (_isEquippingSkill)
        {
            if (inputManager.getKeyDown(inputEnum.Cancel.ToString()))
            {
                UIConfig.getController().useSelectSkillUI();
            }
        }

        if (_isAdquiringSkills)
        {
            if (inputManager.getKeyDown(inputEnum.Cancel.ToString()))
            {
                UIConfig.getController().useSkillsUI();
                UIConfig.getBonfire().useBonfireUI();
            }
        }

    }


    //Menu de pausa
    public void Resume()
    {
        _pauseUI.SetActive(false);
        //gameObject.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void Quit()
    {
        saveSystem.savePlayer();
        _isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public static bool getIsPaused()
    {
        return _isPaused;
    }

    public static bool getIsLevelingUp()
    {
        return _isLevelingUp;
    }

    public static bool getIsAdquiringSkills()
    {
        return _isAdquiringSkills;
    }

    public static bool getIsLevelingUpWeapon()
    {
        return _isLevelingUpWeapon;
    }
    public static bool getIsInEquipUI()
    {
        return _isInEquipUI;
    }

    public static bool getIsInInventory()
    {
        return _isInInventory;
    }
    public static bool getIsEquippingSkill()
    {
        return _isEquippingSkill;
    }


    public GameObject getEquipSkillsUI()
    {
        return _equipSkillsUI;
    }
    public GameObject getLevelUpUI()
    {
        return _statsUI;
    }
    public void usePauseUI()
    {
        _pauseUI.SetActive(!_pauseUI.activeSelf);
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0f : 1f;

        if (_isPaused)
        {
            EventSystem.current.SetSelectedGameObject(_resumeButton.gameObject);
        }
    }
    public void useSkillsUI()
    {
        if (!_isAdquiringSkills)
        {
            GetComponent<abilityTreeUIController>().initializePanel();
        }
        else
        {
            GetComponent<abilityTreeUIController>().setPanelsOff();
        }
        _skillsUI.SetActive(!_skillsUI.activeSelf);
        _isAdquiringSkills = !_isAdquiringSkills;
        Time.timeScale = _isAdquiringSkills ? 0f : 1f;
    }
    public void useLevelUpUI()
    {
        _statsUI.SetActive(!_statsUI.activeSelf);
        _isLevelingUp = !_isLevelingUp;
        Time.timeScale = _isLevelingUp ? 0f : 1f;
    }

    public void useWeaponUI()
    {

        _weaponUI.SetActive(!_weaponUI.activeSelf);
        _isLevelingUpWeapon = !_isLevelingUpWeapon;
        Time.timeScale = _isLevelingUpWeapon ? 0f : 1f;

        if (_isLevelingUpWeapon)
        {
            _weaponUI.GetComponent<levelUpWeaponUIController>().enterUI();
        }
        else
        {
            _weaponUI.GetComponent<levelUpWeaponUIController>().exitUI();
        }
    }

    public void useEquipSkillsUI()
    {
        _equipSkillsUI.SetActive(!_equipSkillsUI.activeSelf);
        _isInEquipUI = !_isInEquipUI;
        Time.timeScale = _isInEquipUI ? 0f : 1f;

        if (_isInEquipUI)     //Hemos entrado al menu
        {
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
    public void useInventoryUI()
    {
        GetComponent<inventoryUIManagement>().setCounter(0);
        int total = 0;
        List<lootItem> inventory = config.getInventory().GetComponent<inventoryManager>().getInventory();
        List<lootItem> backup = config.getInventory().GetComponent<inventoryManager>().getBackUp();

        for (int i = 0; i < inventory.Count; i++)
        {
            total += inventory[i].getQuantity();
        }
        total = 0;
        for (int i = 0; i < backup.Count; i++)
        {
            total += backup[i].getQuantity();
        }

        if (!_isInInventory)
        {
            _inventoryText.color = Color.white;
            UIConfig.getController().usePauseUI();
            GetComponent<inventoryUIManagement>().initializePanels();
        }
        else
        {
            GetComponent<inventoryUIManagement>().setInformationPanelOff();
            GetComponent<inventoryUIManagement>().destroyItems();
            GetComponent<inventoryUIManagement>().setPanelsOff();
        }
        _isInInventory = !_isInInventory;
        _inventoryUI.SetActive(!_inventoryUI.activeSelf);
        Time.timeScale = _isInInventory ? 0f : 1f;
    }

    public void useSelectSkillUI()
    {
        _isEquippingSkill = !_isEquippingSkill;

        if (_isEquippingSkill)
        {
            unlockedSkillsData data = saveSystem.loadSkillsState();

            if (data != null)
            {
                if (data.getUnlockedSkills().Count > 0)
                {
                    _selectSkillUI.GetComponent<equipSkillController>().createSkillInventory(data);
                }
            }
        }
        else
        {
            _selectSkillUI.GetComponent<equipSkillController>().destroySkillInventory();
        }
        _selectSkillUI.SetActive(!_selectSkillUI.activeSelf);
    }
    public void setBackUpValue(int value)
    {
        _backupValue.text = value.ToString() + "/" + config.getInventory().GetComponent<inventoryManager>().getMaximumBackUp().ToString();
    }
    public void setInventoryValue(int value)
    {
        _inventoryValue.text = value.ToString() + "/" + config.getInventory().GetComponent<inventoryManager>().getMaximumInventory().ToString();
    }

    public void setRenderImage(Sprite render)
    {
        _render.GetComponent<Image>().sprite = render;
    }

    public void setDescriptionText(string text)
    {
        _description.text = text;
    }
}
