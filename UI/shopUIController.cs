using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// shopUIController es una clase que se encarga de manejar la UI de las tiendas.
/// </summary>
public class shopUIController : MonoBehaviour
{
    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    [SerializeField] private Image _itemSprite;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _itemName;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _itemDescription;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _quantityLeft;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _itemPrice;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    [SerializeField] private RectTransform _prefabHolder;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    [SerializeField] private GridLayoutGroup _grid;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    [SerializeField] private GameObject _itemPrefab;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    private List<GameObject> _instantiatedPrefabs;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    private List<shopItem> _shopItemsList;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    private GameObject _formerEventSystemSelected = null;

    /// <summary>
    /// El sprite del objeto a comprar en el panel derecho.
    /// </summary>
    private shopData _data;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="shopItemsList"></param>
    public void initializeUI(List<shopItem> shopItemsList)
    {
        _data = saveSystem.loadShopData();

        _shopItemsList = shopItemsList;
        _instantiatedPrefabs = new List<GameObject>();

        createShopInventory();

        calculateNavigation();
        EventSystem.current.SetSelectedGameObject(_instantiatedPrefabs[0].GetComponent<shopItemSlotLogic>().getSlotButton().gameObject);
    }

    /// <summary>
    /// Método auxiliar para crear el inventario de la tienda.
    /// </summary>
    private void createShopInventory()
    {
        //Recorremos los objetos de la tienda
        for (int i = 0; i < _shopItemsList.Count; i++)
        {
            //Instanciamos y sacamos variables comunes
            GameObject newSlot = Instantiate(_itemPrefab);
            newSlot.GetComponent<shopItemSlotLogic>().setSlotID(i);

            int itemPrice = _shopItemsList[i].getPrice();
            int itemIndex = i;
            int shopID = _data.getShopID(SceneManager.GetActiveScene().buildIndex);
            //Si es un objeto
            if (_shopItemsList[i].getItem().GetComponent<generalItem>() != null)
            {
                //Asignamos variables a componentes de la UI
                newSlot.GetComponent<shopItemSlotLogic>().setSprite(_shopItemsList[i].getItem().GetComponent<generalItem>().getIcon());
                generalItem item = _shopItemsList[i].getItem().GetComponent<generalItem>();
                int itemID = item.getID();
                //En caso de que podamos comprar asignamos esa función al botón
                if (config.getPlayer().GetComponent<combatController>().getSouls() >= _shopItemsList[i].getPrice() && _shopItemsList[i].getQuantity() > 0)
                {
                    lootItem newItem = new lootItem(_shopItemsList[i].getItem().GetComponent<generalItem>().getData().getData(), 1);
                    newSlot.GetComponent<shopItemSlotLogic>().getSlotButton().onClick.AddListener(() => {
                        buyItem(shopID, itemIndex, itemPrice);
                        config.getInventory().GetComponent<inventoryManager>().addItemToInventory(newItem);
                        config.getPlayer().GetComponent<equippedInventory>().modifyIfBuy(itemID);
                    });
                }
            }
            else if (_shopItemsList[i].getItem().GetComponent<skill>() != null) //Si es una habilidad
            {
                //Asignamos variables a componentes de la UI
                newSlot.GetComponent<shopItemSlotLogic>().setSprite(_shopItemsList[i].getItem().GetComponent<skill>().getSkillSprite());
                skill selectedSkill = _shopItemsList[i].getItem().GetComponent<skill>();
                unlockedSkillsData data = saveSystem.loadSkillsState();
                //En caso de que podamos comprar asignamos esa función al botón
                if (config.getPlayer().GetComponent<combatController>().getSouls() >= _shopItemsList[i].getPrice() && _shopItemsList[i].getQuantity() > 0)
                {
                    newSlot.GetComponent<shopItemSlotLogic>().getSlotButton().onClick.AddListener(() => {
                        buyItem(shopID, itemIndex, itemPrice);
                        sceneSkillsState newSkill = new sceneSkillsState(-1, selectedSkill);
                        if (data == null)
                        {
                            List<sceneSkillsState> newData = new List<sceneSkillsState>();
                            newData.Add(newSkill);
                            saveSystem.saveSkillsState(newData);
                        }
                        else
                        {
                            data.getUnlockedSkills().Add(newSkill);
                            saveSystem.saveSkillsState(data.getUnlockedSkills());
                        }
                    });
                }
            }

            //Metemos el slot en la UI
            newSlot.transform.SetParent(_prefabHolder, false);
            _instantiatedPrefabs.Add(newSlot);
        }
    }

