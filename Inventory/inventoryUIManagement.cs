using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class inventoryUIManagement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyText;
    [SerializeField] private TextMeshProUGUI _consumableText;
    [SerializeField] private TextMeshProUGUI _weaponText;
    [SerializeField] private TextMeshProUGUI _materialText;

    [SerializeField] private GameObject _keyPanel;
    [SerializeField] private GameObject _consumablePanel;
    [SerializeField] private GameObject _weaponPanel;
    [SerializeField] private GameObject _materialPanel;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _informationPanel;
    [SerializeField] private Transform _slotHolder;
    private GameObject _formerEventSystemSelected = null;
    private List<GameObject> _itemList = new List<GameObject>();
    private int _counter;
    private bool _isShowingInformation = false;

    private (GameObject, TextMeshProUGUI)[] _objects = new (GameObject, TextMeshProUGUI)[4];

    private void Start()
    {
        _objects[0] = (_keyPanel, _keyText);
        _objects[1] = (_consumablePanel, _consumableText);
        _objects[2] = (_weaponPanel, _weaponText);
        _objects[3] = (_materialPanel, _materialText);
    }

    private void Update()
    {
        if (UIController.getIsInInventory())
        {
            GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
            if (inputManager.getKeyDown(inputEnum.Previous.ToString()))
            {
                if (_counter > 0)
                {
                    _counter--;
                    if (_counter > 0)
                    {
                        _objects[_counter - 1].Item1.SetActive(false);
                        _objects[_counter - 1].Item2.color = Color.white;
                    }
                    _objects[_counter].Item1.SetActive(true);
                    _objects[_counter].Item2.color = Color.yellow;
                    destroyItems();
                    createInventory((itemType)_counter);

                    _objects[_counter + 1].Item1.SetActive(false);
                    _objects[_counter + 1].Item2.color = Color.white;
                }
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    setInformationPanelOff();
                }
                else
                {
                    if (_isShowingInformation)
                    {
                        changeInformationPanel();
                    }
                }
            }
            if (inputManager.getKeyDown(inputEnum.Next.ToString()))
            {
                if (_counter < 3)
                {
                    _counter++;
                    _objects[_counter - 1].Item1.SetActive(false);
                    _objects[_counter - 1].Item2.color = Color.white;

                    _objects[_counter].Item1.SetActive(true);
                    _objects[_counter].Item2.color = Color.yellow;
                    destroyItems();
                    createInventory((itemType)_counter);
                    if (_counter < 3)
                    {
                        _objects[_counter + 1].Item1.SetActive(false);
                        _objects[_counter + 1].Item2.color = Color.white;
                    }
                    if (EventSystem.current.currentSelectedGameObject == null)
                    {
                        setInformationPanelOff();
                    }
                    else
                    {
                        if (_isShowingInformation)
                        {
                            changeInformationPanel();
                        }
                    }
                }
            }

            if (EventSystem.current.currentSelectedGameObject != null)
            {
                if (inputManager.getKeyDown(inputEnum.Interact.ToString()))
                {
                    useInformationPanel();
                }
            }
            if (currentSelected != _formerEventSystemSelected)
            {
                _formerEventSystemSelected = currentSelected;
                changeInformationPanel();
            }

        }
    }

    
    public void setInformationPanelOff()
    {
        _isShowingInformation = false;
        _informationPanel.SetActive(false);
    }
    public void useInformationPanel()
    {
        _isShowingInformation = !_isShowingInformation;
        _informationPanel.SetActive(!_informationPanel.activeSelf);
        changeInformationPanel();
    }

    public void changeInformationPanel()
    {
        GetComponent<UIController>().setInventoryValue(EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getInventoryStock());
        GetComponent<UIController>().setBackUpValue(EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getBackUpStock());
        GetComponent<UIController>().setRenderImage(EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getRender());
        GetComponent<UIController>().setDescriptionText(EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getDescription());
    }

    public void setPanelsOff()
    {
        _keyPanel.SetActive(false);
        _keyText.color = Color.white;
        
        _consumablePanel.SetActive(false);
        _consumableText.color = Color.white;

        _materialPanel.SetActive(false);
        _materialText.color = Color.white;

        _weaponPanel.SetActive(false);
        _weaponText.color = Color.white;
    }

    public void setCounter(int count)
    {
        _counter = count;
    }

    public void initializePanels()
    {
        _objects[0].Item1.SetActive(true);
        _objects[0].Item2.color = Color.yellow;
        createInventory((itemType)0);
    }

    public void createInventory(itemType type)
    {
        EventSystem.current.SetSelectedGameObject(null);
        List<lootItem> inventoryQuery = config.getInventory().GetComponent<inventoryManager>().getInventory().FindAll(item => item.getItem().getInstance().getItemData().getTipo() == type);
        List<lootItem> backUpQuery = config.getInventory().GetComponent<inventoryManager>().getBackUp();
        for (int i = 0; i < inventoryQuery.Count; ++i)
        {
            GameObject newItem = Instantiate(_slotPrefab);

            //Debug.Log(newItem.GetComponent<slotData>().getTipo());
            /*if (newItem.GetComponent<slotData>().getTipo() == ItemType.weapon)
            {
                newItem.GetComponent<Button>().onClick.AddListener ( ()=> {
                    Config.getInventory().GetComponent<weaponInventoryManagement>().equipWeapon();
                });
            }
            */
            newItem.GetComponent<Image>().sprite = inventoryQuery[i].getIcon();
            newItem.GetComponent<slotData>().setInventoryStock(inventoryQuery[i].getQuantity());
            newItem.GetComponent<slotData>().setBackUpStock(0);
            newItem.GetComponent<slotData>().setLootItem(inventoryQuery[i]); 
            
            if (newItem.GetComponent<slotData>().getTipo() == itemType.weapon)
            {
                newItem.GetComponent<Button>().onClick.AddListener(() => {
                    config.getInventory().GetComponent<weaponInventoryManagement>().equipWeapon();
                });
            }
            /*
            newItem.GetComponent<slotData>().setDescription(inventoryQuery[i].getItem().getInstance().getItemData().getDesc());
            newItem.GetComponent<slotData>().setRender(inventoryQuery[i].getItem().getInstance().getItemData().getRender());*/
            if (backUpQuery.Find(itemBackUp => itemBackUp.getTipo() == type) != null)
            {
                newItem.GetComponent<slotData>().setBackUpStock(backUpQuery.Find(itemBackUp => itemBackUp.getInstance()).getQuantity());
            }
            //newItem.GetComponent<slotData>().setBackUpStock(backUpQuery.Find(itemBackUp => itemBackUp.getItem()).getQuantity());
            newItem.transform.SetParent(_slotHolder, false);
            _itemList.Add(newItem);
            /*if (i == 0)
            {
                EventSystem.current.SetSelectedGameObject(newItem.GetComponent<Button>().gameObject);
            }
            */
        }
        if (_itemList.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(_itemList[0]);
        }
        int counter = 0;
        Navigation current_slot_navigation;
        Navigation modeNavigation = new Navigation();
        for (int index = 0; index < _itemList.Count; ++index)
        {
            modeNavigation.mode = Navigation.Mode.None;
            _itemList[index].GetComponent<Button>().navigation = modeNavigation;
            modeNavigation.mode = Navigation.Mode.Explicit;
            _itemList[index].GetComponent<Button>().navigation = modeNavigation;
            current_slot_navigation = _itemList[index].GetComponent<Button>().navigation;
            if (index == 0 && _itemList.Count > 1)
            {
                current_slot_navigation.selectOnRight = _itemList[index + 1].GetComponent<Button>();
                counter++;
            }
            else
            {
                if (index == _itemList.Count - 1)
                {
                    if (index % 10 != 0)
                    {
                        current_slot_navigation.selectOnLeft = _itemList[index - 1].GetComponent<Button>();
                    }
                }
                else
                {
                    if (index % 10 != 0)
                    {
                        current_slot_navigation.selectOnLeft = _itemList[index - 1].GetComponent<Button>();
                    }
                    if (counter < 9)
                    {
                        current_slot_navigation.selectOnRight = _itemList[index + 1].GetComponent<Button>();
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }

            if (index - 10 >= 0)
            {
                current_slot_navigation.selectOnUp = _itemList[index - 10].GetComponent<Button>();
            }

            if (index + 10 < _itemList.Count)
            {
                current_slot_navigation.selectOnDown = _itemList[index + 10].GetComponent<Button>();
            }
            _itemList[index].GetComponent<Button>().navigation = current_slot_navigation;

        }

    }
    public Transform getSlotHolder()
    {
        return _slotHolder;
    }

    public GameObject getSlotPPrefab()
    {
        return _slotPrefab;
    }

    public void destroyItems()
    {
        foreach (GameObject item in _itemList)
        {
            Destroy(item);
        }
        _itemList.Clear();
    }
}
