using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// downWardBlowSkill es una clase que se usa para controlar el ataque picado.
/// </summary>
public class downWardBlowSkill : MonoBehaviour
{
    /// <summary>
    /// Capa de suelo.
    /// </summary>
    [SerializeField] private LayerMask _groundLayer;

    /// <summary>
    /// Capa de plataforma.
    /// </summary>
    [SerializeField] private LayerMask _ladderTopLayer;

    /// <summary>
    /// Capa de plataforma.
    /// </summary>
    [SerializeField] private LayerMask _oneWayLayer;

    /// <summary>
    /// Capa de rampa.
    /// </summary>
    [SerializeField] private LayerMask _slopeLayer;

    /// <summary>
    /// Capa de enemigos.
    /// </summary>
    [SerializeField] private LayerMask _enemiesLayer;

    /// <summary>
    /// Capa de obstáculo rompible.
    /// </summary>
    [SerializeField] private LayerMask _breakableWall;

    /// <summary>
    /// Lista con los IDs internos de los enemigos para no golpearlos más de una vez.
    /// </summary>
    private List<int> _enemiesID;

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        //Si no estamos tocando suelo ni en medio de una esquiva
        if (!config.getPlayer().GetComponent<collisionController>().getIsOnPlatform() && !config.getPlayer().GetComponent<playerMovement>().getIsDodging())
        {
            //Comprobamos inputs
            if (inputManager.GetKey(inputEnum.down) && inputManager.GetKeyDown(inputEnum.primaryAttack) && !config.getPlayer().GetComponent<downWardBlowController>().getIsInDownWardBlow())
            {
                _enemiesID = new List<int>();

                //Modificamos la velocidad del jugador
                config.getPlayer().GetComponent<playerMovement>().getRigidBody().velocity = new Vector2(0f, -10f);
                config.getPlayer().GetComponent<downWardBlowController>().setIsInDownWardBlow(true);
                doTerrainRayCast();
                config.getPlayer().GetComponent<combatController>().getDownWardHitbox().enabled = true;
            }
        }

        //Si estamos en medio de un ataque picado
        if (config.getPlayer().GetComponent<downWardBlowController>().getIsInDownWardBlow())
        {
            doEnemiesRayCast();
            doBreakableRayCast();
        }
    }

    /// <summary>
    /// Método que traza rayos para los obstáculos rompibles.
    /// </summary>
    private void doBreakableRayCast()
    {
        //Cálculo de la posición inicial del rayo
        Vector3 initialPos = new Vector3(config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2),
                                 config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.y - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.y / 2),
                                 1.0f);

        //Trazamos el rayo
        float rayDistance = 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(initialPos, Vector2.down, rayDistance, _breakableWall);

        //Comprobamos la colisión
        if (hit.collider != null && config.getPlayer().GetComponent<downWardBlowController>().getAttackHeight() == heightEnum.strong)
        {
            hit.collider.GetComponent<breakableWallBehaviour>().destroyWall();
        }
    }

    /// <summary>
    /// Método que traza rayos para los enemigos.
    /// </summary>
    private void doEnemiesRayCast()
    {

        //Debug.DrawRay(initialPos, Vector2.down, Color.black);
        //Debug.DrawRay(finalPos, Vector2.down, Color.black);
        //Calculamos la distancia entre el número de rayos
        int step = 5;
        float distanceBetweenRays = (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x + (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2)) - 
                                    (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2));
        distanceBetweenRays = distanceBetweenRays / step;

        //Trazamos todos los rayos
        for (int i = 0; i < step; i++)
        {
            //Calculamos la posición inicial de la que parte el rayo
            Vector3 initialPos = new Vector3(config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2),
                                 config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.y - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.y / 2),
                                 1.0f);

            //Vector3 finalPos = new Vector3(config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x + (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2),
            //                     config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.y - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.y / 2),
            //                     1.0f);

            Vector3 initialRayPosition = new Vector3(initialPos.x + (distanceBetweenRays * i), initialPos.y);

            Debug.DrawRay(initialRayPosition, Vector2.down, Color.red);

            //Trazamos cada rayo
            float rayDistance = 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(initialRayPosition, Vector2.down, rayDistance, _enemiesLayer);

            //Comprobamos la colisión
            if (hit.collider != null && _enemiesID.FindIndex(index => index == hit.collider.gameObject.GetComponent<enemy>().getEnemyID()) == -1)
            {
                //Añadimos el ID del enemigo
                _enemiesID.Add(hit.collider.gameObject.GetComponent<enemy>().getEnemyID());

                //Calculamos el daño del ataque picado
                float bleed = 0f, penetration = 0f, crit = 0f;
                config.getPlayer().GetComponent<combatController>().calculateExtraDamages(ref penetration, ref bleed, ref crit);
                
                //Modificamos el daño según la altura del ataque
                if (config.getPlayer().GetComponent<downWardBlowController>().getAttackHeight() == heightEnum.weak)
                {
                    hit.collider.gameObject.GetComponent<enemy>().receiveDMG(weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getTotalDMG() / 4.0f, crit, penetration, bleed);
                }
                else
                {
                    hit.collider.gameObject.GetComponent<enemy>().receiveDMG(weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getTotalDMG() / 2.0f, crit, penetration, bleed);
                }
            }
        }

    }

    /// <summary>
    /// Método que traza el rayo para el terreno.
    /// </summary>
    private void doTerrainRayCast()
    {
        //Posición donde se origina el rayo
        Vector3 origin = config.getPlayer().transform.position - Vector3.up * (config.getPlayer().GetComponent<playerMovement>().getBoxCollider().size.y / 2f);

        // Lanzamos el rayo hacia abajo
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, Mathf.Infinity, _groundLayer | _ladderTopLayer | _oneWayLayer | _slopeLayer | _breakableWall);

        //Comprobamos las distancias para saber si es un ataque fuerte o débil
        if (hit && ((config.getPlayer().transform.position.y - hit.point.y) >= 10.0f))
        {
            config.getPlayer().GetComponent<downWardBlowController>().setAttackHeight(heightEnum.strong);
        }
        else
        {
            config.getPlayer().GetComponent<downWardBlowController>().setAttackHeight(heightEnum.weak);
        }
    }
}