    /// <summary>
    /// Método auxiliar para asignar la navegación entre botones de la UI.
    /// </summary>
    public void calculateNavigation()
    {
        int counter = 0;
        Navigation currentSlotNavigation;
        Navigation modeNavigation = new Navigation();

        //Recorremos los slots instanciados
        for (int index = 0; index < _instantiatedPrefabs.Count; ++index)
        {
            modeNavigation.mode = Navigation.Mode.None;
            _instantiatedPrefabs[index].GetComponent<shopItemSlotLogic>().getSlotButton().navigation = modeNavigation;
            modeNavigation.mode = Navigation.Mode.Explicit;
            _instantiatedPrefabs[index].GetComponent<shopItemSlotLogic>().getSlotButton().navigation = modeNavigation;
            currentSlotNavigation = _instantiatedPrefabs[index].GetComponent<shopItemSlotLogic>().getSlotButton().navigation;

            //Si estamos en el primero de la fila y hay más
            if (index == 0 && _instantiatedPrefabs.Count > 1)
            {
                currentSlotNavigation.selectOnRight = _instantiatedPrefabs[index + 1].GetComponent<shopItemSlotLogic>().getSlotButton();
                counter++;
            }
            else
            {
                if (index == _instantiatedPrefabs.Count - 1) //Si es el último
                {
                    if (index % _grid.constraintCount != 0) //Si no es el primero de la fila
                    {
                        currentSlotNavigation.selectOnLeft = _instantiatedPrefabs[index - 1].GetComponent<shopItemSlotLogic>().getSlotButton();
                    }
                }
                else //Si no es el último
                {
                    if (index % _grid.constraintCount != 0) //Si no es el primero de la fila
                    {
                        currentSlotNavigation.selectOnLeft = _instantiatedPrefabs[index - 1].GetComponent<shopItemSlotLogic>().getSlotButton();
                    }
                    if (counter < (_grid.constraintCount - 1)) //Si no es el último de la fila
                    {
                        currentSlotNavigation.selectOnRight = _instantiatedPrefabs[index + 1].GetComponent<shopItemSlotLogic>().getSlotButton();
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }
            //Si no es la fila de arriba
            if (index - _grid.constraintCount >= 0)
            {
                currentSlotNavigation.selectOnUp = _instantiatedPrefabs[index - _grid.constraintCount].GetComponent<shopItemSlotLogic>().getSlotButton();
            }

            //Si no es la fila de abajo
            if (index + _grid.constraintCount < _instantiatedPrefabs.Count)
            {
                currentSlotNavigation.selectOnDown = _instantiatedPrefabs[index + _grid.constraintCount].GetComponent<shopItemSlotLogic>().getSlotButton();
            }
            _instantiatedPrefabs[index].GetComponent<shopItemSlotLogic>().getSlotButton().navigation = currentSlotNavigation;

        }
    }

    /// <summary>
    /// Método usado para limpiar la UI de la tienda para las siguientes veces que entremos.
    /// </summary>
    public void setUIOff()
    {
        for (int i = 0; i < _instantiatedPrefabs.Count; i++)
        {
            Destroy(_instantiatedPrefabs[i]);
        }
        _instantiatedPrefabs.Clear();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_shopItemsList"/>.
    /// </summary>
    /// <returns>Una lista de <see cref="shopItem"/> que contiene los objetos de los que dispone la tienda.</returns>
    public List<shopItem> getShopItemsList()
    {
        return _shopItemsList;
    }

    /// <summary>
    /// Método para comprar el objeto.
    /// </summary>
    /// <param name="shopID">El ID de la tienda en la que estamos comprando.</param>
    /// <param name="itemIndex">El ID del objeto que estamos comprando.</param>
    /// <param name="price">El precio del objeto que estamos comprando.</param>
    private void buyItem(int shopID, int itemIndex, int price)
    {
        config.getPlayer().GetComponent<combatController>().useSouls(price);
        changeInternalInformation();
        sceneShopData currentShop = _data.getData().Find(shop => shop.getShopID() == shopID);
        currentShop.buyItem(itemIndex);
        saveSystem.saveShopData(_data.getData());
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// Se encarga de modificar el panel de información si cambiamos de objeto o de eliminar la función de compra del botón si nos hemos quedado sin stock en la tienda
    /// de ese objeto.
    /// </summary>
    private void Update()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        if (currentSelected != _formerEventSystemSelected)
        {
            _formerEventSystemSelected = currentSelected;
            changeInformationPanel();
        }
        
        if (_shopItemsList[_formerEventSystemSelected.transform.gameObject.transform.parent.GetComponent<shopItemSlotLogic>().getSlotID()].getQuantity() == 0)
        {
            EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<shopItemSlotLogic>().getSlotButton().onClick.RemoveAllListeners();
        }
    }

    /// <summary>
    /// Método que modifica la información del objeto seleccionado en el panel derecho.
    /// </summary>
    private void changeInformationPanel()
    {
        //Obtenemos el objeto seleccionado
        shopItem selectedItem = _shopItemsList[_formerEventSystemSelected.transform.gameObject.transform.parent.GetComponent<shopItemSlotLogic>().getSlotID()];

        //Si es un item
        if (selectedItem.getItem().GetComponent<generalItem>() != null)
        {
            _itemSprite.sprite = selectedItem.getItem().GetComponent<generalItem>().getIcon();
            _itemName.text = selectedItem.getItem().GetComponent<generalItem>().getName();
            _itemDescription.text = selectedItem.getItem().GetComponent<generalItem>().getDesc();
        }
        else if (selectedItem.getItem().GetComponent<skill>() != null) //Si es una habilidad
        {
            _itemSprite.sprite = selectedItem.getItem().GetComponent<skill>().getSkillSprite();
            _itemName.text = selectedItem.getItem().GetComponent<skill>().getSkillName();
            _itemDescription.text = selectedItem.getItem().GetComponent<skill>().getSkillDescription();
        }
        //Modificamos el precio y la cantidad
        _itemPrice.text = selectedItem.getPrice().ToString();
        _quantityLeft.text = selectedItem.getQuantity().ToString();

        //Cambiamos el color según si podemos o no comprar para dar feedback visual
        if (config.getPlayer().GetComponent<combatController>().getSouls() < selectedItem.getPrice())
        {
            _itemPrice.color = Color.red;
        }
        else
        {
            _itemPrice.color = Color.black;
        }
    }

    /// <summary>
    /// Método que se encarga de comprar internamente el objeto y modifica la cantidad.
    /// </summary>
    private void changeInternalInformation()
    {
        shopItem selectedItem = _shopItemsList[_formerEventSystemSelected.transform.gameObject.transform.parent.GetComponent<shopItemSlotLogic>().getSlotID()];
        selectedItem.buyItem();
        _quantityLeft.text = selectedItem.getQuantity().ToString();
    }
}
