using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class skill : MonoBehaviour
{
    [SerializeField] protected skill[] _skillsToUnlock;
    [SerializeField] protected GameObject[] _linkList;
    protected Dictionary<skillValuesEnum, float> _skillValues;
    private void Start()
    {

        unlockedSkillsData data = saveSystem.loadSkillsState();

        if (data != null)
        {
            List<GameObject> UIs = UIConfig.getController().getSkillTreesUI().GetComponent<skillTreeUIController>().getAllUIs();
            for (int i = 0; i < UIs.Count; ++i)
            {
                int currentID = UIs[i].GetComponent<generalItem>().getID(); 
                sceneSkillsState currentSkill = data.getUnlockedSkills().Find(skill => skill.getWeaponID() == currentID && skill.getAssociatedSkill().getIsUnlocked() && skill.getAssociatedSkill().getSkillID() == getSkillID());

                if (currentSkill != null)
                {
                    setIsUnlocked(true);
                    changeLinksColors();
                }
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

    public Dictionary<skillValuesEnum, float> getSkillValues()
    {
        return _skillValues;
    }
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

    public void changeLinksColors()
    {
        foreach (GameObject link in getLinks())
        {
            link.GetComponent<Image>().color = Color.yellow;
        }
    }

    public skillTypeEnum getType()
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