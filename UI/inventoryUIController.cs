using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// inventoryUIController es una clase que se usa para modificar el contenido de la UI del inventario.
/// </summary>
public class inventoryUIController : MonoBehaviour
{
    /// <summary>
    /// Referencia al prefab que representa el objeto.
    /// </summary>
    [SerializeField] private GameObject _slotPrefab;

    /// <summary>
    /// Referencia al panel de información del objeto.
    /// </summary>
    [SerializeField] private GameObject _informationPanel;

    /// <summary>
    /// Referencia a la parte de la UI donde se instancian los prefabs si no se muestra la descipción.
    /// </summary>
    [SerializeField] private Transform _expandedSlotHolder;

    /// <summary>
    /// Referencia al grid donde se meten los objetos si no se muestra la descipción.
    /// </summary>
    [SerializeField] private GridLayoutGroup _expandedGrid;

    /// <summary>
    /// Referencia a la parte de la UI donde se instancian los prefabs si se muestra la descipción.
    /// </summary>
    [SerializeField] private Transform _descriptionSlotHolder;

    /// <summary>
    /// Referencia al grid donde se meten los objetos si se muestra la descipción.
    /// </summary>
    [SerializeField] private GridLayoutGroup _descriptionGrid;

    /// <summary>
    /// Referencia a la parte de la UI donde se instancian los prefabs.
    /// </summary>
    private Transform _slotHolder;

    /// <summary>
    /// Referencia al grid donde se meten los objetos.
    /// </summary>
    private GridLayoutGroup _grid;

    /// <summary>
    /// Referencia al último objeto seleccionado.
    /// </summary>
    private GameObject _formerEventSystemSelected = null;

    /// <summary>
    /// Lista que contiene los objetos instanciados.
    /// </summary>
    private List<GameObject> _itemList;

    /// <summary>
    /// Contador para claridad visual en la UI para saber en qué tipo de objeto nos encontramos.
    /// </summary>
    private int _counter;

    /// <summary>
    /// Booleano para saber si estamos o no mostrando información
    /// </summary>
    private bool _isShowingInformation = false;

    /// <summary>
    /// ID interno del objeto seleccionado para que al abrir la descripción no cambie el objeto.
    /// </summary>
    private int _selectedID;

    /// <summary>
    /// Referencia al campo de texto de <see cref="_informationPanel"/> que contiene el número de objetos en el inventario.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _inventoryValue;

    /// <summary>
    /// Referencia al campo de texto de <see cref="_informationPanel"/> que contiene el número de objetos en el backup.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _backupValue;

    /// <summary>
    /// Referencia al sprite del objeto que aparece en <see cref="_informationPanel"/>.
    /// </summary>
    [SerializeField] private GameObject _render;
    
    /// <summary>
    /// Referencia a la descripción del objeto que aparece en <see cref="_informationPanel"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _description;

    /// <summary>
    /// Referencia al nombre del objeto que aparece en <see cref="_informationPanel"/>.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _name;

    /// <summary>
    /// Referencia al icono de la imagen para navegar hacia la izquierda en tipos de objetos.
    /// </summary>
    [SerializeField] private Image _leftButtonImage;
    /// <summary>
    /// Referencia al icono de la imagen para navegar hacia la derecha en tipos de objetos.
    /// </summary>
    [SerializeField] private Image _rightButtonImage;

    /// <summary>
    /// Lista que contiene los nombres de los tipos de objeto para dar claridad visual a la UI
    /// </summary>
    [SerializeField] private List<TextMeshProUGUI> _topPanelTexts;

    /// <summary>
    /// Lista que contiene los objetos buscados del inventario.
    /// </summary>
    private List<lootItem> _inventoryQuery;

