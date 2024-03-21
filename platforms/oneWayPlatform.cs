using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneWayPlatform : MonoBehaviour
{

    [SerializeField] private bool _playerJumpedOff = false;
    [SerializeField] private float _timer = 0f;
    [SerializeField] private bool _playerIsOn = false;

    [SerializeField] private Collider2D _bc;

    // Start is called before the first frame update
    void Start()
    {
        _bc = GetComponent<Collider2D>();
        //Physics2D.IgnoreCollision(_bc, Config.getPlayer().GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerJumpedOff)
        {
            if (config.getPlayer().GetComponent<BoxCollider2D>().bounds.min.y >= _bc.bounds.max.y)
            {
                _bc.enabled = true;
            }
            else
            {
                _bc.enabled = false;
            }
        }
        else
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= 0.25f)
        {
            _timer = 0;
            _playerJumpedOff = false;
        }

        if (config.getPlayer().GetComponent<collisionController>().getIsOnOneWay() && inputManager.GetKey(inputEnum.down) && inputManager.GetKey(inputEnum.jump))
        {
            Debug.Log("salto");
            _bc.enabled = false;
            _playerJumpedOff = true;
            config.getPlayer().GetComponent<playerMovement>().setIsLookingDown(false);
            config.getPlayer().GetComponent<playerMovement>().setCanMove(true);
            config.getPlayer().GetComponent<playerMovement>().setCanRoll(true);
        }

    }

}
