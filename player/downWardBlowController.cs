using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// downWardBlowController es una clase que se ocupa de manejar la lógica del ataque en picado.
/// </summary>
public class downWardBlowController : MonoBehaviour
{
    /// <summary>
    /// Flag booleano para indicar si estamos o no realizando el ataque.
    /// </summary>
    private bool _isInDownWardBlow;

    /// <summary>
    /// Valor para indicar la altura desde la que se ha realizado el golpe.
    /// </summary>
    private heightEnum _blowHeight;

    /// <summary>
    /// Valor para el daño crítico.
    /// </summary>
    private float _critDamage;

    /// <summary>
    /// Valor para el daño penetrante.
    /// </summary>
    private float _penetratingDamage;

    /// <summary>
    /// Valor para el daño base.
    /// </summary>
    private float _baseDamage;

    /// <summary>
    /// Flag booleano que indice si se puede o no crear la onda expansiva.
    /// </summary>
    private bool _canCreateExpansive;

    /// <summary>
    /// Referencia al prefab de la onda expansiva.
    /// </summary>
    [SerializeField] private GameObject _expansivePrefab;

    /// <summary>
    /// Método que se ejecuta al iniciar el script.
    /// </summary>
    private void Awake()
    {
        _canCreateExpansive = false;
        _critDamage = 0f;
        _penetratingDamage = 0f;
        _baseDamage = 0f;
    }

    /// <summary>
    /// Método que se ejecuta al iniciar el script tras el <see cref="Awake()"/>.
    /// </summary>
    private void Start()
    {
        _blowHeight = heightEnum.none;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isInDownWardBlow"/>.
    /// </summary>
    /// <returns> <see cref="_isInDownWardBlow"/></returns>
    public bool getIsInDownWardBlow()
    {
        return _isInDownWardBlow;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_blowHeight"/>.
    /// </summary>
    /// <returns> <see cref="_blowHeight"/></returns>
    public heightEnum getAttackHeight()
    {
        return _blowHeight;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_baseDamage"/>.
    /// </summary>
    /// <returns> <see cref="_baseDamage"/></returns>
    public float getBaseDamage()
    {
        return _baseDamage;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_penetratingDamage"/>.
    /// </summary>
    /// <returns> <see cref="_penetratingDamage"/></returns>
    public float getPenDamage()
    {
        return _penetratingDamage;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_critDamage"/>.
    /// </summary>
    /// <returns> <see cref="_critDamage"/></returns>
    public float getCritDamage()
    {
        return _critDamage;
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        //Si nos encontramos en un suelo
        if (config.getPlayer().GetComponent<collisionController>().getIsOnPlatform())
        {
            //Si hemos ejecutado un ataque fuerte (desde bastante alto) y además se puede crear onda expansiva
            if (_canCreateExpansive && _blowHeight == heightEnum.strong && !config.getPlayer().GetComponent<collisionController>().getIsOnBreakable())
            {
                //Calculamos la posición de la onda izquierda
                Vector2 leftHitPos = new Vector2(transform.position.x - GetComponent<playerMovement>().getBoxCollider().size.x, 
                                                 transform.position.y - (GetComponent<playerMovement>().getBoxCollider().size.y / 2) - 
                                                 (Mathf.Abs(GetComponent<playerMovement>().getBoxCollider().offset.y / 2)));

                //Calculamos la posición de la onda derecha
                Vector2 rightHitPos = new Vector2(transform.position.x + GetComponent<playerMovement>().getBoxCollider().size.x, 
                                                  transform.position.y - (GetComponent<playerMovement>().getBoxCollider().size.y / 2) - 
                                                  (Mathf.Abs(GetComponent<playerMovement>().getBoxCollider().offset.y / 2)));
                
                //Instanciamos las ondas izquierda y derecha
                GameObject leftHit = Instantiate(_expansivePrefab, leftHitPos, Quaternion.identity);
                leftHit.GetComponent<expansiveController>().setGoingLeft(true);
                
                GameObject rightHit = Instantiate(_expansivePrefab, rightHitPos, Quaternion.identity);
                rightHit.GetComponent<expansiveController>().setGoingLeft(false);
            }
            _blowHeight = heightEnum.none;
            GetComponent<downWardBlowController>().setIsInDownWardBlow(false);
            GetComponent<combatController>().getDownWardHitbox().enabled = false;
        }
    }

    /// <summary>
    /// Setter que modifica <see cref="_canCreateExpansive"/>.
    /// </summary>
    /// <param name="value">Valor a asignar.</param>
    public void setCanCreateExpansive(bool value)
    {
        _canCreateExpansive = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_isInDownWardBlow"/>.
    /// </summary>
    /// <param name="value">Valor a asignar.</param>
    public void setIsInDownWardBlow(bool value)
    {
        _isInDownWardBlow = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_blowHeight"/>.
    /// </summary>
    /// <param name="height">Valor a asignar.</param>
    public void setAttackHeight(heightEnum height)
    {
        _blowHeight = height;
    }

    /// <summary>
    /// Método auxiliar para añadir los daños correspondientes.
    /// </summary>
    /// <param name="critDamage">Daño crítico.</param>
    /// <param name="penDamage">Daño penetrante.</param>
    /// <param name="baseDamage">Daño de sangrado.</param>
    public void addDamages(float critDamage, float penDamage, float baseDamage)
    {
        _critDamage += critDamage;
        _baseDamage += baseDamage;
        _penetratingDamage += penDamage;
    }
}
