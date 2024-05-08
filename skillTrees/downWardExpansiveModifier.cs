using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// downWardExpansiveModifier es una clase que se usa para modificar varios par�metros de la onda expansiva.
/// </summary>
public class downWardExpansiveModifier : MonoBehaviour
{
    /// <summary>
    /// M�todo que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        //Obtenemos los datos de la habilidad asociada
        statusSkillData skillData = GetComponent<statusSkill>().getData() as statusSkillData;

        //Podemos crear onda expansiva y adem�s se modifican los da�os
        config.getPlayer().GetComponent<downWardBlowController>().setCanCreateExpansive(true);
        config.getPlayer().GetComponent<downWardBlowController>().addDamages(skillData.getCritDamage(), skillData.getPenetrationDamage(), skillData.getBaseDamage());
    }
}
