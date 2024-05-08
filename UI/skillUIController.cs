using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// skillUIController es una clase que se usa para controlar la UI de selección de slot para equipar una habilidad.
/// </summary>
public class skillUIController : MonoBehaviour
{
    /// <summary>
    /// Índice del slot que tenemos seleccionado.
    /// </summary>
    [SerializeField] private int _currentID;

    /// <summary>
    /// Lista con las referencias a los slots equipables.
    /// </summary>
    [SerializeField] private List<GameObject> _slots;

    /// <summary>
    /// Lista con los sprites a modificar de cada slot.
    /// </summary>
    [SerializeField] private List<GameObject> _sprites;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    void Start()
    {
        _currentID = 0;
    }

    /// <summary>
    /// Setter que modifica <see cref="_currentID"/>.
    /// </summary>
    /// <param name="id">ID a asignar.</param>
    public void setID(int id)
    {
        _currentID = id;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_sprites"/>.
    /// </summary>
    /// <returns><see cref="_sprites"/>.</returns>
    public List<GameObject> getSprites()
    {
        return _sprites;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_slots"/>.
    /// </summary>
    /// <returns><see cref="_slots"/>.</returns>
    public List<GameObject> getSlots()
    {
        return _slots;
    }

    /// <summary>
    /// Método que equipa una habilidad determinada.
    /// </summary>
    /// <param name="skill">Datos internos de la habilidad a equipar.</param>
    public void equipSkill(skillData skill)
    {
        //Obtenemos la lista de habilidades equipadas
        List<GameObject> equippedSkills = config.getPlayer().GetComponent<skillManager>().getEquippedSkills();

        //Comprobamos que esté equipada
        int index = -1;
        checkIfEquipped(equippedSkills, ref index, skill);

        //Cargamos las habilidades que hay equipadas
        equippedSkillsData data = saveSystem.loadEquippedSkillsState();
        int[] IDs;
        skillTypeEnum[] types;

        //Si no hay ninguna equipada
        if (data == null)
        {
            //Entonces todos los ID serán -1
            IDs = new int[3];
            types = new skillTypeEnum[3];
            for (int i = 0; i < equippedSkills.Count; ++i)
            {
                IDs[i] = -1;
            }
        }
        else //Obtenemos los ID que había equipados
        {
            IDs = data.getIDs();
            types = data.getTypes();
        }

        //Si se ha encontrado la habilidad
        if (index != -1)
        {
            //Destruimos la habilidad anterior
            Destroy(config.getPlayer().GetComponent<skillManager>().getEquippedSkills()[index]);
            config.getPlayer().GetComponent<skillManager>().setEquippedValue(null, index);
            _sprites[index].GetComponent<Image>().sprite = null;
            _sprites[index].SetActive(false);

            IDs[index] = -1;

        }

        //Equipamos la nueva habilidad
        config.getPlayer().GetComponent<skillManager>().equipSkill(skill, _currentID);

        //Modificamos el sprite del slot donde hemos equipado el arma
        _sprites[_currentID].SetActive(true);
        _sprites[_currentID].GetComponent<Image>().sprite = skill.getSkillSprite();

        IDs[_currentID] = skill.getSkillID();
        types[_currentID] = skill.getType();

        //Guardamos el nuevo estado de habilidades equipadas
        saveSystem.saveEquippedSkillsState(IDs, types);

        //Modificamos los daños por si era una habilidad que aumentara algún valor que afecte
        if (weaponConfig.getPrimaryWeapon() != null)
        {
            weaponConfig.getPrimaryWeapon().GetComponent<weapon>().setTotalDMG(weaponConfig.getPrimaryWeapon().GetComponent<weapon>().calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel()));
        }
        if (weaponConfig.getSecundaryWeapon() != null)
        {
            weaponConfig.getSecundaryWeapon().GetComponent<weapon>().setTotalDMG(weaponConfig.getSecundaryWeapon().GetComponent<weapon>().calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel()));
        }
    }

    /// <summary>
    /// Método auxiliar que comprueba que una habilidad concreta esté equipada en un slot concreto.
    /// </summary>
    /// <param name="skills">Lista con las habilidades equipadas.</param>
    /// <param name="index">Índice del slot seleccionado.</param>
    /// <param name="data">Datos internos de la habilidad.</param>
    public void checkIfEquipped(List<GameObject> skills, ref int index, skillData data)
    {
        for (int i = 0; i < skills.Count; ++i)
        {
            if (skills[i] != null) //Hay una habilidad en este slot
            {
                //Esa habilidad no es la que queremos equipar
                if (skills[i].GetComponent<skill>().getData().getSkillID() == data.getSkillID() && i != _currentID)
                {
                    index = i;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        //Si no estamos seleccionando la habilidad
        if (!UIController.getIsSelectingSkillUI())
        {
            //Comprobación de límites hacia la izquierda
            if (inputManager.GetKeyDown(inputEnum.left) && _currentID > 0)
            {
                _slots[_currentID].SetActive(false);
                _currentID--;
                _slots[_currentID].SetActive(true);
            }
            //Comprobación de límites hacia la derecha
            if (inputManager.GetKeyDown(inputEnum.right) && _currentID < (_slots.Count - 1))
            {
                _slots[_currentID].SetActive(false);
                _currentID++;
                _slots[_currentID].SetActive(true);
            }

            //Si hemos pulsado el botón asignado a la acción de entrar a equipar
            if (inputManager.GetKeyDown(inputEnum.enterEquip))
            {
                UIConfig.getController().useSelectSkillUI();
            }
        }
        else
        {
            //Salimos de la UI de seleccionar habilidad
            if (inputManager.GetKeyDown(inputEnum.enterEquip))
            {
                UIConfig.getController().useSelectSkillUI();
            }
        }
    }
}
