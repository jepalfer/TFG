using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpablePlatform : MonoBehaviour
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

        if (config.getPlayer().GetComponent<collisionController>().getIsOnOneWay() && inputManager.getKey(inputEnum.Down.ToString()) && inputManager.getKey(inputEnum.Jump.ToString()))
        {
            _bc.enabled = false;
            _playerJumpedOff = true;
            config.getPlayer().GetComponent<playerMovement>().setIsLookingDown(false);
            config.getPlayer().GetComponent<playerMovement>().setCanMove(true);
            config.getPlayer().GetComponent<playerMovement>().setCanRoll(true);
        }


        /*
        if (!Config.getPlayer().GetComponent<CollisionController>().getIsOnOneWay())
        {
            Physics2D.IgnoreCollision(_bc, Config.getPlayer().GetComponent<BoxCollider2D>());
        }

        if (Config.getPlayer().GetComponent<CollisionController>().getIsOnOneWay() && !_playerJumpedOff && (Config.getPlayer().GetComponent<CollisionController>().getIsOnOneWay()))
        {
            _playerJumpedOff = false;
            Physics2D.IgnoreCollision(_bc, Config.getPlayer().GetComponent<BoxCollider2D>(), false);
        }



        if (Config.getPlayer().GetComponent<CollisionController>().getIsOnOneWay() && inputManager.getKey("Down") && inputManager.getKey("Jump") && (Config.getPlayer().GetComponent<CollisionController>().getIsOnOneWay()))
        {
            Config.getPlayer().GetComponent<PlayerMovement>().setCanJump(false);
            Config.getPlayer().GetComponent<PlayerMovement>().setPassedThrough(true);
            Physics2D.IgnoreCollision(_bc, Config.getPlayer().GetComponent<BoxCollider2D>());
            _playerJumpedOff = true;
        }

        if (_playerJumpedOff)
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= 0.5f)
        {
            _playerJumpedOff = false;
            _timer = 0f;
            Config.getPlayer().GetComponent<PlayerMovement>().setPassedThrough(false);
        }*/
    }

    /*
    [SerializeField] private bool _playerJumpedOff = false;
    [SerializeField] private float _timer = 0f;
    [SerializeField] private bool _playerIsOn = false;

    [SerializeField] private PlatformEffector2D _platform;
    [SerializeField] private Collider2D _bc;

    // Start is called before the first frame update
    void Start()
    {
        _bc = GetComponent<Collider2D>();
        _bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _bc.enabled = false;
        if (Config.getPlayer().GetComponent<CollisionController>().getIsOnOneWay() && !_playerJumpedOff && _playerIsOn)
        {
            _playerJumpedOff = false;
            _bc.enabled = true;
        }

        if (Config.getPlayer().GetComponent<CollisionController>().getIsOnOneWay() && inputManager.getKey("Down") && inputManager.getKey("Jump") && _playerIsOn)
        {
            Config.getPlayer().GetComponent<PlayerMovement>().setCanJump(false);
            Config.getPlayer().GetComponent<PlayerMovement>().setPassedThrough(true);
            _bc.enabled = false;
            _playerJumpedOff = true;
        }

        if (_playerJumpedOff)
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= 0.5f)
        {
            _playerJumpedOff = false;
            _timer = 0f;
            Config.getPlayer().GetComponent<PlayerMovement>().setPassedThrough(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _playerIsOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _playerIsOn = false;
        }
    }*/
}
