using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class buyItemUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _quantityText;
    private int _minBuy;
    private int _singularItemPrice;
    private int _currentBuy;
    private int _totalPrice;
    private int _maxBuy;
    private int _shopID;
    private int _itemIndex;
    private GameObject _itemToBuy;
    public void initializeUI(GameObject itemToBuy, int itemPrice, int itemIndex)
    {
        _minBuy = 1;
        _currentBuy = 1;
        _quantityText.text = _currentBuy.ToString();
        _shopID = saveSystem.loadShopData().getShopID(SceneManager.GetActiveScene().buildIndex);
        _totalPrice = itemPrice;
        _singularItemPrice = itemPrice;
        _itemIndex = itemIndex;
        _itemToBuy = itemToBuy;
        int itemsInShop = UIConfig.getController().getShopUI().GetComponent<shopUIController>().getShopItemsList()[itemIndex].getQuantity();

        inventoryManager inventory = config.getInventory().GetComponent<inventoryManager>();

        if (itemToBuy.GetComponent<generalItem>() != null)
        {
            lootItem inventoryItem = config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getID() == itemToBuy.GetComponent<generalItem>().getID());
            lootItem backupItem = config.getInventory().GetComponent<inventoryManager>().getBackUp().Find(item => item.getID() == itemToBuy.GetComponent<generalItem>().getID());
            int inventoryQuantity = inventoryItem == null ? inventory.getMaximumInventory() : inventory.getMaximumInventory() - inventoryItem.getQuantity();
            int backupQuantity = backupItem == null ? inventory.getMaximumBackUp() : inventory.getMaximumBackUp() - backupItem.getQuantity();

            int canBuy = inventoryQuantity + backupQuantity;
            _maxBuy = Mathf.Min(itemsInShop, canBuy);
        }
        else
        {
            _maxBuy = 1;
        }
        UIConfig.getController().getShopUI().GetComponent<shopUIController>().setNavigationOff();

    }

    private void Update()
    {
        if (inputManager.GetKeyDown(inputEnum.oneLessItem) && _currentBuy > _minBuy)
        {
            _currentBuy--;
            _quantityText.text = _currentBuy.ToString();
            _totalPrice -= _singularItemPrice;
        }

        if (((_totalPrice + _singularItemPrice) <= config.getPlayer().GetComponent<combatController>().getSouls()) &&
             (inputManager.GetKeyDown(inputEnum.oneMoreItem) && _currentBuy < _maxBuy))
        {
            _currentBuy++;
            _quantityText.text = _currentBuy.ToString();
            _totalPrice += _singularItemPrice;
        }

        if (inputManager.GetKeyDown(inputEnum.buyItem))
        {
            UIConfig.getController().getShopUI().GetComponent<shopUIController>().buyItem(_shopID, _itemIndex, _totalPrice, _currentBuy);

            if (_itemToBuy.GetComponent<generalItem>() != null)
            {
                config.getInventory().GetComponent<inventoryManager>().addItemToInventory(new lootItem(_itemToBuy.GetComponent<generalItem>().getData().getData(), _currentBuy));
                config.getPlayer().GetComponent<equippedInventory>().modifyIfBuy(_itemToBuy.GetComponent<generalItem>().getID());
            }
            else
            {
                Debug.Log("hola");
                sceneSkillsState newSkill = new sceneSkillsState(-1, _itemToBuy.GetComponent<skill>()); 
                unlockedSkillsData data = saveSystem.loadSkillsState();

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
            }
            UIConfig.getController().useBuyItemUI();
        }
    }
    
    public void setUIOff()
    {
        UIConfig.getController().getShopUI().GetComponent<shopUIController>().calculateNavigation();
    }
}
