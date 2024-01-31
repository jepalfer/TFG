using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderInteraction : MonoBehaviour
{
    float _gravity;
    Vector2 _newVelocity, _velocity0;
    // Start is called before the first frame update
    void Start()
    {
        _velocity0 = new Vector2(0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            /*newVelocity = player.GetComponent<PlayerMovement>().getRigidBodyVelocity();

            if (!player.GetComponent<PlayerMovement>().getDodging())
            {
                
                player.GetComponent<PlayerMovement>().setRigidBodyVelocity(velocity0);

            }
            player.GetComponent<PlayerMovement>().setGravity(gravity0);

            if (newVelocity.y < 0)
            {
                if (!player.GetComponent<PlayerMovement>().getDodging())
                {
                    player.GetComponent<PlayerMovement>().setRigidBodyVelocity(newVelocity);
                    player.GetComponent<PlayerMovement>().setGravity(gravity);
                }
            }
*/
            if (!config.getPlayer().GetComponent<playerMovement>().getIsDodging())
            {
                config.getPlayer().GetComponent<playerMovement>().setCanClimb(true);
                config.getPlayer().GetComponent<playerMovement>().setCouldClimb(true);

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _newVelocity = config.getPlayer().GetComponent<playerMovement>().getRigidBodyVelocity();
            _gravity = config.getPlayer().GetComponent<playerMovement>().getGravity();

            if (!config.getPlayer().GetComponent<playerMovement>().getIsDodging())
            {
                config.getPlayer().GetComponent<playerMovement>().setGravity(_gravity);

                config.getPlayer().GetComponent<playerMovement>().setRigidBodyVelocity(_newVelocity);
            }
            config.getPlayer().GetComponent<playerMovement>().setCanClimb(false);
            config.getPlayer().GetComponent<playerMovement>().setCouldClimb(false);

            if (!config.getPlayer().GetComponent<playerMovement>().getHasRolled())
            {
                config.getPlayer().GetComponent<playerMovement>().setCanRoll(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!config.getPlayer().GetComponent<playerMovement>().getIsDodging())
            {
                config.getPlayer().GetComponent<playerMovement>().setCanClimb(true);
                config.getPlayer().GetComponent<playerMovement>().setCouldClimb(true);
            }

            if (config.getPlayer().GetComponent<combatController>().getIsAttacking())
            {
                config.getPlayer().GetComponent<playerMovement>().setCanClimb(false);
                config.getPlayer().GetComponent<playerMovement>().setCouldClimb(false);
            }
        }
    }
}
