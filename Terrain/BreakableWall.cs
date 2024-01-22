using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] float wallHP;
    [SerializeField] GameObject player;
    BoxCollider2D bc;
    float hp;

    public void receiveDamage(float damage)
    {
        hp -= damage; 
        
        if (hp <= 0)
        {
            breakWall();
        }

    }

    public void breakWall()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        hp = wallHP;
        bc = GetComponent<BoxCollider2D>();
    }
}
