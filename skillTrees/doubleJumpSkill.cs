using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJumpSkill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hey");
        config.getPlayer().GetComponent<playerMovement>().setCanDoubleJump(true);   
    }
    private void OnDestroy()
    {
        Debug.Log("adios");
        config.getPlayer().GetComponent<playerMovement>().setCanDoubleJump(false);
    }
}
