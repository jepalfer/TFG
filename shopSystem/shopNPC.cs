using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// shopNPC es una clase que maneja la lógica y representación de la entidad de la tienda.
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
    /// Referencia a la información serializada sobre las tiendas.
    /// </summary>
    private shopData _data;

    /// <summary>
    /// Booleano para saber si podemos abrir o no la tienda al interactuar con la tienda.
    /// </summary>
    private bool _playerOn;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// Crea la información de la tienda o asigna las cantidades según si esta ya existe o no.
    /// </summary>
    private void Start()
    {
        //Obtención de la información
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
            else //La información de la ya está guardada
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
    /// Método que se encarga de crear la información de la tienda instanciada.
    /// </summary>
    private void createNewShop()
    {
        //Creamos un nuevo objeto para representar la tienda
        sceneShopData newShop = new sceneShopData();

        //Añadimos los objetos a la tienda
        for (int i = 0; i < _shopInventory.Count; i++)
        {
            newShop.addItem(_shopInventory[i].getQuantity(), _shopID, SceneManager.GetActiveScene().buildIndex);
        }

        //Añadimos la tienda
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
    /// Método para cuando el jugador entra en el trigger de la entidad.
    /// </summary>
    /// <param name="collision">Colisión que ha entrado en contacto con el trigger de la entidad.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerOn = true;
        }
    }


    /// <summary>
    /// Método para cuando el jugador sale del trigger de la entidad.
    /// </summary>
    /// <param name="collision">Colisión que ha salido de contacto con el trigger de la entidad.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerOn = false;
        }
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        if (_playerOn && inputManager.GetKeyDown(inputEnum.interact) && !UIController.getIsInShopUI() && !UIController.getIsInPauseUI() && !UIController.getIsInInventoryUI() && !UIController.getIsInEquippingSkillUI())
        {
            UIConfig.getController().useShopUI(_shopInventory);
        }
    }
}
