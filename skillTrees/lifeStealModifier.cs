using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ligeStealModifier es una clase que se usa para modificar el robo de vida que da una habilidad.
/// </summary>
public class lifeStealModifier : MonoBehaviour
{
    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        //Obtenemos los datos de la habilidad y ajustamos el robo de vida
        lifeStealSkillData skillData = GetComponent<lifeStealSkill>().getData() as lifeStealSkillData;
        config.getPlayer().GetComponent<combatController>().setLifeSteal(config.getPlayer().GetComponent<combatController>().getLifeSteal() + skillData.getLifeSteal());
    }

    /// <summary>
    /// Método que se ejecuta al destruir el prefab.
    /// </summary>
    private void OnDestroy()
    {
        config.getPlayer().GetComponent<combatController>().setLifeSteal(0);
    }
}
