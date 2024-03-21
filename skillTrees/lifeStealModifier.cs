using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeStealModifier : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        lifeStealSkillData skillData = GetComponent<lifeStealSkill>().getData() as lifeStealSkillData;
        config.getPlayer().GetComponent<combatController>().setLifeSteal(config.getPlayer().GetComponent<combatController>().getLifeSteal() + skillData.getLifeSteal());
    }

    private void OnDestroy()
    {
        config.getPlayer().GetComponent<combatController>().setLifeSteal(0);
    }
}