    /// <summary>
    /// Lista que contiene los objetos buscados del backup.
    /// </summary>
    private List<lootItem> _backupQuery;

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null && !UIController.getIsEquippingObjectUI())
        {
            Debug.Log("hola");
            _selectedID = EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getID();
        }
        //Comprobamos que no estemos equipando un objeto
        if (!UIController.getIsEquippingObjectUI())
        {
            GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
            //Controlamos la navegación por los tipos de objeto
            if (inputManager.GetKeyDown(inputEnum.previous))
            {
                if (_counter > 0)
                {
                    //Cambiamos la información que se ve en la UI
                    _counter--;
                    if (_counter > 0)
                    {
                        _topPanelTexts[_counter - 1].color = Color.white;
                    }
                    _topPanelTexts[_counter].color = Color.yellow;
                    destroyItems();
                    createInventory((itemTypeEnum)_counter);

                    _topPanelTexts[_counter + 1].color = Color.white;
                }
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    setInformationPanelOff();
                }
                else
                {
                    //Modificamos la información para que sea coherente
                    if (_isShowingInformation)
                    {
                        changeInformationPanel();
                    }
                }
            }
            if (inputManager.GetKeyDown(inputEnum.next))
            {
                if (_counter < 3)
                {
                    //Cambiamos la información que se ve en la UI
                    _counter++;
                    _topPanelTexts[_counter - 1].color = Color.white;

                    _topPanelTexts[_counter].color = Color.yellow;
                    destroyItems();
                    createInventory((itemTypeEnum)_counter);
                    if (_counter < 3)
                    {
                        _topPanelTexts[_counter + 1].color = Color.white;
                    }
                    if (EventSystem.current.currentSelectedGameObject == null)
                    {
                        setInformationPanelOff();
                    }
                    else
                    {
                        //Modificamos la información para que sea coherente
                        if (_isShowingInformation)
                        {
                            changeInformationPanel();
                        }
                    }
                }
            }
            //Mostramos/modificamos la información del objeto
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                if (inputManager.GetKeyDown(inputEnum.interact))
                {
                    useInformationPanel();
                }
            }
            if (currentSelected != _formerEventSystemSelected)
            {
                if (_formerEventSystemSelected != null)
                {
                    _formerEventSystemSelected.GetComponent<slotData>().getOverlayImage().GetComponent<Image>().enabled = false;
                }
                _formerEventSystemSelected = currentSelected;
                _formerEventSystemSelected.GetComponent<slotData>().getOverlayImage().GetComponent<Image>().enabled = true;
                changeInformationPanel();
            }

        }
    }

    /// <summary>
    /// Método auxiliar que oculta el panel de información.
    /// </summary>
    public void setInformationPanelOff()
    {
        _isShowingInformation = false;
        _informationPanel.SetActive(false);
    }

    /// <summary>
    /// Método auxiliar que muestra el panel de información y lo modifica usando <see cref="changeInformationPanel()"/>.
    /// </summary>
    public void useInformationPanel()
    {
        _isShowingInformation = !_isShowingInformation;
        _informationPanel.SetActive(!_informationPanel.activeSelf);
        changeInformationPanel();

        if (_isShowingInformation)
        {
            _slotHolder = _descriptionSlotHolder;
            _grid = _descriptionGrid;
        }
        else
        {
            _slotHolder = _expandedSlotHolder;
            _grid = _expandedGrid;
        }
        destroyItems();
        createInventory((itemTypeEnum)_counter);
    }

    /// <summary>
    /// Método auxiliar que cambia la información del panel de información.
    /// </summary>
    public void changeInformationPanel()
    {
        setInventoryValue(EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getInventoryStock());
        setBackUpValue(EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getBackUpStock());
        setRenderImage(EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getRender());
        setDescriptionText(EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getDescription());
        setItemName(EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>().getName());
    }

    /// <summary>
    /// Método para poner la UI como estaba antes de inicializarse.
    /// </summary>
    public void setUIOff()
    {
        for (int i = 0; i < _topPanelTexts.Count; ++i)
        {
            _topPanelTexts[i].color = Color.white;
        }
    }

    /// <summary>
    /// Setter que modifica <see cref="_counter"/>.
    /// </summary>
    /// <param name="count">El valor a asignar.</param>
    public void setCounter(int count)
    {
        _counter = count;
    }

    /// <summary>
    /// Método que inicializa el primer inventario.
    /// </summary>
    public void initializeUI()
    {
        _selectedID = -1;
        _itemList = new List<GameObject>();
        _counter = 0;
        _topPanelTexts[0].color = Color.yellow;
        _slotHolder = _expandedSlotHolder;
        _grid = _expandedGrid;
        createInventory((itemTypeEnum)0);
    }

    /// <summary>
    /// Método que crea la UI de inventario mostrando los objetos que corresponda al tipo que se pasa por parámetro.
    /// </summary>
    /// <param name="type">El tipo de objeto a mostrar.</param>
    public void createInventory(itemTypeEnum type)
    {
        //En el caso de que sea un objeto recargable ya que es un consumible
        itemTypeEnum secondType = type;
        if (type == itemTypeEnum.consumable)
        {
            secondType = itemTypeEnum.refillable;
        }
       
        EventSystem.current.SetSelectedGameObject(null);

        //Obtenemos la información del inventario correspondiente al tipo
        //List<lootItem> inventoryQuery = config.getInventory().GetComponent<inventoryManager>().getInventory().FindAll(item => item.getItem().getInstance().getItemData().getTipo() == type || item.getItem().getInstance().getItemData().getTipo() == secondType);
        //List<lootItem> backUpQuery = config.getInventory().GetComponent<inventoryManager>().getBackUp();

        _inventoryQuery = config.getInventory().GetComponent<inventoryManager>().getInventory().FindAll(item => item.getItem().getInstance().getItemData().getTipo() == type || item.getItem().getInstance().getItemData().getTipo() == secondType);
        _backupQuery = config.getInventory().GetComponent<inventoryManager>().getBackUp();

        _inventoryQuery.Sort((item1, item2) => item1.getID().CompareTo(item2.getID()));
        GameObject selectFormer = null;
        //Recorremos el inventario
        for (int i = 0; i < _inventoryQuery.Count; ++i)
        {
            createItem(_inventoryQuery[i], type);
            if (_inventoryQuery[i].getID() == _selectedID)
            {
                selectFormer = _itemList[i];
            }
        }
        if ((_selectedID == -1 || selectFormer == null) && _itemList.Count > 0)
        {
            Debug.Log(_selectedID + "no entro");
            EventSystem.current.SetSelectedGameObject(_itemList[0]);
        }
        else
        {
            Debug.Log(_selectedID + "entro");
            EventSystem.current.SetSelectedGameObject(selectFormer);
        }

        //Calculamos la navegación en la UI
        Debug.Log("hola");
        calculateNavigation();
    }

    /// <summary>
    /// Método auxiliar que instancia e inicializa el prefab <see cref="_slotPrefab"/> según el objeto pasado por parámetro.
    /// </summary>
    /// <param name="item">El objeto a instanciar.</param>
    /// <param name="type">El tipo del objeto buscado.</param>
    public void createItem(lootItem item, itemTypeEnum type)
    {
        //Creamos, inicializamos e instanciamos el prefab
        GameObject newItem = Instantiate(_slotPrefab);
        newItem.GetComponent<Image>().sprite = item.getIcon();
        newItem.GetComponent<slotData>().setInventoryStock(item.getQuantity());
        newItem.GetComponent<slotData>().setBackUpStock(0);
        newItem.GetComponent<slotData>().setLootItem(item);

        if (newItem.GetComponent<slotData>().getTipo() == itemTypeEnum.weapon)
        {
            newItem.GetComponent<Button>().onClick.AddListener(() => {
                config.getInventory().GetComponent<weaponInventoryManagement>().equipWeapon();
            });
        }
        else if (newItem.GetComponent<slotData>().getTipo() == itemTypeEnum.consumable || newItem.GetComponent<slotData>().getTipo() == itemTypeEnum.refillable)
        {
            newItem.GetComponent<Button>().onClick.AddListener(() => {
                UIConfig.getController().useEquippingObjectUI();
            });
        }

        lootItem searchedBackup = _backupQuery.Find(itemBackUp => itemBackUp.getTipo() == type && itemBackUp.getID() == item.getID());

        if (searchedBackup != null)
        {
            newItem.GetComponent<slotData>().setBackUpStock(searchedBackup.getQuantity());
        }
        newItem.transform.SetParent(_slotHolder, false);
        _itemList.Add(newItem);
    }

    /// <summary>
    /// Método auxiliar que calcula la navegación en la UI entre objetos.
    /// </summary>
    public void calculateNavigation()
    {
        int counter = 0;
        Navigation currentSlotNavigation;
        Navigation modeNavigation = new Navigation();
        //Recorremos los objetos instanciados
        for (int index = 0; index < _itemList.Count; ++index)
        {
            modeNavigation.mode = Navigation.Mode.None;
            _itemList[index].GetComponent<Button>().navigation = modeNavigation;
            modeNavigation.mode = Navigation.Mode.Explicit;
            _itemList[index].GetComponent<Button>().navigation = modeNavigation;

            //Obtenemos la navegación del objeto actual
            currentSlotNavigation = _itemList[index].GetComponent<Button>().navigation;

            if (index == 0 && _itemList.Count > 1) //Estamos en el primer objeto y no es el último
            {
                currentSlotNavigation.selectOnRight = _itemList[index + 1].GetComponent<Button>();
                counter++;
            }
            else
            {
                if (index == _itemList.Count - 1) //Es el último objeto
                {
                    if (index % _grid.constraintCount != 0) //No es el primer objeto de una fila
                    {
                        currentSlotNavigation.selectOnLeft = _itemList[index - 1].GetComponent<Button>();
                    }
                }
                else //No estamos en el último objeto
                {
                    if (index % _grid.constraintCount != 0) //No es el primer objeto de una fila
                    {
                        currentSlotNavigation.selectOnLeft = _itemList[index - 1].GetComponent<Button>();
                    }
                    if (counter < (_grid.constraintCount - 1)) // No es el último objeto de la fila
                    {
                        currentSlotNavigation.selectOnRight = _itemList[index + 1].GetComponent<Button>();
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }

            //No es la fila de arriba

            if (index - _grid.constraintCount >= 0)
            {
                currentSlotNavigation.selectOnUp = _itemList[index - _grid.constraintCount].GetComponent<Button>();
            }

            //No es la fila de abajo
            if (index + _grid.constraintCount < _itemList.Count)
            {
                currentSlotNavigation.selectOnDown = _itemList[index + _grid.constraintCount].GetComponent<Button>();
            }
            _itemList[index].GetComponent<Button>().navigation = currentSlotNavigation;

        }
    }

    /// <summary>
    /// Getter que devuelve <see cref="_expandedSlotHolder"/>.
    /// </summary>
    /// <returns>Un objeto de tipo Transform que representa la zona de la UI en la que aparecen los objetos.</returns>
    public Transform getSlotHolder()
    {
        return _expandedSlotHolder;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_slotPrefab"/>.
    /// </summary>
    /// <returns>Un GameObject que representa el prefab del objeto.</returns>
    public GameObject getSlotPrefab()
    {
        return _slotPrefab;
    }

    /// <summary>
    /// Método auxiliar para eliminar todos los objetos instanciados para próximas inicializaciones de la UI.
    /// </summary>
    public void destroyItems()
    {
        foreach (GameObject item in _itemList)
        {
            Destroy(item);
        }
        _itemList.Clear();
    }

    /// <summary>
    /// Setter que modifica el texto de <see cref="_backupValue"/>.
    /// </summary>
    /// <param name="value">El texto a asignar.</param>
    public void setBackUpValue(int value)
    {
        _backupValue.text = value.ToString() + "/" + config.getInventory().GetComponent<inventoryManager>().getMaximumBackUp().ToString();
    }
    /// <summary>
    /// Setter que modifica el texto de <see cref="_inventoryValue"/>.
    /// </summary>
    /// <param name="value">El texto a asignar.</param>
    public void setInventoryValue(int value)
    {
        _inventoryValue.text = value.ToString() + "/" + config.getInventory().GetComponent<inventoryManager>().getMaximumInventory().ToString();
    }

    /// <summary>
    /// Setter que modifica el sprite de <see cref="_render"/>.
    /// </summary>
    /// <param name="render">El sprite a asignar.</param>
    public void setRenderImage(Sprite render)
    {
        _render.GetComponent<Image>().sprite = render;
    }

    /// <summary>
    /// Setter que modifica el texto de <see cref="_description"/>.
    /// </summary>
    /// <param name="text">El texto a asignar.</param>
    public void setDescriptionText(string text)
    {
        _description.text = text;
    }

    /// <summary>
    /// Setter que modifica el texto de <see cref="_name"/>.
    /// </summary>
    /// <param name="text">El texto a asignar.</param>
    public void setItemName(string text)
    {
        _name.text = text;
    }
}
