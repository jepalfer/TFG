using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// equippedInventory es una clase que permite manejar la l�gica de los objetos equipados.
/// </summary>
public class equippedInventory : MonoBehaviour
{
    /// <summary>
    /// Lista con uan referencia a los objetos equipados.
    /// </summary>
    [SerializeField] private List<GameObject> _equippedItems;

    /// <summary>
    /// Lista con una referencia a todos los objetos del juego para poder instanciarlos seg�n corresponda.
    /// </summary>
    [SerializeField] private List<GameObject> _allItems;

    /// <summary>
    /// Datos de los objetos equipados serializados.
    /// </summary>
    private equippedObjectData _data;

    /// <summary>
    /// �ndice de la posici�n en la que nos encontramos en <see cref="_equippedItems"/>.
    /// </summary>
    private int _indexInEquipped;

    /// <summary>
    /// Getter que devuelve <see cref="_equippedItems"/>.
    /// </summary>
    /// <returns></returns>
    public List<GameObject> getEquippedItems()
    {
        return _equippedItems;
    }

    /// <summary>
    /// M�todo que se ejecuta al iniciar el script.
    /// Instancia los objetos equipados seg�n la informaci�n serializada.
    /// </summary>
    private void Awake()
    {
        //Obtenemos los datos serializados
        _indexInEquipped = -1;
        _data = saveSystem.loadEquippedObjectsData();

        //Si hay datos
        if (_data != null)
        {
            //Instanciamos objetos
            for (int i = 0; i < _data.getData().Count; i++)
            {
                if (_data.getData()[i] != null)
                {
                    _equippedItems[i] = Instantiate(_allItems.Find(item => item.GetComponent<generalItem>().getID() == _data.getData()[i].getItemID()));
                }
            }

            //Cambiamos el �ndice en el que estamos
            _indexInEquipped = _data.getIndexInEquipped();
        }
    }

    /// <summary>
    /// M�todo que se llama al iniciar el script, despu�s de Awake.
    /// </summary>
    private void Start()
    {
        //Si tenemos un objeto equipado
        if (_indexInEquipped != -1)
        {
            //Activamos y modificamos la informaci�n en la UI
            changeSpritesMode(true);
            modifyEquippedObject(_indexInEquipped);
        }
        else
        {
            changeSpritesMode(false);
        }
    }

