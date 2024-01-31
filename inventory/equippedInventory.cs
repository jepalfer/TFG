using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equippedInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _equippedItems;
    [SerializeField] private List<GameObject> _allItems;
    private equippedObjectData _data;

    public List<GameObject> getEquippedItems()
    {
        return _equippedItems;
    }

    private void Awake()
    {
        _data = saveSystem.loadEquippedObjectsData();

        if (_data != null)
        {
            for (int i = 0; i < _data.getData().Count; i++)
            {
                if (_data.getData()[i] != null)
                {
                    _equippedItems[i] = Instantiate(_allItems.Find(item => item.GetComponent<generalItem>().getID() == _data.getData()[i].getItemID()));
                }
            }
        }
    }

    public void checkIfEquipped(int index, int itemID)
    {
        int searchedIndex = -1;
        for (int i = 0; i < _equippedItems.Count; ++i)
        {
            if (_equippedItems[i] != null)
            {
                if (_equippedItems[i].GetComponent<generalItem>().getID() == itemID)
                {
                    searchedIndex = i;
                    break;
                }
            }
        }
        if (searchedIndex != -1)
        {
            _data = saveSystem.loadEquippedObjectsData();
            if (_data != null)
            {
                _data.setEquippedObject(searchedIndex, null);
                saveSystem.saveEquippedObjectsData(_data.getData());
            }
            Destroy(_equippedItems[searchedIndex]);
            _equippedItems[searchedIndex] = null;
        }
    }

    public void equipObject(int index, int itemID)
    {
        checkIfEquipped(index, itemID);
        _equippedItems[index] = Instantiate(_allItems.Find(item => item.GetComponent<generalItem>().getID() == itemID));

        _data = saveSystem.loadEquippedObjectsData();

        if (_data == null) //Primer objeto que equipamos
        {
            List<newEquippedObjectData> aux = new List<newEquippedObjectData>();
            for (int i = 0; i < _equippedItems.Count; i++)
            {
                aux.Add(null);
            }

            aux[index] = new newEquippedObjectData(itemID);
            saveSystem.saveEquippedObjectsData(aux);
        }
        else
        {
            _data.setEquippedObject(index, new newEquippedObjectData(itemID));
            saveSystem.saveEquippedObjectsData(_data.getData());
        }
        UIConfig.getController().getSprite().enabled = true;
        UIConfig.getController().getText().enabled = true;
        UIConfig.getController().setSprite(_equippedItems[index].GetComponent<generalItem>().getIcon());
        UIConfig.getController().setText(_equippedItems[index].GetComponent<generalItem>().getName());
    }

    private void Update()
    {
        if (_equippedItems.Find(item => item != null) != null)
        {
            if (!UIController.getIsAdquiringSkills() && !UIController.getIsEquippingObject() && !UIController.getIsPaused() && !UIController.getIsInEquipUI() &&
                !UIController.getIsInInventory() && !UIController.getIsLevelingUp() && !UIController.getIsLevelingUpWeapon() && !bonfireBehaviour.getIsInBonfireMenu())
            {
                //Controlar el desplazamiento en la lista
            }
        }
    }
}
