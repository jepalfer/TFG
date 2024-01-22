using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHitController : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerHurtbox"))
        {
            config.getPlayer().GetComponent<statsController>().receiveDMG(_enemy.GetComponent<enemy>().getDamage());
        }
    }

}
