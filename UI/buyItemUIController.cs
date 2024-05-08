using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// buyItemUIController es una clase que controla la UI para gestionar la compra del objeto seleccionado de 
/// una tienda.
/// </summary>
public class buyItemUIController : MonoBehaviour
{
    /// <summary>
    /// Referencia al campo de texto donde aparece la cantidad a comprar.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _quantityText;

    /// <summary>
    /// Entero que representa la cantidad mínima de compra.
    /// </summary>
    private int _minBuy;

    /// <summary>
    /// Entero que representa el coste de un único objeto.
    /// </summary>
    private int _singularItemPrice;

    /// <summary>
    /// Entero que representa la cantidad a comprar.
    /// </summary>
    private int _currentBuy;

    /// <summary>
    /// Entero que representa el coste acumulado de objetos.
    /// </summary>
    private int _totalPrice;

    /// <summary>
    /// Entero que representa la cantidad máxima de compra.
    /// </summary>
    private int _maxBuy;

    /// <summary>
    /// ID interno de la tienda.
    /// </summary>
    private int _shopID;

    /// <summary>
    /// Índice del objeto dentro de la tienda.
    /// </summary>
    private int _itemIndex;

    /// <summary>
    /// Referencia al objeto que se está comprando.
    /// </summary>
    private GameObject _itemToBuy;

    /// <summary>
    /// Método que se ejecuta al hacer visible la UI y la inicializa.
    /// </summary>
    /// <param name="itemToBuy">Objeto a comprar.</param>
    /// <param name="itemPrice">Precio de compra del objeto.</param>
    /// <param name="itemIndex">Índice del objeto en la tienda.</param>
    public void initializeUI(GameObject itemToBuy, int itemPrice, int itemIndex)
    {
        //Asignación de variables
        _minBuy = 1;
        _currentBuy = 1;
        _quantityText.text = _currentBuy.ToString();
        _shopID = saveSystem.loadShopData().getShopID(SceneManager.GetActiveScene().buildIndex);
        _totalPrice = itemPrice;
        _singularItemPrice = itemPrice;
        _itemIndex = itemIndex;
        _itemToBuy = itemToBuy;

        //Obtención de los objetos de los que dispone la tienda
        int itemsInShop = UIConfig.getController().getShopUI().GetComponent<shopUIController>().getShopItemsList()[itemIndex].getQuantity();

        //Obtención del inventario
        inventoryManager inventory = config.getInventory().GetComponent<inventoryManager>();

        //Es un objeto
        if (itemToBuy.GetComponent<generalItem>() != null)
        {
            //Cálculo de la cantidad máxima de compra
            lootItem inventoryItem = config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getID() == itemToBuy.GetComponent<generalItem>().getID());
            lootItem backupItem = config.getInventory().GetComponent<inventoryManager>().getBackUp().Find(item => item.getID() == itemToBuy.GetComponent<generalItem>().getID());
            int inventoryQuantity = inventoryItem == null ? inventory.getMaximumInventory() : inventory.getMaximumInventory() - inventoryItem.getQuantity();
            int backupQuantity = backupItem == null ? inventory.getMaximumBackUp() : inventory.getMaximumBackUp() - backupItem.getQuantity();

            int canBuy = inventoryQuantity + backupQuantity;
            _maxBuy = Mathf.Min(itemsInShop, canBuy);
        }
        else //Es una habilidad
        {
            _maxBuy = 1;
        }
        UIConfig.getController().getShopUI().GetComponent<shopUIController>().setNavigationOff();

    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        //Restamos un objeto
        if (inputManager.GetKeyDown(inputEnum.oneLessItem) && _currentBuy > _minBuy)
        {
            _currentBuy--;
            _quantityText.text = _currentBuy.ToString();
            _totalPrice -= _singularItemPrice;
        }
        //Sumamos un objeto
        if (((_totalPrice + _singularItemPrice) <= config.getPlayer().GetComponent<combatController>().getSouls()) &&
             (inputManager.GetKeyDown(inputEnum.oneMoreItem) && _currentBuy < _maxBuy))
        {
            _currentBuy++;
            _quantityText.text = _currentBuy.ToString();
            _totalPrice += _singularItemPrice;
        }
        //Compramos el objeto
        if (inputManager.GetKeyDown(inputEnum.buyItem))
        {
            //Se efectúa la compra
            UIConfig.getController().getShopUI().GetComponent<shopUIController>().buyItem(_shopID, _itemIndex, _totalPrice, _currentBuy);

            //Es un objeto
            if (_itemToBuy.GetComponent<generalItem>() != null)
            {
                config.getInventory().GetComponent<inventoryManager>().addItemToInventory(new lootItem(_itemToBuy.GetComponent<generalItem>().getData().getData(), _currentBuy));
                config.getPlayer().GetComponent<equippedInventory>().modifyIfBuy(_itemToBuy.GetComponent<generalItem>().getID());
            }
            else //Es una habilidad
            {
                Debug.Log("hola");
                sceneSkillsState newSkill = new sceneSkillsState(-1, _itemToBuy.GetComponent<skill>()); 
                //Cargamos las habilidades desbloqueadas
                unlockedSkillsData data = saveSystem.loadSkillsState();

                if (data == null) //No teníamos ninguna habilidad
                {
                    List<sceneSkillsState> newData = new List<sceneSkillsState>();
                    newData.Add(newSkill);
                    saveSystem.saveSkillsState(newData);
                }
                else //Ya teníamos alguna habilidad
                {
                    data.getUnlockedSkills().Add(newSkill);
                    saveSystem.saveSkillsState(data.getUnlockedSkills());
                }
            }
            //Escondemos la UI
            UIConfig.getController().useBuyItemUI();
        }
    }
    
    /// <summary>
    /// Método que se ejecuta al esconder la UI.
    /// </summary>
    public void setUIOff()
    {
        //Calculamos la navegación de nuevo
        UIConfig.getController().getShopUI().GetComponent<shopUIController>().calculateNavigation();
    }
}
