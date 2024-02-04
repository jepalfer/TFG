using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class skillTreeUIController : MonoBehaviour
{
    [SerializeField] private GameObject _rightPanel;
    private GameObject _instantiatedWeapon;
    private int _indexInList;
    private GameObject _weaponToGiveSkill;

    [SerializeField] private List<GameObject> _prefabs;
    [SerializeField] private List<GameObject> _instantiatedTreesPrefabs;
    [SerializeField] private List<TextMeshProUGUI> _menusNames;
    [SerializeField] private RectTransform _skillTreeContent;
    [SerializeField] private Image _leftButtonImage;
    [SerializeField] private Image _rightButtonImage;
    [SerializeField] private Image _skillSprite;
    [SerializeField] private TextMeshProUGUI _skillName;
    [SerializeField] private TextMeshProUGUI _skillDesc;
    [SerializeField] private TextMeshProUGUI _skillPrice;
    private GameObject _formerEventSystemSelected = null;

    // Update is called once per frame
    void Update()
    {
        if (_indexInList < (_instantiatedTreesPrefabs.Count - 1) && inputManager.GetKeyDown(inputEnum.next)) 
        {
            _menusNames[_indexInList].color = Color.white;
            _instantiatedTreesPrefabs[_indexInList].SetActive(false);

            _indexInList++;

            _instantiatedTreesPrefabs[_indexInList].SetActive(true);
            _menusNames[_indexInList].color = Color.yellow;

            if (_instantiatedWeapon != null)
            {
                Destroy(_instantiatedWeapon);
                _instantiatedWeapon = null;
            }
            manageUpgradeSkillLogic();

            EventSystem.current.SetSelectedGameObject(_instantiatedTreesPrefabs[_indexInList].GetComponent<treeController>().getInitialSkill());
        }

        if (_indexInList > 0 && inputManager.GetKeyDown(inputEnum.previous))
        {
            _menusNames[_indexInList].color = Color.white;
            _instantiatedTreesPrefabs[_indexInList].SetActive(false);

            _indexInList--;

            _instantiatedTreesPrefabs[_indexInList].SetActive(true);
            _menusNames[_indexInList].color = Color.yellow;

            if (_instantiatedWeapon != null)
            {
                Destroy(_instantiatedWeapon);
                _instantiatedWeapon = null;
            }
            manageUpgradeSkillLogic();

            EventSystem.current.SetSelectedGameObject(_instantiatedTreesPrefabs[_indexInList].GetComponent<treeController>().getInitialSkill());
        }
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        if (currentSelected != _formerEventSystemSelected)
        {
            _formerEventSystemSelected = currentSelected;
            modifyRightPanel();
        }

    }

    private void modifyRightPanel()
    {
        Debug.Log(_formerEventSystemSelected);
        _skillSprite.sprite = _formerEventSystemSelected.GetComponent<skill>().getSkillSprite();
        _skillName.text = _formerEventSystemSelected.GetComponent<skill>().getSkillName();
        _skillDesc.text = _formerEventSystemSelected.GetComponent<skill>().getSkillDescription();
        _skillPrice.text = _formerEventSystemSelected.GetComponent<skill>().getSkillPoints().ToString();

        if (config.getPlayer().GetComponent<combatController>().getSouls() < _formerEventSystemSelected.GetComponent<skill>().getSkillPoints())
        {
            _skillPrice.color = Color.red;
        }
        else
        {
            _skillPrice.color = Color.black;
        }
    }

    public void initializeUI()
    {
        _instantiatedTreesPrefabs = new List<GameObject>();
        inventoryData data = saveSystem.loadInventory();

        for (int i = 0; i < _menusNames.Count; ++i)
        {
            _menusNames[i].text = "";
        }

        _leftButtonImage.GetComponent<Image>().enabled = false;
        _rightButtonImage.GetComponent<Image>().enabled = false;
        if (data != null)
        {
            List<serializedItemData> unlockedWeapons = data.getInventory().FindAll(item => item.getData().getTipo() == itemTypeEnum.weapon);

            if (unlockedWeapons.Count > 0)
            {
                _rightPanel.SetActive(true);
                if (unlockedWeapons.Count > 1)
                {
                    _leftButtonImage.GetComponent<Image>().enabled = true;
                    _rightButtonImage.GetComponent<Image>().enabled = true;
                }
            }

            for (int i = 0; i < unlockedWeapons.Count; ++i)
            {
                GameObject newPrefab = _prefabs.Find(prefab => prefab.GetComponent<generalItem>().getID() == unlockedWeapons[i].getData().getID());
                if (newPrefab != null)
                {
                    GameObject instantiatedPrefab = Instantiate(newPrefab);
                    instantiatedPrefab.transform.SetParent(_skillTreeContent, false);
                    _instantiatedTreesPrefabs.Add(instantiatedPrefab);
                    _menusNames[i].text = unlockedWeapons[i].getData().getName();
                }
            }
        }

        _indexInList = 0;

        if (_instantiatedTreesPrefabs.Count > 0)
        {
            _menusNames[_indexInList].color = Color.yellow;
            _instantiatedTreesPrefabs[_indexInList].SetActive(true);

            manageUpgradeSkillLogic();

            EventSystem.current.SetSelectedGameObject(_instantiatedTreesPrefabs[_indexInList].GetComponent<treeController>().getInitialSkill());
        }
    }

    public void manageUpgradeSkillLogic()
    {
        lootItem selectedWeapon = config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getTipo() == itemTypeEnum.weapon && item.getID() == _instantiatedTreesPrefabs[_indexInList].GetComponent<generalItem>().getID());

        weaponSlot unlockedWeapon = config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Find(weapon => weapon.getID() == selectedWeapon.getID());
        if (unlockedWeapon.getHand() == handEnum.primary)
        {

            if (saveSystem.loadWeaponsState().getPrimaryIndex() == -1 || saveSystem.loadWeaponsState().getPrimaryIndex() != config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().IndexOf(unlockedWeapon))
            {
                _instantiatedWeapon = Instantiate(unlockedWeapon.getWeapon());
                _weaponToGiveSkill = _instantiatedWeapon;
            }
            else
            {
                _weaponToGiveSkill = weaponConfig.getPrimaryWeapon();
            }
        }
        else
        {

            if (saveSystem.loadWeaponsState().getSecundaryIndex() == -1 || saveSystem.loadWeaponsState().getSecundaryIndex() != config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().IndexOf(unlockedWeapon))
            {
                _instantiatedWeapon = Instantiate(unlockedWeapon.getWeapon());
                _weaponToGiveSkill = _instantiatedWeapon;
            }
            else
            {
                _weaponToGiveSkill = weaponConfig.getSecundaryWeapon();
            }
        }
    }

    public void unlockSkill(skill skill)
    {
       //_weaponToGiveSkill.GetComponent<Weapon>().addSkill(skill);
        unlockedSkillsData data = saveSystem.loadSkillsState();
        

        sceneSkillsState newSkill = new sceneSkillsState(_weaponToGiveSkill.GetComponent<weapon>().getID(), skill);
        if (data == null)
        {
            unlockedSkillsData aux = new unlockedSkillsData();
            aux.getUnlockedSkills().Add(newSkill);
            data = aux;
        }
        else
        {
            data.getUnlockedSkills().Add(newSkill);
            for(int i = 0; i < data.getUnlockedSkills().Count; ++i)
            {
                Debug.Log(data.getUnlockedSkills()[i].getAssociatedSkill().getSkillID());
                Debug.Log("ID: " + data.getUnlockedSkills()[i].getWeaponID());
            }
        }
        saveSystem.saveSkillsState(data.getUnlockedSkills());
    }

    public void setUIOff()
    {
        _menusNames[_indexInList].color = Color.white;
        _indexInList = 0;
        if (_instantiatedWeapon != null)
        {
            Destroy(_instantiatedWeapon);
            _instantiatedWeapon = null;
        }

        for (int i = 0; i < _instantiatedTreesPrefabs.Count; ++i)
        {
            Destroy(_instantiatedTreesPrefabs[i]);
        }

        _instantiatedTreesPrefabs.Clear();
    }

    public GameObject getCurrentUI()
    {
        return _instantiatedTreesPrefabs[_indexInList];
    }

    public List<GameObject> getAllUIs()
    {
        return _instantiatedTreesPrefabs;
    }
}
