using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// weaponInventoryManagement es una clase usada para manejar las armas que tenemos equipadas y utilizada para
/// equipar el arma que seleccionemos en la UI de inventario.
/// </summary>
public class weaponInventoryManagement : MonoBehaviour
{
    /// <summary>
    /// Lista con las armas del juego.
    /// </summary>
    [SerializeField] private List<weaponSlot> _weaponList;
    
    /// <summary>
    /// El objeto pulsado en la UI de inventario.
    /// </summary>
    private slotData _pressedItem;

    /// <summary>
    /// El índice del objeto pulsado en la UI de inventario.
    /// </summary>
    private int _pressedIndex;

    /// <summary>
    /// Referencia al arma que instanciemos.
    /// </summary>
    private GameObject _instantiatedPrefab;

    /// <summary>
    /// Referencia al slot del arma seleccionada.
    /// </summary>
    private weaponSlot _clickedWeapon;

    /// <summary>
    /// Referencia al arma que tenemos en la mano primaria.
    /// </summary>
    private GameObject _primary;

    /// <summary>
    /// Referencia al arma que tenemos en la mano secundaria.
    /// </summary>
    private GameObject _secundary;

    /// <summary>
    /// Método que se ejecuta al inicio del script tras todos los Awake.
    /// </summary>
    private void Start()
    {
        //Obtenemos los datos de armas cargadas
        equippedWeaponsData data = saveSystem.loadWeaponsState();

        //Si hemos equipado alguna alguna vez
        if (data != null)
        {
            //Si teníamos equipada la primera
            if (data.getPrimaryIndex() != -1)
            {
                //La creamos y equipamos
                _primary = Instantiate(_weaponList[data.getPrimaryIndex()].getWeapon());
                _primary.GetComponent<weapon>().createWeapon(data.getWeaponsLevels()[data.getPrimaryIndex()]);
            }

            //Si teníamos equipada la segunda
            if (data.getSecundaryIndex() != -1)
            {
                //La creamos y equipamos
                _secundary = Instantiate(_weaponList[data.getSecundaryIndex()].getWeapon());
                _secundary.GetComponent<weapon>().createWeapon(data.getWeaponsLevels()[data.getSecundaryIndex()]);
                
            }
        }
    }

    /// <summary>
    /// Getter que devuelve <see cref="_weaponList"/>.
    /// </summary>
    /// <returns><see cref="_weaponList"/></returns>
    public List<weaponSlot> getWeaponList()
    {
        return _weaponList;
    }

    /// <summary>
    /// Método para obtener el índice del slot.
    /// </summary>
    /// <param name="slot">Slot del cual queremos obtener el índice.</param>
    /// <returns>Índice del slot dentro de la UI.</returns>
    public int getIndexOf(slotData slot)
    {
        weaponSlot data = _weaponList.Find(item => item.getID() == slot.getID());
        return _weaponList.IndexOf(data);
    }

    /// <summary>
    /// Método encargado de equipar el arma seleccionada.
    /// </summary>
    public void equipWeapon()
    {
        //Obtenemos el objeto pulsado
        _pressedItem = EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<slotData>();
        if (_pressedItem != null)  
        {
            //Comprobamos que sea un arma
            if (_pressedItem.getTipo() == itemTypeEnum.weapon)
            {
                //Obtenemos los datos de las armas equipadas
                equippedWeaponsData data = saveSystem.loadWeaponsState();
                _pressedIndex = getIndexOf(_pressedItem);
                _clickedWeapon = _weaponList.Find(weapon => weapon.getID() == _pressedItem.getID());
                
                //Si el arma pulsada es primaria
                if (_clickedWeapon.getHand() == handEnum.primary)
                {
                    //Comprobamos que esté equipada y la eliminamos
                    if (_primary != null)
                    {
                        destroySkills(_primary);
                        Destroy(_primary);
                    }
                    //La equipamos
                    _instantiatedPrefab = Instantiate(_weaponList[_pressedIndex].getWeapon());
                    _instantiatedPrefab.GetComponent<weapon>().createWeapon(data.getWeaponsLevels()[_pressedIndex]);
                    _primary = _instantiatedPrefab;

                    //Modificamos el índice de arma primaria
                    data.setPrimaryIndex(_weaponList.IndexOf(_clickedWeapon));
                }
                _clickedWeapon = _weaponList.Find(weapon => weapon.getID() == _pressedItem.getID());

                //Si en cambio el arma pulsada es secundaria
                if (_clickedWeapon.getHand() == handEnum.secundary)
                {
                    //Comprobamos que esté equipada y la eliminamos
                    if (_secundary != null)
                    {
                        destroySkills(_secundary);
                        Destroy(_secundary);
                    }
                    //La equipamos
                    _instantiatedPrefab = Instantiate(_weaponList[_pressedIndex].getWeapon());
                    _instantiatedPrefab.GetComponent<weapon>().createWeapon(data.getWeaponsLevels()[_pressedIndex]);
                    _secundary = _instantiatedPrefab;

                    //Modificamos el índice de arma secundaria
                    data.setSecundaryIndex(_weaponList.IndexOf(_clickedWeapon));
                }
                //Guardamos el nuevo estado de armas equipadas
                saveSystem.saveWeaponsState(data.getPrimaryIndex(), data.getSecundaryIndex(), data.getWeaponsLevels());
            }
        }
    }

    /// <summary>
    /// Getter que devuelve la cantidad de material de mejora necesario para subir un nivel de un arma dada.
    /// </summary>
    /// <param name="index">ID del arma.</param>
    /// <returns>Cantidad del material de mejora necesario.</returns>
    public int getMaterialQuantity(int index)
    {
        return getWeaponList()[index].getWeapon().GetComponent<weapon>().getQuantites()[(saveSystem.loadWeaponsState().getWeaponsLevels()[index] - 1)];
    }

    /// <summary>
    /// Getter que devuelve el sprite del material de mejora necesario para subir el siguiente nivel.
    /// </summary>
    /// <param name="index">ID del arma.</param>
    /// <returns>Sprite del material de mejora necesario para el siguiente nivel.</returns>
    public Sprite getMaterialSprite(int index)
    {
        return getWeaponList()[index].getWeapon().GetComponent<weapon>().getListOfMaterials()[(saveSystem.loadWeaponsState().getWeaponsLevels()[index] - 1)].getItemData().getIcon();
    }

    /// <summary>
    /// Getter que devuelve la cantidad de todos los materiales de mejora necesarios para subir 
    /// los niveles de un arma dada.
    /// </summary>
    /// <param name="index">ID del arma.</param>
    /// <returns>Cantidad de los materiales de mejora necesarios.</returns>
    public int getQuantityOf(int index)
    {
        return getWeaponList()[index].getWeapon().GetComponent<weapon>().getQuantites()[(saveSystem.loadWeaponsState().getWeaponsLevels()[index] - 1)];
    }

    /// <summary>
    /// Método auxiliar para destruir las habilidades del arma que teníamos equipada.
    /// </summary>
    /// <param name="weapon">Arma equipada.</param>
    public void destroySkills(GameObject weapon)
    {
        //Si tiene habilidades
        if (weapon.GetComponent<weapon>().getWeaponSkills() != null)
        {
            //Destruimos cada una de las habilidades
            for (int i = 0; i < weapon.GetComponent<weapon>().getWeaponSkills().Count; ++i)
            {
                Destroy(weapon.GetComponent<weapon>().getWeaponSkills()[i]);
            }
            weapon.GetComponent<weapon>().getWeaponSkills().Clear();
        }
    }

}
