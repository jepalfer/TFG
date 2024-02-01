using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equippedInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _equippedItems;
    [SerializeField] private List<GameObject> _allItems;
    private equippedObjectData _data;
    private int _indexInEquipped;

    public List<GameObject> getEquippedItems()
    {
        return _equippedItems;
    }

    private void Awake()
    {
        _indexInEquipped = -1;
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
            _indexInEquipped = _data.getIndexInEquipped();
        }
    }

    private void Start()
    {
        if (_indexInEquipped != -1)
        {
            changeSpritesMode(true);
            modifyEquippedObject(_indexInEquipped);
        }
        else
        {
            changeSpritesMode(false);
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

                if (_equippedItems[_data.getIndexInEquipped()].GetComponent<generalItem>().getID() != itemID)
                {
                    index = _data.getIndexInEquipped();
                }

                saveSystem.saveEquippedObjectsData(_data.getData(), index);
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
            saveSystem.saveEquippedObjectsData(aux, index);
            changeSpritesMode(true);
            modifyEquippedObject(index);
        }
        else if (allEquippedAreNull() && (_data.getIndexInEquipped() != -1))
        {
            _data.setEquippedObject(index, new newEquippedObjectData(itemID));
            int id = _data.getIndexInEquipped();
            saveSystem.saveEquippedObjectsData(_data.getData(), _data.getIndexInEquipped());
        }
        else
        {
            _data.setEquippedObject(index, new newEquippedObjectData(itemID));
            int id = index;
            
            saveSystem.saveEquippedObjectsData(_data.getData(), id);
            _data = saveSystem.loadEquippedObjectsData();
            changeSpritesMode(true);
            modifyEquippedObject(index);
        }
    }

    public bool allEquippedAreNull()
    {
        return _equippedItems.Find(item => item != null) != null;
    }

    public void changeSpritesMode(bool mode)
    {

        UIConfig.getController().getSprite().enabled = mode;
        UIConfig.getController().getText().enabled = mode;
        UIConfig.getController().getQuantity().enabled = mode;
    }

    public void modifyEquippedObject(int index)
    {
        UIConfig.getController().setSprite(_equippedItems[index].GetComponent<generalItem>().getIcon());
        UIConfig.getController().setText(_equippedItems[index].GetComponent<generalItem>().getName());
        UIConfig.getController().setQuantity(config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getID() == _equippedItems[index].GetComponent<generalItem>().getID()).getQuantity());

    }

    private void Update()
    {
        if (_equippedItems.Find(item => item != null) != null)
        {
            if (!UIController.getIsAdquiringSkills() && !UIController.getIsEquippingObject() && !UIController.getIsPaused() && !UIController.getIsInEquipUI() &&
                !UIController.getIsInInventory() && !UIController.getIsLevelingUp() && !UIController.getIsLevelingUpWeapon() && !bonfireBehaviour.getIsInBonfireMenu())
            {
                //Controlar el desplazamiento en la lista
                if (inputManager.GetKeyDown(inputEnum.nextItem))
                {
                    for (int i = (saveSystem.loadEquippedObjectsData().getIndexInEquipped() + 1) % _equippedItems.Count; ; i++)
                    {
                        i = (i % 6);
                        if (_equippedItems[i] != null)
                        {
                            saveSystem.saveEquippedObjectsData(saveSystem.loadEquippedObjectsData().getData(), i);
                            modifyEquippedObject(i);
                            break;
                        }
                    }
                }
                else if (inputManager.GetKeyDown(inputEnum.previousItem))
                {
                    for (int i = (saveSystem.loadEquippedObjectsData().getIndexInEquipped() - 1) % _equippedItems.Count; ; i--)
                    {
                        if (i < 0)
                        {
                            i = 5;
                        }
                        if(_equippedItems[i] != null)
                        {
                            saveSystem.saveEquippedObjectsData(saveSystem.loadEquippedObjectsData().getData(), i);
                            modifyEquippedObject(i);
                            break;
                        }
                    }
                }
                else if (inputManager.GetKeyDown(inputEnum.useItem))
                {
                    _data = saveSystem.loadEquippedObjectsData();
                    lootItem inventoryItem = new lootItem(_equippedItems[_data.getIndexInEquipped()].GetComponent<generalItem>().getData().getData(), config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getID() == _equippedItems[_data.getIndexInEquipped()].GetComponent<generalItem>().getID()).getQuantity());
                    config.getInventory().GetComponent<inventoryManager>().removeItemFromInventory(inventoryItem, 1);

                    
                    if (inventoryItem.getQuantity() > 1)
                    {
                        modifyEquippedObject(_data.getIndexInEquipped());
                    }
                    else
                    {
                        _equippedItems[_data.getIndexInEquipped()] = null;
                        _data = saveSystem.loadEquippedObjectsData();
                        _data.setEquippedObject(_data.getIndexInEquipped(), null);
                        changeSpritesMode(false);
                        for (int i = (_data.getIndexInEquipped() + 1) % _equippedItems.Count; ;i++)
                        {
                            i = (i % 6);

                            if (i == _data.getIndexInEquipped())//No hay ningun objeto disponible
                            {
                                saveSystem.saveEquippedObjectsData(_data.getData(), -1);
                                break;
                            }

                            if (_equippedItems[i] != null)
                            {
                                saveSystem.saveEquippedObjectsData(_data.getData(), i);
                                modifyEquippedObject(i);
                                changeSpritesMode(true);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
