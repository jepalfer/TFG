using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// shopNPC es una clase que maneja la l�gica y representaci�n de la entidad de la tienda.
/// </summary>
public class shopNPC : MonoBehaviour
{
    /// <summary>
    /// int que representa el ID interno de la tienda.
    /// </summary>
    [SerializeField] private int _shopID;

    /// <summary>
    /// Lista de <see cref="shopItem"/> que representa el inventario de la tienda.
    /// </summary>
    [SerializeField] private List<shopItem> _shopInventory;

    /// <summary>
    /// Referencia a la informaci�n serializada sobre las tiendas.
    /// </summary>
    private shopData _data;

    /// <summary>
    /// Booleano para saber si podemos abrir o no la tienda al interactuar con la tienda.
    /// </summary>
    private bool _playerOn;

    /// <summary>
    /// M�todo que se ejecuta al inicio del script.
    /// Crea la informaci�n de la tienda o asigna las cantidades seg�n si esta ya existe o no.
    /// </summary>
    private void Start()
    {
        //Obtenci�n de la informaci�n
        _playerOn = false;
        _data = saveSystem.loadShopData();

        //Primera tienda que instanciamos
        if (_data == null)
        {
            createNewShop();
        }
        else
        {
            //Primera vez que instanciamos la tienda
            if (_data.getData().Find(shop => shop.getSceneID() == SceneManager.GetActiveScene().buildIndex && shop.getShopID() == _shopID) == null)
            {
                createNewShop();
            }
            else //La informaci�n de la ya est� guardada
            {
                sceneShopData searchedShop = _data.getData().Find(shop => shop.getSceneID() == SceneManager.GetActiveScene().buildIndex && shop.getShopID() == _shopID);

                for (int i = 0; i < searchedShop.getItemsInShop().Count; i++)
                {
                    _shopInventory[i].setQuantity(searchedShop.getItemsInShop()[i]);
                }
            }
        }

    }

    /// <summary>
    /// M�todo que se encarga de crear la informaci�n de la tienda instanciada.
    /// </summary>
    private void createNewShop()
    {
        //Creamos un nuevo objeto para representar la tienda
        sceneShopData newShop = new sceneShopData();

        //A�adimos los objetos a la tienda
        for (int i = 0; i < _shopInventory.Count; i++)
        {
            newShop.addItem(_shopInventory[i].getQuantity(), _shopID, SceneManager.GetActiveScene().buildIndex);
        }

        //A�adimos la tienda
        shopData newShopData = new shopData();
        newShopData.addData(newShop);
        saveSystem.saveShopData(newShopData.getData());
    }

    /// <summary>
    /// Getter que devuelve <see cref="_shopID"/>.
    /// </summary>
    /// <returns>int que representa el ID interno de la tienda.</returns>
    public int getShopID()
    {
        return _shopID;
    }

    /// <summary>
    /// M�todo para cuando el jugador entra en el trigger de la entidad.
    /// </summary>
    /// <param name="collision">Colisi�n que ha entrado en contacto con el trigger de la entidad.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerOn = true;
        }
    }


    /// <summary>
    /// M�todo para cuando el jugador sale del trigger de la entidad.
    /// </summary>
    /// <param name="collision">Colisi�n que ha salido de contacto con el trigger de la entidad.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerOn = false;
        }
    }

    /// <summary>
    /// M�todo que se ejecuta cada frame para actualizar la l�gica.
    /// </summary>
    private void Update()
    {
        if (_playerOn && inputManager.GetKeyDown(inputEnum.interact) && !UIController.getIsInShopUI() && !UIController.getIsInPauseUI() && !UIController.getIsInInventoryUI() && !UIController.getIsInEquippingSkillUI())
        {
            UIConfig.getController().useShopUI(_shopInventory);
        }
    }
}
