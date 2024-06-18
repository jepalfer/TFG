using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bulletController es una clase que controla el comportamiento de las balas.
/// </summary>
public class bulletController : MonoBehaviour
{
    /// <summary>
    /// La distancia total que puede recorrer.
    /// </summary>
    [SerializeField] private float _totalDistance = 20f;

    /// <summary>
    /// La distancia que ha recorrido.
    /// </summary>
    [SerializeField] private float _travelledDistance;

    /// <summary>
    /// La velocidad a la que viaja la bala.
    /// </summary>
    [SerializeField] private float _travelVelocity;

    /// <summary>
    /// La nueva posición de la bala.
    /// </summary>
    [SerializeField] private Vector3 _newPos;

    /// <summary>
    /// Una capa que no debe destruir el objeto.
    /// </summary>
    [SerializeField] private LayerMask _bonfireLayer;

    /// <summary>
    /// Una capa que no debe destruir el objeto.
    /// </summary>
    [SerializeField] private LayerMask _ladderLayer;

    /// <summary>
    /// Una capa que no debe destruir el objeto.
    /// </summary>
    [SerializeField] private LayerMask _chestLayer;
    /// <summary>
    /// Una capa que no debe destruir el objeto.
    /// </summary>
    [SerializeField] private LayerMask _bulletLayer;

    /// <summary>
    /// Una capa que no debe destruir el objeto.
    /// </summary>
    [SerializeField] private LayerMask _hurtboxLayer;

    /// <summary>
    /// Una capa que no debe destruir el objeto.
    /// </summary>
    [SerializeField] private LayerMask _hitboxLayer;
    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        _travelledDistance = 0f;
        if (!config.getPlayer().GetComponent<playerMovement>().getIsFacingLeft())
        {
            _travelVelocity *= -1;
        }

    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        //La distancia del raycast
        float distance = GetComponent<BoxCollider2D>().size.x / 2 + 0.01f;

        //El desplazamiento de la bala
        float displace = _travelVelocity * Time.deltaTime;

        //Calculamos la dirección del raycast
        Vector2 raycastDirection = _travelVelocity > 0 ? Vector2.right : Vector2.left;
        raycastDirection.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, distance, (~_bonfireLayer & ~_ladderLayer & ~_bulletLayer & ~_hitboxLayer & ~_hurtboxLayer));

        //Comprobamos colisiones y distancia
        if (_travelledDistance >= _totalDistance || hit.collider != null)
        {
            if (hit.collider != null)
            {
                float bleed = 0f, penetration = 0f, crit = 0f;
                config.getPlayer().GetComponent<combatController>().calculateExtraDamages(ref penetration, ref bleed, ref crit);
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<enemy>() != null)
                {
                    hitObject.GetComponent<enemy>().receiveDMG(weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getTotalDMG(), crit, penetration, bleed);
                }
                else if (hitObject.GetComponent<boss>() != null)
                {
                    hitObject.GetComponent<boss>().receiveDMG(weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getTotalDMG(), crit, penetration, bleed);
                }

                if (hitObject.GetComponent<obstacleLogic>() != null)
                {
                    hitObject.GetComponent<obstacleLogic>().activateObstacle();
                }
            }
            Destroy(gameObject);
        }
        //Trasladamos la bala
        _travelledDistance += Mathf.Abs(displace);
        _newPos = gameObject.transform.position;
        _newPos.x += displace;

        transform.position = _newPos;
    }
}
