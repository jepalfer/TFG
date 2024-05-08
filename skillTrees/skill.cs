using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// skill es una clase que se usa para representar internamente las habilidades dentro de un árbol de habilidades.
/// </summary>
[System.Serializable]
public abstract class skill : MonoBehaviour
{
    /// <summary>
    /// Habilidades que necesitan ser desbloqueadas previamente.
    /// </summary>
    [SerializeField] protected skill[] _skillsToUnlock;

    /// <summary>
    /// Lista de enlaces que salen de la habilidad desbloqueada.
    /// </summary>
    [SerializeField] protected GameObject[] _linkList;

    /// <summary>
    /// Diccionario con los valores que aumenta la habilidad.
    /// </summary>
    protected Dictionary<skillValuesEnum, float> _skillValues;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        //Cargamos los datos de habilidades desbloqueadas
        unlockedSkillsData data = saveSystem.loadSkillsState();

        if (data != null) //Si ya tenemos alguna habilidad
        {
            //Obtenemos todos los árboles instanciados
            List<GameObject> UIs = UIConfig.getController().getSkillTreesUI().GetComponent<skillTreeUIController>().getAllUIs();
            for (int i = 0; i < UIs.Count; ++i)
            {
                //Obtenemos cada una de las UI
                int currentID = UIs[i].GetComponent<generalItem>().getID(); 
                
                //Obtenemos la habilidad actual
                sceneSkillsState currentSkill = data.getUnlockedSkills().Find(skill => skill.getWeaponID() == currentID && skill.getAssociatedSkill().getIsUnlocked() && skill.getAssociatedSkill().getSkillID() == getSkillID());

                //Hacemos que la habilidad esté desbloqueada
                if (currentSkill != null)
                {
                    setIsUnlocked(true);
                    changeLinksColors();
                }
            }
            
        }
    }

    /// <summary>
    /// Método que determina si una habilidad puede desbloquearse.
    /// </summary>
    /// <returns>Un flag booleano que indica si se puede desbloquear (true) o no (false).</returns>
    public bool isUnlockable()
    {
        bool allSkillsUnlocked = true;

        foreach (skill skill in getSkillsToUnlock())
        {
            if (!skill.getIsUnlocked())
            {
                allSkillsUnlocked = false;
                break;
            }
        }

        return !getIsUnlocked() && config.getPlayer().GetComponent<combatController>().getSouls() >= getSkillPoints() && allSkillsUnlocked;
    }

    /// <summary>
    /// Getter abstracto que cada habilidad va a implementar para devolver sus datos internos concretos.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="skillData"/>.</returns>
    public abstract skillData getData();

    /// <summary>
    /// Getter que devuelve <see cref="_skillValues"/>.
    /// </summary>
    /// <returns><see cref="_skillValues"/>.</returns>
    public Dictionary<skillValuesEnum, float> getSkillValues()
    {
        return _skillValues;
    }

    /// <summary>
    /// Método del evento onClick de las habilidades.
    /// </summary>
    public void unlockSkill()
    {
        if (isUnlockable())
        {
            setIsUnlocked(true);
            config.getPlayer().GetComponent<combatController>().useSouls(getSkillPoints());
            changeLinksColors();
            UIConfig.getController().getSkillTreesUI().GetComponent<skillTreeUIController>().unlockSkill(this);

        }
    }

    /// <summary>
    /// Método auxiliar que cambia el color a todos los links que salen de la habilidad.
    /// </summary>
    public void changeLinksColors()
    {
        foreach (GameObject link in getLinks())
        {
            link.GetComponent<Image>().color = Color.yellow;
        }
    }

    /// <summary>
    /// Getter que devuelve el tipo de la habilidad.
    /// </summary>
    /// <returns><see cref="skillData.getType()"/>.</returns>
    public skillTypeEnum getType()
    {
        return getData().getType();
    }

    /// <summary>
    /// Getter que devuelve el nombre de la habilidad.
    /// </summary>
    /// <returns><see cref="skillData.getSkillName()"/>.</returns>
    public string getSkillName()
    {
        return getData().getSkillName();
    }

    /// <summary>
    /// Getter que devuelve el sprite de la habilidad.
    /// </summary>
    /// <returns><see cref="skillData.getSkillSprite()"/>.</returns>
    public Sprite getSkillSprite()
    {
        return getData().getSkillSprite();
    }

    /// <summary>
    /// Getter que devuelve si una habilidad está desbloqueada.
    /// </summary>
    /// <returns><see cref="skillData.getIsUnlocked()"/>.</returns>
    public bool getIsUnlocked()
    {
        return getData().getIsUnlocked();
    }

    /// <summary>
    /// Getter que devuelve el precio de la habilidad.
    /// </summary>
    /// <returns><see cref="skillData.getSkillPoints()"/>.</returns>
    public int getSkillPoints()
    {
        return getData().getSkillPoints();
    }

    /// <summary>
    /// Getter que devuelve si una habilidad puede ser desbloqueada.
    /// </summary>
    /// <returns><see cref="skillData.getCanBeUnlocked()"/>.</returns>
    public bool getCanBeUnlocked()
    {
        return getData().getCanBeUnlocked();
    }

    /// <summary>
    /// Getter que devuelve el ID interno de la habilidad.
    /// </summary>
    /// <returns><see cref="skillData.getSkillID()"/>.</returns>
    public int getSkillID()
    {
        return getData().getSkillID();
    }

    /// <summary>
    /// Getter que devuelve la descripción de la habilidad.
    /// </summary>
    /// <returns><see cref="skillData.getSkillDescription()"/>.</returns>
    public string getSkillDescription()
    {
        return getData().getSkillDescription();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_skillsToUnlock"/>.
    /// </summary>
    /// <returns><see cref="_skillsToUnlock"/>.</returns>
    public skill[] getSkillsToUnlock()
    {
        return _skillsToUnlock;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_linkList"/>.
    /// </summary>
    /// <returns><see cref="_linkList"/>.</returns>
    public GameObject[] getLinks()
    {
        return _linkList;
    }

    /// <summary>
    /// Setter que modifica si una habilidad puede ser desbloqueada.
    /// </summary>
    /// <param name="value">Valor a asignar.</param>
    public void setCanBeUnlocked(bool value)
    {
        getData().setCanBeUnlocked(value);
    }

    /// <summary>
    /// Setter que modifica si una habilidad está desbloqueada.
    /// </summary>
    /// <param name="value">Valor a asignar.</param>
    public void setIsUnlocked(bool value)
    {
        getData().setIsUnlocked(value);
    }
}