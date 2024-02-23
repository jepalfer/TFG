using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downWardExpansiveModifier : MonoBehaviour
{
    private void Start()
    {
        statusSkillData skillData = GetComponent<statusSkill>().getData() as statusSkillData;

        config.getPlayer().GetComponent<downWardBlowController>().setCanCreateExpansive(true);
        config.getPlayer().GetComponent<downWardBlowController>().addDamages(skillData.getCritDamage(), skillData.getPenetrationDamage(), skillData.getBaseDamage());
    }
}
