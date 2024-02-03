using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopNPC : MonoBehaviour
{
    [SerializeField] private int _shopID;
    [SerializeField] private List<shopItem> _shopInventory;
    private bool _playerOn;

    private void Start()
    {
        _playerOn = false;
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
