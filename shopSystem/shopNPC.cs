using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shopNPC : MonoBehaviour
{
    [SerializeField] private int _shopID;
    [SerializeField] private List<shopItem> _shopInventory;

    private shopData _data;
    private bool _playerOn;

    private void Start()
    {
        _playerOn = false;
        _data = saveSystem.loadShopData();

        if (_data == null)
        {
            createNewShop();
        }
        else
        {
            if (_data.getData().Find(shop => shop.getSceneID() == SceneManager.GetActiveScene().buildIndex && shop.getShopID() == _shopID) == null)
            {
                createNewShop();
            }
            else
            {
                sceneShopData searchedShop = _data.getData().Find(shop => shop.getSceneID() == SceneManager.GetActiveScene().buildIndex && shop.getShopID() == _shopID);

                for (int i = 0; i < searchedShop.getItemsInShop().Count; i++)
                {
                    _shopInventory[i].setQuantity(searchedShop.getItemsInShop()[i]);
                }
            }
        }

    }

    private void createNewShop()
    {
        sceneShopData newShop = new sceneShopData();

        for (int i = 0; i < _shopInventory.Count; i++)
        {
            newShop.addItem(_shopInventory[i].getQuantity(), _shopID, SceneManager.GetActiveScene().buildIndex);
        }

        shopData newShopData = new shopData();
        newShopData.addData(newShop);
        saveSystem.saveShopData(newShopData.getData());
    }

    public int getShopID()
    {
        return _shopID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerOn = false;
        }
    }

    private void Update()
    {
        if (_playerOn && inputManager.GetKeyDown(inputEnum.interact) && !UIController.getIsInShopUI() && !UIController.getIsInPauseUI() && !UIController.getIsInInventoryUI() && !UIController.getIsInEquippingSkillUI())
        {
            UIConfig.getController().useShopUI(_shopInventory);
        }
    }
}
