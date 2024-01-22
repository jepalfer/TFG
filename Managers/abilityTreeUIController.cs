using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class abilityTreeUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _UIList = new List<GameObject>();
    [SerializeField] private List<TextMeshProUGUI> _weaponsList = new List<TextMeshProUGUI>();
    private GameObject _instantiatedWeapon;
    private int _indexInList;
    private GameObject _weaponToGiveSkill;

    // Update is called once per frame
    void Update()
    {
        if (UIController.getIsAdquiringSkills())
        {
            if (inputManager.getKeyDown(inputEnum.Next.ToString()) && _indexInList < _UIList.Count)
            {

                /*
                if (SaveSystem.loadWeaponsState().getPrimaryIndex() != -1 && _UIList[_indexInList].GetComponent<generalItem>().getID() == SaveSystem.loadWeaponsState().getPrimaryIndex())
                {

                }

                if (SaveSystem.loadWeaponsState().getSecundaryIndex() != -1 && _UIList[_indexInList].GetComponent<generalItem>().getID() == SaveSystem.loadWeaponsState().getSecundaryIndex())
                {

                }
                */


                _weaponsList[_indexInList].color = Color.white;
                _UIList[_indexInList].SetActive(false);
                _indexInList += 1;
                _weaponsList[_indexInList].color = Color.yellow;
                _UIList[_indexInList].SetActive(true);

                if (_instantiatedWeapon != null)
                {
                    Destroy(_instantiatedWeapon);
                    _instantiatedWeapon = null;
                }

                manageUpgradeSkillLogic();

                /*
                if (_UIList[_indexInList].GetComponent<generalItem>().getID() == Config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[SaveSystem.loadWeaponsState().getPrimaryIndex()].getID())
                {

                }
                else if (_UIList[_indexInList].GetComponent<generalItem>().getID() == Config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[SaveSystem.loadWeaponsState().getSecundaryIndex()].getID())
                {

                    Debug.Log("HOLA");
                }
                else
                {
                    _instantiatedWeapon = Instantiate(Config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Find(weapon => weapon.getID() == _UIList[_indexInList].GetComponent<generalItem>().getID()).getWeapon());
                }*/
                if (_weaponsList[_indexInList].GetComponent<treeController>() != null)
                {
                    EventSystem.current.SetSelectedGameObject(_weaponsList[_indexInList].GetComponent<treeController>().getInitialSkill());
                }
            }

            if (inputManager.getKeyDown(inputEnum.Previous.ToString()) && _indexInList > 0)
            {
                _weaponsList[_indexInList].color = Color.white;
                _UIList[_indexInList].SetActive(false);
                _indexInList -= 1;
                _weaponsList[_indexInList].color = Color.yellow;
                _UIList[_indexInList].SetActive(true);

                if (_instantiatedWeapon != null)
                {
                    Destroy(_instantiatedWeapon);
                    _instantiatedWeapon = null;
                }
                manageUpgradeSkillLogic();
                /*
                if (_UIList[_indexInList].GetComponent<generalItem>().getID() == Config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[SaveSystem.loadWeaponsState().getPrimaryIndex()].getID())
                {

                }
                else if (_UIList[_indexInList].GetComponent<generalItem>().getID() == Config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[SaveSystem.loadWeaponsState().getSecundaryIndex()].getID())
                {

                    Debug.Log("HOLA");
                }
                else
                {
                    _instantiatedWeapon = Instantiate(Config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Find(weapon => weapon.getID() == _UIList[_indexInList].GetComponent<generalItem>().getID()).getWeapon());
                }
                */

                if (_weaponsList[_indexInList].GetComponent<treeController>() != null)
                {
                    EventSystem.current.SetSelectedGameObject(_weaponsList[_indexInList].GetComponent<treeController>().getInitialSkill());
                }
            }
        }
        
    }

    public void initializePanel()
    {
        _indexInList = 0;
        _weaponsList[0].color = Color.yellow;
        _UIList[0].SetActive(true);

        manageUpgradeSkillLogic();
        /*
        if (_UIList[_indexInList].GetComponent<generalItem>().getID() == Config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[SaveSystem.loadWeaponsState().getPrimaryIndex()].getID())
        {

        }
        else if (_UIList[_indexInList].GetComponent<generalItem>().getID() == Config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[SaveSystem.loadWeaponsState().getSecundaryIndex()].getID())
        {

            Debug.Log("HOLA");
        }
        else
        {
            _instantiatedWeapon = Instantiate(Config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Find(weapon => weapon.getID() == _UIList[_indexInList].GetComponent<generalItem>().getID()).getWeapon());
        }*/
        EventSystem.current.SetSelectedGameObject(_UIList[_indexInList].GetComponent<treeController>().getInitialSkill());
    }

    public void manageUpgradeSkillLogic()
    {
        lootItem selectedWeapon = config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getTipo() == itemType.weapon && item.getID() == _UIList[_indexInList].GetComponent<generalItem>().getID());

        if (selectedWeapon != null)             //Hemos desbloqueado el arma
        {
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
            }
        }
        saveSystem.saveSkillsState(data.getUnlockedSkills());
    }

    public void setPanelsOff()
    {
        _UIList[_indexInList].SetActive(false);
        _weaponsList[_indexInList].color = Color.white;
        _indexInList = 0;
        if (_instantiatedWeapon != null)
        {
            Destroy(_instantiatedWeapon);
            _instantiatedWeapon = null;
        }
    }

    public GameObject getCurrentUI()
    {
        return _UIList[_indexInList];
    }
}
