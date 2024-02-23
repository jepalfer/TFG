using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downWardBlowSkill : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _ladderTopLayer;
    [SerializeField] private LayerMask _oneWayLayer;
    [SerializeField] private LayerMask _slopeLayer;

    // Update is called once per frame
    void Update()
    {

        if (!config.getPlayer().GetComponent<collisionController>().getIsGrounded() && !config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop() &&
            !config.getPlayer().GetComponent<collisionController>().getIsOnOneWay() && !config.getPlayer().GetComponent<collisionController>().getIsOnSlope() && 
            !config.getPlayer().GetComponent<playerMovement>().getIsDodging())
        {
            if (inputManager.GetKey(inputEnum.down) && inputManager.GetKeyDown(inputEnum.primaryAttack))
            {
                config.getPlayer().GetComponent<playerMovement>().getRigidBody().velocity = new Vector2(0f, -10f);
                config.getPlayer().GetComponent<downWardBlowController>().setIsInDownWardBlow(true);
                doRayCast();
            }
        }
    }

    private void doRayCast()
    {

        Vector3 origin = config.getPlayer().transform.position - Vector3.up * (config.getPlayer().GetComponent<playerMovement>().getBoxCollider().size.y / 2f);

        // Lanzamos el rayo hacia abajo
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, Mathf.Infinity, _groundLayer | _ladderTopLayer | _oneWayLayer | _slopeLayer);

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
