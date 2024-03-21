using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downWardBlowSkill : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _ladderTopLayer;
    [SerializeField] private LayerMask _oneWayLayer;
    [SerializeField] private LayerMask _slopeLayer;
    [SerializeField] private LayerMask _enemiesLayer;
    [SerializeField] private LayerMask _breakableWall;

    private List<int> _enemiesID;
    // Update is called once per frame
    void Update()
    {
        if (!config.getPlayer().GetComponent<collisionController>().getIsOnPlatform() && !config.getPlayer().GetComponent<playerMovement>().getIsDodging())
        {
            if (inputManager.GetKey(inputEnum.down) && inputManager.GetKeyDown(inputEnum.primaryAttack) && !config.getPlayer().GetComponent<downWardBlowController>().getIsInDownWardBlow())
            {
                _enemiesID = new List<int>();
                config.getPlayer().GetComponent<playerMovement>().getRigidBody().velocity = new Vector2(0f, -10f);
                config.getPlayer().GetComponent<downWardBlowController>().setIsInDownWardBlow(true);
                doTerrainRayCast();
                config.getPlayer().GetComponent<combatController>().getDownWardHitbox().enabled = true;
            }
        }

        if (config.getPlayer().GetComponent<downWardBlowController>().getIsInDownWardBlow())
        {
            doEnemiesRayCast();
            doBreakableRayCast();
        }
    }

    private void doBreakableRayCast()
    {
        Vector3 initialPos = new Vector3(config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2),
                                 config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.y - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.y / 2),
                                 1.0f);
        float rayDistance = 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(initialPos, Vector2.down, rayDistance, _breakableWall);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<breakableWallBehaviour>().destroyWall();
        }
    }

    private void doEnemiesRayCast()
    {

        //Debug.DrawRay(initialPos, Vector2.down, Color.black);
        //Debug.DrawRay(finalPos, Vector2.down, Color.black);
        int step = 5;
        float distanceBetweenRays = (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x + (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2)) - 
                                    (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2));

        distanceBetweenRays = distanceBetweenRays / step;
        for (int i = 0; i < step; i++)
        {
            Vector3 initialPos = new Vector3(config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2),
                                 config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.y - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.y / 2),
                                 1.0f);

            Vector3 finalPos = new Vector3(config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.x + (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.x / 2),
                                 config.getPlayer().GetComponent<combatController>().getDownWardHitbox().transform.position.y - (config.getPlayer().GetComponent<combatController>().getDownWardHitbox().size.y / 2),
                                 1.0f);

            Vector3 initialRayPosition = new Vector3(initialPos.x + (distanceBetweenRays * i), initialPos.y);

            Debug.DrawRay(initialRayPosition, Vector2.down, Color.red);
            float rayDistance = 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(initialRayPosition, Vector2.down, rayDistance, _enemiesLayer);

            if (hit.collider != null && _enemiesID.FindIndex(index => index == hit.collider.gameObject.GetComponent<enemy>().getEnemyID()) == -1)
            {
                _enemiesID.Add(hit.collider.gameObject.GetComponent<enemy>().getEnemyID());
                float bleed = 0f, penetration = 0f, crit = 0f;
                config.getPlayer().GetComponent<combatController>().calculateExtraDamages(ref penetration, ref bleed, ref crit);
                
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

    private void doTerrainRayCast()
    {

        Vector3 origin = config.getPlayer().transform.position - Vector3.up * (config.getPlayer().GetComponent<playerMovement>().getBoxCollider().size.y / 2f);

        // Lanzamos el rayo hacia abajo
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, Mathf.Infinity, _groundLayer | _ladderTopLayer | _oneWayLayer | _slopeLayer | _breakableWall);

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
