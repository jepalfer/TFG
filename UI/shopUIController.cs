using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class shopUIController : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    [SerializeField] private TextMeshProUGUI _quantityLeft;
    [SerializeField] private TextMeshProUGUI _itemPrice;
    [SerializeField] private RectTransform _prefabHolder;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private GameObject _itemPrefab;

    private List<GameObject> _instantiatedPrefabs;
    private List<shopItem> _shopItemsList;
    private GameObject _formerEventSystemSelected = null;
    public void initializeUI(List<shopItem> shopItemsList)
    {
        _shopItemsList = shopItemsList;
        _instantiatedPrefabs = new List<GameObject>();

        for (int i = 0; i < shopItemsList.Count; i++)
        {
            GameObject newSlot = Instantiate(_itemPrefab);
            newSlot.GetComponent<shopItemSlotLogic>().setSlotID(i);
            newSlot.GetComponent<shopItemSlotLogic>().setSprite(_shopItemsList[i].getItem().GetComponent<generalItem>().getIcon());

            if (_shopItemsList[i].getItem().GetComponent<generalItem>() != null)
            {
                if (config.getPlayer().GetComponent<combatController>().getSouls() >= _shopItemsList[i].getPrice() && _shopItemsList[i].getQuantity() > 0)
                {
                    lootItem newItem = new lootItem(_shopItemsList[i].getItem().GetComponent<generalItem>().getData().getData(), 1);
                    int itemPrice = _shopItemsList[i].getPrice();
                    newSlot.GetComponent<shopItemSlotLogic>().getSlotButton().onClick.AddListener(() => {
                        Debug.Log(newItem.getID());
                        config.getInventory().GetComponent<inventoryManager>().addItemToInventory(newItem);
                        config.getPlayer().GetComponent<combatController>().useSouls(itemPrice);
                        changeInternalInformation();
                    });
                }
            }
            /*else if (_shopItemsList[i].getItem().GetComponent<skill>() != null)
            {
                if (config.getPlayer().GetComponent<combatController>().getSouls() >= _shopItemsList[i].getPrice() && _shopItemsList[i].getQuantity() > 0)
                {
                    newSlot.GetComponent<shopItemSlotLogic>().getSlotButton().onClick.AddListener(() => {
                    });
                }
            }*/
            newSlot.transform.SetParent(_prefabHolder, false);
            _instantiatedPrefabs.Add(newSlot);
        }

        calculateNavigation();
        EventSystem.current.SetSelectedGameObject(_instantiatedPrefabs[0].GetComponent<shopItemSlotLogic>().getSlotButton().gameObject);
    }

    public void calculateNavigation()
    {
        int counter = 0;
        Navigation current_slot_navigation;
        Navigation modeNavigation = new Navigation();
        for (int index = 0; index < _instantiatedPrefabs.Count; ++index)
        {
            modeNavigation.mode = Navigation.Mode.None;
            _instantiatedPrefabs[index].GetComponent<shopItemSlotLogic>().getSlotButton().navigation = modeNavigation;
            modeNavigation.mode = Navigation.Mode.Explicit;
            _instantiatedPrefabs[index].GetComponent<shopItemSlotLogic>().getSlotButton().navigation = modeNavigation;
            current_slot_navigation = _instantiatedPrefabs[index].GetComponent<shopItemSlotLogic>().getSlotButton().navigation;
            if (index == 0 && _instantiatedPrefabs.Count > 1)
            {
                current_slot_navigation.selectOnRight = _instantiatedPrefabs[index + 1].GetComponent<shopItemSlotLogic>().getSlotButton();
                counter++;
            }
            else
            {
                if (index == _instantiatedPrefabs.Count - 1)
                {
                    if (index % _grid.constraintCount != 0)
                    {
                        current_slot_navigation.selectOnLeft = _instantiatedPrefabs[index - 1].GetComponent<shopItemSlotLogic>().getSlotButton();
                    }
                }
                else
                {
                    if (index % _grid.constraintCount != 0)
                    {
                        current_slot_navigation.selectOnLeft = _instantiatedPrefabs[index - 1].GetComponent<shopItemSlotLogic>().getSlotButton();
                    }
                    if (counter < (_grid.constraintCount - 1))
                    {
                        current_slot_navigation.selectOnRight = _instantiatedPrefabs[index + 1].GetComponent<shopItemSlotLogic>().getSlotButton();
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }

            if (index - _grid.constraintCount >= 0)
            {
                current_slot_navigation.selectOnUp = _instantiatedPrefabs[index - _grid.constraintCount].GetComponent<shopItemSlotLogic>().getSlotButton();
            }

            if (index + _grid.constraintCount < _instantiatedPrefabs.Count)
            {
                current_slot_navigation.selectOnDown = _instantiatedPrefabs[index + _grid.constraintCount].GetComponent<shopItemSlotLogic>().getSlotButton();
            }
            _instantiatedPrefabs[index].GetComponent<shopItemSlotLogic>().getSlotButton().navigation = current_slot_navigation;

        }
    }

    public void setUIOff()
    {
        for (int i = 0; i < _instantiatedPrefabs.Count; i++)
        {
            Destroy(_instantiatedPrefabs[i]);
        }
        _instantiatedPrefabs.Clear();
    }

    public List<shopItem> getShopItemsList()
    {
        return _shopItemsList;
    }

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

    private void changeInformationPanel()
    {
        shopItem selectedItem = _shopItemsList[_formerEventSystemSelected.transform.gameObject.transform.parent.GetComponent<shopItemSlotLogic>().getSlotID()];

        _itemSprite.sprite = selectedItem.getItem().GetComponent<generalItem>().getIcon();
        _itemName.text = selectedItem.getItem().GetComponent<generalItem>().getName();
        _itemDescription.text = selectedItem.getItem().GetComponent<generalItem>().getDesc();
        _itemPrice.text = selectedItem.getPrice().ToString();
        _quantityLeft.text = selectedItem.getQuantity().ToString();
    }

    private void changeInternalInformation()
    {
        shopItem selectedItem = _shopItemsList[_formerEventSystemSelected.transform.gameObject.transform.parent.GetComponent<shopItemSlotLogic>().getSlotID()];
        selectedItem.buyItem();
        _quantityLeft.text = selectedItem.getQuantity().ToString();
    }
}
