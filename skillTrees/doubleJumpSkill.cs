using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJumpSkill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        config.getPlayer().GetComponent<playerMovement>().setCanDoubleJump(true);   
    }
    private void OnDestroy()
    {
        config.getPlayer().GetComponent<playerMovement>().setCanDoubleJump(false);
    }
}
