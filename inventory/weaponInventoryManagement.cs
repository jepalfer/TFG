using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class weaponInventoryManagement : MonoBehaviour
{
    [SerializeField] private List<weaponSlot> _weaponList;
    private slotData _pressedItem;
    private int _pressedIndex;
    private GameObject _instantiatedPrefab;
    private weaponSlot _clickedWeapon;
    private GameObject _primary;
    private GameObject _secundary;

    private void Start()
    {
        equippedWeaponsData data = saveSystem.loadWeaponsState();


        if (data != null)
        {
            if (data.getPrimaryIndex() != -1)
            {
                _primary = Instantiate(_weaponList[data.getPrimaryIndex()].getWeapon());
                _primary.GetComponent<weapon>().createWeapon(data.getWeaponsLevels()[data.getPrimaryIndex()]);
            }
            if (data.getSecundaryIndex() != -1)
            {
                _secundary = Instantiate(_weaponList[data.getSecundaryIndex()].getWeapon());
                _secundary.GetComponent<weapon>().createWeapon(data.getWeaponsLevels()[data.getSecundaryIndex()]);
                
            }
        }
    }

    public List<weaponSlot> getWeaponList()
    {
        return _weaponList;
    }

    public void levelUp(int id)
    {
        _weaponList[id].getWeapon().GetComponent<weapon>().addLevel();
    }

    public GameObject getWeaponAt(int index)
    {
        return _weaponList[index].getWeapon();
    }

    public int getIndexOf(slotData slot)
    {
        weaponSlot data = _weaponList.Find(item => item.getID() == slot.getID());
        return _weaponList.IndexOf(data);
    }

    public int getPrimaryIndex()
    {
        return _weaponList.Find(weapon => weapon.getHand() == handEnum.primary).getID();
    }
    
    public void equipWeapon()
    {
        _pressedItem = EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>();
        if (_pressedItem != null)
        {
            if (_pressedItem.getTipo() == itemTypeEnum.weapon)
            {
                equippedWeaponsData data = saveSystem.loadWeaponsState();

                /*
                if (data == null)
                {
                    for (int i = 0; i < _weaponList.Count; ++i)
                    {
                        _weaponsLevels.Add(1);
                    }
                    data = new equippedWeaponsData(-1, -1, _weaponsLevels);
                }*/

                _pressedIndex = getIndexOf(_pressedItem);

                _clickedWeapon = _weaponList.Find(weapon => weapon.getID() == _pressedItem.getID());
                if (_clickedWeapon.getHand() == handEnum.primary)
                {
                    if (_primary != null)
                    {
                        destroySkills(_primary);
                        Destroy(_primary);
                    }
                    _instantiatedPrefab = Instantiate(_weaponList[_pressedIndex].getWeapon());
                    _instantiatedPrefab.GetComponent<weapon>().createWeapon(data.getWeaponsLevels()[_pressedIndex]);
                    _primary = _instantiatedPrefab;

                    data.setPrimaryIndex(_weaponList.IndexOf(_clickedWeapon));
                }
                _clickedWeapon = _weaponList.Find(weapon => weapon.getID() == _pressedItem.getID());
                if (_clickedWeapon.getHand() == handEnum.secundary)
                {
                    if (_secundary != null)
                    {
                        destroySkills(_secundary);
                        Destroy(_secundary);
                    }

                    _instantiatedPrefab = Instantiate(_weaponList[_pressedIndex].getWeapon());
                    _instantiatedPrefab.GetComponent<weapon>().createWeapon(data.getWeaponsLevels()[_pressedIndex]);
                    _secundary = _instantiatedPrefab;
                    data.setSecundaryIndex(_weaponList.IndexOf(_clickedWeapon));
                }

                saveSystem.saveWeaponsState(data.getPrimaryIndex(), data.getSecundaryIndex(), data.getWeaponsLevels());
            }
        }
    }

    public int getMaterialQuantity(int index)
    {
        return getWeaponList()[index].getWeapon().GetComponent<weapon>().getQuantites()[(saveSystem.loadWeaponsState().getWeaponsLevels()[index] - 1)];
    }

    public Sprite getMaterialSprite(int index)
    {
        return getWeaponList()[index].getWeapon().GetComponent<weapon>().getListOfMaterials()[(saveSystem.loadWeaponsState().getWeaponsLevels()[index] - 1)].getItemData().getIcon();
    }

    public int getQuantityOf(int index)
    {
        return getWeaponList()[index].getWeapon().GetComponent<weapon>().getQuantites()[(saveSystem.loadWeaponsState().getWeaponsLevels()[index] - 1)];
    }

    public void destroySkills(GameObject weapon)
    {

        if (weapon.GetComponent<weapon>().getWeaponSkills() != null)
        {
            Debug.Log(weapon.GetComponent<weapon>().getWeaponSkills().Count);
            for (int i = 0; i < weapon.GetComponent<weapon>().getWeaponSkills().Count; ++i)
            {
                Destroy(weapon.GetComponent<weapon>().getWeaponSkills()[i]);
                weapon.GetComponent<weapon>().getWeaponSkills().Clear();
            }
        }
    }

}