    /// <summary>
    /// M�todo auxliar que comprueba que ya tengamos equipado el objeto que queremos cambiar de slot.
    /// </summary>
    /// <param name="index">�ndice de <see cref="_equippedItems"/> en el que lo queremos equipar.</param>
    /// <param name="itemID">ID del objeto a equipar.</param>
    public void checkIfEquipped(int index, int itemID)
    {
        //Buscamos que el objeto no sea nulo y adem�s su ID coincida
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

        //Si el objeto est� instanciado
        if (searchedIndex != -1)
        {
            //Cargamos nuevamente datos
            _data = saveSystem.loadEquippedObjectsData();

            //Si ya se han guardado
            if (_data != null)
            {
                //Modificamos la lista
                _data.setEquippedObject(searchedIndex, null);
                if (_equippedItems[_data.getIndexInEquipped()].GetComponent<generalItem>().getID() != itemID)
                {
                    index = _data.getIndexInEquipped();
                }

                //Guardamos
                saveSystem.saveEquippedObjectsData(_data.getData(), index);
            }

            //Eliminamos el objeto anterior
            Destroy(_equippedItems[searchedIndex]);
            _equippedItems[searchedIndex] = null;
        }
    }
    /// <summary>
    /// M�todo que se encarga de la l�gica de equipar un objeto.
    /// </summary>
    /// <param name="index">�ndice de <see cref="_equippedItems"/> en el que lo queremos equipar.</param>
    /// <param name="itemID">ID del objeto a equipar.</param>
    public void equipObject(int index, int itemID)
    {
        //Comprobamos que est� equipado
        checkIfEquipped(index, itemID);

        //Instanciamos el objeto (equipamos)
        _equippedItems[index] = Instantiate(_allItems.Find(item => item.GetComponent<generalItem>().getID() == itemID));

        //Cargamos datos
        _data = saveSystem.loadEquippedObjectsData();

        if (_data == null) //Primer objeto que equipamos
        {
            //Creamos un objeto equipable y serializable
            List<newEquippedObjectData> aux = new List<newEquippedObjectData>();
            for (int i = 0; i < _equippedItems.Count; i++)
            {
                aux.Add(null);
            }

            //Equipamos, modificamos y guardamos
            aux[index] = new newEquippedObjectData(itemID);
            saveSystem.saveEquippedObjectsData(aux, index);
            changeSpritesMode(true);
            modifyEquippedObject(index);
        }
        else if (allEquippedAreNotNull() && (_data.getIndexInEquipped() != -1)) //Hay un objeto equipado 
        {
            //Equipamos, modificamos y guardamos
            _data.setEquippedObject(index, new newEquippedObjectData(itemID));
            int id = _data.getIndexInEquipped();
            saveSystem.saveEquippedObjectsData(_data.getData(), _data.getIndexInEquipped());
            modifyEquippedObject(id);
        }
        else //No hay objeto equipado
        {
            //Equipamos, modificamos y guardamos
            _data.setEquippedObject(index, new newEquippedObjectData(itemID));
            int id = index;
            saveSystem.saveEquippedObjectsData(_data.getData(), id);
            _data = saveSystem.loadEquippedObjectsData();
            changeSpritesMode(true);
            modifyEquippedObject(index);
        }
    }

    /// <summary>
    /// M�todo auxiliar que comprueba que haya un objeto equipado.
    /// </summary>
    /// <returns>Un booleano que representa si hay o no un objeto equipado.</returns>
    public bool allEquippedAreNotNull()
    {
        return _equippedItems.Find(item => item != null) != null;
    }

    /// <summary>
    /// M�todo auxiliar para activar o desactivar la informaci�n de la UI.
    /// </summary>
    /// <param name="mode">Booleano que indica si hay que activar o desactivar.</param>
    public void changeSpritesMode(bool mode)
    {

        UIConfig.getController().getSprite().enabled = mode;
        UIConfig.getController().getText().enabled = mode;
        UIConfig.getController().getQuantity().enabled = mode;
    }
    /// <summary>
    /// M�todo auxiliar para cambiar la informaci�n de la UI.
    /// </summary>
    /// <param name="index">�ndice del objeto del que se obtiene la informaci�n.</param>
    public void modifyEquippedObject(int index)
    {
        UIConfig.getController().setSprite(_equippedItems[index].GetComponent<generalItem>().getIcon());
        UIConfig.getController().setText(_equippedItems[index].GetComponent<generalItem>().getName());
        UIConfig.getController().setQuantity(config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getID() == _equippedItems[index].GetComponent<generalItem>().getID()).getQuantity());

    }

    /// <summary>
    /// M�todo que se ejecuta cada frame para actualizar la l�gica.
    /// </summary>
    private void Update()
    {
        //Comprobamos que haya objeto equipado
        if (allEquippedAreNotNull())
        {
            //Si no estamos en ning�n men�
            if (!UIController.getIsInAdquireSkillUI() && !UIController.getIsEquippingObjectUI() && !UIController.getIsInPauseUI() && !UIController.getIsInEquippingSkillUI() &&
                !UIController.getIsInInventoryUI() && !UIController.getIsInLevelUpUI() && !UIController.getIsInLevelUpWeaponUI() && !UIController.getIsInShopUI() && 
                !bonfireBehaviour.getIsInBonfireMenu())
            {
                //Controlamos el desplazamiento en la lista
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
                //Utilizamos el objeto
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
