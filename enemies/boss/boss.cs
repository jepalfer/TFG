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
    /// Daño que ha recibido para ponerlo en la UI como feedback visual.
    /// </summary>
    private float _receivedDamage;

    /// <summary>
    /// Booleano para saber cuándo podemos modificar el campo de texto de daño.
    /// </summary>
    private bool _isReceivingDamage;

    /// <summary>
    /// Campo de texto de daño recibido.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _receivedDamageText;

    /// <summary>
    /// Timer para saber cuánto tiempo llevamos sin recibir daño;
    /// </summary>
    private float _timerNotReceivingDamage;

    /// <summary>
    /// Tiempo máximo para recibir daño.
    /// </summary>
    private float _maximumTimeNotReceivingDamage;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
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
    /// Método que implementa <see cref="enemy.receiveDMG(float, bool, bool)"/>.
    /// </summary>
    /// <param name="dmg">Es el daño que recibe el jefe.</param>
    /// <param name="critDamage">El daño crítico.</param>
    /// <param name="penetrationDamage">La armadura que eliminamos.</param>
    /// <param name="bleedingDamage">El daño por sangrado.</param>
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
    /// Método que se ejecuta cada frame para actualizar la lógica.
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
