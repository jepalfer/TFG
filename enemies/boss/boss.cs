using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// boss es una clase que representa a un tipo de enemigo especial.
/// </summary>
public class boss : enemy
{
    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// Carga los datos del enemigo y modifica si debe estar activo o no.
    /// </summary>
    private void Start()
    {
        //Cargamos los datos
        enemyStateData data = saveSystem.loadEnemyData();

        if (data != null)
        {
            //Lo desactivamos si ha muerto
            if (data.getEnemyStates().Find(enemy => enemy.getSceneID() == SceneManager.GetActiveScene().buildIndex && enemy.getEnemyID() == getEnemyID()).getIsAlive() == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Método que implementa <see cref="enemy.receiveDMG(float, bool, bool)"/>.
    /// </summary>
    /// <param name="dmg">Es el daño que recibe el jefe.</param>
    /// <param name="isCrit">Si el golpe es crítico.</param>
    /// <param name="piercesArmor">Si el golpe penetra armadura.</param>
    public override void receiveDMG(float dmg, bool isCrit, bool piercesArmor)
    {
        GetComponent<bossUIController>().recalculateHPBar(dmg);
        base.receiveDMG(dmg, isCrit, piercesArmor);
    }
}
