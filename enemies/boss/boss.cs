using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// boss es una clase que representa a un tipo de enemigo especial.
/// </summary>
public class boss : enemy
{
    /// <summary>
    /// Da�o que ha recibido para ponerlo en la UI como feedback visual.
    /// </summary>
    private float _receivedDamage;

    /// <summary>
    /// Booleano para saber cu�ndo podemos modificar el campo de texto de da�o.
    /// </summary>
    private bool _isReceivingDamage;

    /// <summary>
    /// Campo de texto de da�o recibido.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _receivedDamageText;

    /// <summary>
    /// Timer para saber cu�nto tiempo llevamos sin recibir da�o;
    /// </summary>
    private float _timerNotReceivingDamage;

    /// <summary>
    /// Tiempo m�ximo para recibir da�o.
    /// </summary>
    private float _maximumTimeNotReceivingDamage;

    /// <summary>
    /// M�todo que se ejecuta al inicio del script.
    /// Carga los datos del enemigo y modifica si debe estar activo o no.
    /// </summary>
    private void Start()
    {
        _receivedDamage = 0f;
        _maximumTimeNotReceivingDamage = 1.5f;
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
    /// M�todo que implementa <see cref="enemy.receiveDMG(float, bool, bool)"/>.
    /// </summary>
    /// <param name="dmg">Es el da�o que recibe el jefe.</param>
    /// <param name="critDamage">El da�o cr�tico.</param>
    /// <param name="penetrationDamage">La armadura que eliminamos.</param>
    /// <param name="bleedingDamage">El da�o por sangrado.</param>
    public override void receiveDMG(float dmg, float critDamage, float penetrationDamage, float bleedingDamage)
    {
        float damageDealt = calculateDMG(dmg, critDamage, penetrationDamage, bleedingDamage);
        if (damageDealt >= 0)
        {
            GetComponent<bossUIController>().recalculateHPBar(damageDealt);
            base.receiveDMG(dmg, critDamage, penetrationDamage, bleedingDamage);
            _receivedDamage += damageDealt;
            _timerNotReceivingDamage = 0f;
            _receivedDamageText.enabled = true;
            _receivedDamageText.text = ((int)_receivedDamage).ToString();
        }
    }

    /// <summary>
    /// M�todo que se ejecuta cada frame para actualizar la l�gica.
    /// </summary>
    private void Update()
    {
        _timerNotReceivingDamage += Time.deltaTime;

        if (_timerNotReceivingDamage >= _maximumTimeNotReceivingDamage)
        {
            _receivedDamage = 0f;
            _receivedDamageText.enabled = false;
        }
    }
}
