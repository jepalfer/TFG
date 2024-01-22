using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// chestSystem es una clase que controla el comportamiento de los cofres.
/// </summary>
public class chestSystem : MonoBehaviour
{
    /// <summary>
    /// El ID interno del cofre.
    /// </summary>
    [SerializeField] private int _chestID;
    /// <summary>
    /// El animator del cofre.
    /// </summary>
    [Header("Animacion")]
    private Animator _animator;

    /// <summary>
    /// Si el cofre está o no abierto.
    /// </summary>
    private bool _isOpen = false;

    /// <summary>
    /// Si el jugador está o no en el cofre.
    /// </summary>
    private bool _playerOn = false;

    /// <summary>
    /// Método que se llama al iniciar el script. Ver <see cref="lootableItemData"/>, <see cref="sceneLootableItem"/> y <see cref="saveSystem.saveLootableObjectsData(List{sceneLootableItem})"/> para más información
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();

        //Creamos un objeto para serializarlo
        sceneLootableItem objectData = new sceneLootableItem(SceneManager.GetActiveScene().buildIndex, _chestID, 0);
        
        //Obtenemos la información respectiva a los objetos looteables del saveSystem
        lootableItemData data = saveSystem.loadLootableObjectsData();

        //Si el archivo existe
        if (data != null)
        {
            //Buscamos si ya hemos serializado el objeto que hemos creado
            sceneLootableItem item = data.getObjectsStates().Find(item => item.getSceneID() == objectData.getSceneID() && item.getObjectID() == objectData.getObjectID());
            if (item == null)               //Si no existe
            {
                //Metemos el objeto en el vector y guardamos
                data.incrementSize(objectData);
                saveSystem.saveLootableObjectsData(data.getObjectsStates());
            }
            else                            //En caso contrario
            {
                //Comprobamos que esté looteado
                if (item.getIsLooted() == 1)
                {
                    _isOpen = true;
                    _animator.SetTrigger("Open");
                }
            }
        }
        else     //En caso contrario
        {           
            //Creamos un lootableItemData nuevo, le metemos el objeto y guardamos
            lootableItemData state = new lootableItemData();
            state.incrementSize(objectData);
            saveSystem.saveLootableObjectsData(state.getObjectsStates());
        }

    }

    /// <summary>
    /// Comprueba que algo haya entrado al trigger y enseña la UI.
    /// </summary>
    /// <param name="collision">Collider que ha entrado al trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !_isOpen)
        {
            _playerOn = true;
            GetComponent<chestUI>().showUI();
        }
    }
    /// <summary>
    /// Comprueba que algo haya salido del trigger y esconde la UI.
    /// </summary>
    /// <param name="collision">Collider que ha salido del trigger</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _playerOn = false;
            GetComponent<chestUI>().hideUI();
        }
    }

    /// <summary>
    /// Getter que devuelve si el cofre ha sido abierto.
    /// </summary>
    /// <returns>Devuelve un valor booleano</returns>
    public bool getIsOpen()
    {
        return _isOpen;
    }

    /// <summary>
    /// Método al que se llama cada frame para actualizar la lógica
    /// </summary>
    private void Update()
    {
        //Si pulsamos la tecla/botón asociado a interactuar, no está looteado, el jugador está tocando el cofre y no hay ninguna UI abierta
        if (inputManager.getKeyDown(inputEnum.Interact.ToString()) && !_isOpen && _playerOn && !config.getPlayer().GetComponent<playerMovement>().getIsDodging() && !UIController.getIsPaused() && !UIController.getIsLevelingUp() && !UIController.getIsAdquiringSkills() && !UIController.getIsLevelingUpWeapon() && !UIController.getIsInInventory())
        {
            _animator.SetTrigger("Open");
            _isOpen = true;
            GetComponent<chestUI>().hideUI();
            GetComponent<lootSystem>().giveLoot();

            //Obtenemos la información de los objetos lootables, lo modificamos y guardamos
            lootableItemData data = saveSystem.loadLootableObjectsData();
            data.getObjectsStates().Find(item => item.getSceneID() == SceneManager.GetActiveScene().buildIndex && item.getObjectID() == _chestID).setIsLooted(1);
            saveSystem.saveLootableObjectsData(data.getObjectsStates());

        }
    }
}
