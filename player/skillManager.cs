using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// skillManager es una clase que se usa para gestionar las habilidades equipadas y desbloqueadas.
/// </summary>
public class skillManager : MonoBehaviour
{
    /// <summary>
    /// Lista con las referencias a las habilidades equipadas.
    /// </summary>
    [SerializeField] private List<GameObject> _equippedSkills;

    /// <summary>
    /// Lista formada por las habilidades desbloqueadas.
    /// </summary>
    [SerializeField] private List<sceneSkillsState> _unlockedSkills;
    
    /// <summary>
    /// Lista con las referencias a todas las habilidades del juego.
    /// </summary>
    [SerializeField] private List<GameObject> _allSkills;

    /// <summary>
    /// Método que se encarga de equipar una habilidad concreta en un slot concreto.
    /// </summary>
    /// <param name="skillToEquip">Habilidad a equipar.</param>
    /// <param name="index">Índice del slot donde se equipa la habilidad.</param>
    public void equipSkill(skillData skillToEquip, int index)
    {
        //Destruimos la habilidad del slot si existe
        if (_equippedSkills[index] != null)
        {
            Destroy(_equippedSkills[index]);
        }
        //Instanciamos la habilidad a la vez que la "equipamos"
        _equippedSkills[index] = Instantiate(getSkillToEquip(skillToEquip.getType(), skillToEquip.getSkillID()));
    }

    /// <summary>
    /// Getter que devuelve <see cref="_allSkills"/>.
    /// </summary>
    /// <returns><see cref="_allSkills"/>.</returns>
    public List<GameObject> getAllSkills()
    {
        return _allSkills;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_equippedSkills"/>.
    /// </summary>
    /// <returns><see cref="_equippedSkills"/>.</returns>
    public List<GameObject> getEquippedSkills()
    {
        return _equippedSkills;
    }

    /// <summary>
    /// Getter que devuelve una habilidad concreta según su tipo e ID.
    /// </summary>
    /// <param name="type">Tipo de la habilidad buscada.</param>
    /// <param name="id">ID interno de la habilidad.</param>
    /// <returns></returns>
    public GameObject getSkillToEquip(skillTypeEnum type, int id)
    {
        GameObject skill = _allSkills.Find(searchedSkill => searchedSkill.GetComponent<skill>().getType() == type && searchedSkill.GetComponent<skill>().getSkillID() == id);
        return skill;
    }

    /// <summary>
    /// Setter que modifica un valor concreto de <see cref="_equippedSkills"/>.
    /// </summary>
    /// <param name="val">Valor a asignar.</param>
    /// <param name="index">Índice a modificar.</param>
    public void setEquippedValue(GameObject val, int index)
    {
        _equippedSkills[index] = val;
    }

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    void Start()
    {
        //Obtenemos los datos de las habilidades equipadas.
        equippedSkillsData equippedData = saveSystem.loadEquippedSkillsState();
        _equippedSkills = new List<GameObject>();

        for (int i = 0; i < 3; ++i)
        {
            _equippedSkills.Add(null);
        }

        if (equippedData == null)
        {
        }
        else //Si hemos equipado alguna habilidad alguna vez
        {
            //Equipamos las habilidades que teníamos equipadas anteriormente
            for (int i = 0; i < 3; ++i)
            {
                if (equippedData.getIDs()[i] != -1)
                {
                    Destroy(_equippedSkills[i]);
                    _equippedSkills[i] = Instantiate(getSkillToEquip(equippedData.getTypes()[i], equippedData.getIDs()[i]));
                    if (_equippedSkills[i] != null)
                    {
                        UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSprites()[i].GetComponent<Image>().sprite = _equippedSkills[i].GetComponent<skill>().getSkillSprite();
                        
                        UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSprites()[i].SetActive(true);

                        UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getBackgroundImages()[i].gameObject.SetActive(true);
                        
                        if (_equippedSkills[i].GetComponent<skill>().getType() == skillTypeEnum.combo)
                        {
                            UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getBackgroundImages()[i].sprite = UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getComboColor();
                        }
                        else if (_equippedSkills[i].GetComponent<skill>().getType() == skillTypeEnum.status)
                        {
                            UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getBackgroundImages()[i].sprite = UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getStatusColor();
                        }
                        else if (_equippedSkills[i].GetComponent<skill>().getType() == skillTypeEnum.souls)
                        {
                            UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getBackgroundImages()[i].sprite = UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSoulsColor();
                        }
                    }
                }
            }
        }

        //Cargamos las habilidades desbloqueadas
        unlockedSkillsData data = saveSystem.loadSkillsState();
        if (data != null)
        {
            _unlockedSkills = data.getUnlockedSkills();
        }
    }
}
