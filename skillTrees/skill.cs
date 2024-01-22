using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "Nueva_habilidad", menuName = "Habilidades/Crear nueva habilidad")]
[System.Serializable]
public abstract class skill : MonoBehaviour
{
    [SerializeField] protected skill[] _skillsToUnlock;
    [SerializeField] protected GameObject[] _linkList;
    private void Start()
    {

        unlockedSkillsData data = saveSystem.loadSkillsState();

        if (data != null)
        {
            int currentID = UIConfig.getController().gameObject.GetComponent<abilityTreeUIController>().getCurrentUI().GetComponent<generalItem>().getID();
            sceneSkillsState currentSkill = data.getUnlockedSkills().Find(skill => skill.getWeaponID() == currentID && skill.getAssociatedSkill().getIsUnlocked() && skill.getAssociatedSkill().getSkillID() == getSkillID());

            if (currentSkill != null)
            {
                setIsUnlocked(true);
                changeLinksColors();
            }
        }
    }
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

    public abstract skillData getData();

    public void unlockSkill()
    {
        if (isUnlockable())
        {
            setIsUnlocked(true);
            config.getPlayer().GetComponent<combatController>().useSouls(getSkillPoints());
            changeLinksColors();
            UIConfig.getController().gameObject.GetComponent<abilityTreeUIController>().unlockSkill(this);

        }
    }

    public void changeLinksColors()
    {
        foreach (GameObject link in getLinks())
        {
            link.GetComponent<Image>().color = Color.yellow;
        }
    }

    public skillType getType()
    {
        return getData().getType();
    }

    public string getSkillName()
    {
        return getData().getSkillName();
    }
    public Sprite getSkillSprite()
    {
        return getData().getSkillSprite();
    }

    public bool getIsUnlocked()
    {
        return getData().getIsUnlocked();
    }

    public int getSkillPoints()
    {
        return getData().getSkillPoints();
    }

    public bool getCanBeUnlocked()
    {
        return getData().getCanBeUnlocked();
    }
    public int getSkillID()
    {
        return getData().getSkillID();
    }

    public string getSkillDescription()
    {
        return getData().getSkillDescription();
    }
    public skill[] getSkillsToUnlock()
    {
        return _skillsToUnlock;
    }

    public GameObject[] getLinks()
    {
        return _linkList;
    }


    public void setCanBeUnlocked(bool value)
    {
        getData().setCanBeUnlocked(value);
    }

    public void setIsUnlocked(bool value)
    {
        getData().setIsUnlocked(value);
    }
}