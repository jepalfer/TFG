using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderTopInteraction : MonoBehaviour
{/*
    [SerializeField] private bool _playerPassedOff = false;
    [SerializeField] private float _timer = 0f;
    [SerializeField] private float _crouchTime = 0f;

    [SerializeField] private BoxCollider2D _bc;
    [SerializeField] private PlatformEffector2D _platform;

    // Start is called before the first frame update
    void Start()
    {
        _bc = GetComponent<BoxCollider2D>();
        _bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _bc.enabled = false;
        if (Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop() && !_playerPassedOff)
        {
            _playerPassedOff = false;
            _bc.enabled = true;
            Config.getPlayer().GetComponent<PlayerMovement>().setCanGoDown(false);
        }

        if (inputManager.getKey("Down") && Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop())
        {
            _crouchTime += Time.deltaTime;
        }

        if (inputManager.getKeyUp("Down")  && Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop() && _crouchTime < 0.25f)
        {
            _crouchTime = 0f;
        }

        if (Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop() && inputManager.getKey("Down") && _crouchTime >= 0.25f)
        {
            _bc.enabled = false;
            _playerPassedOff = true;
            _crouchTime = 0f;
            Config.getPlayer().GetComponent<PlayerMovement>().setCanGoDown(true);

            if (Config.getPlayer().GetComponent<PlayerMovement>().getCanClimb())
            {
                Config.getPlayer().GetComponent<PlayerMovement>().setGravity(0f);
                Config.getPlayer().GetComponent<PlayerMovement>().setRigidBodyVelocity(new Vector2(0, 0));
            }
        }

        if (_playerPassedOff)
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= 0.25f)
        {
            _playerPassedOff = false;
            _timer = 0f;
        }
    }*/
    [SerializeField] private bool _playerPassedOff = false;
    [SerializeField] private float _timer = 0f;
    [SerializeField] private float _crouchTime = 0f;
    private float _timeLimit = 0.5f;

    [SerializeField] private BoxCollider2D _bc;

    // Start is called before the first frame update
    void Start()
    {
        _bc = GetComponent<BoxCollider2D>();
        //Physics2D.IgnoreCollision(_bc, Config.getPlayer().GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerPassedOff)
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
            _playerPassedOff = false;
        }
        
        if (inputManager.getKey(inputEnum.Down.ToString()) && config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop())
        {
            _crouchTime += Time.deltaTime;
        }

        if (inputManager.getKeyUp(inputEnum.Down.ToString()) && config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop() && _crouchTime < _timeLimit)
        {
            _crouchTime = 0f;
        }


        if (config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop() && inputManager.getKey(inputEnum.Down.ToString()) && _crouchTime >= _timeLimit)
        {
            _bc.enabled = false;
            _playerPassedOff = true;
            _crouchTime = 0f;
        }

        /*        if ((Config.getPlayer().transform.position.y) > (gameObject.transform.position.y + Config.getPlayer().GetComponent<BoxCollider2D>().size.y / 2))
                {
                    _bc.enabled = true;
                }
                else
                {
                    _bc.enabled = false;
                }

                if (inputManager.getKey("Down") && Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop())
                {
                    _crouchTime += Time.deltaTime;
                }


                if (inputManager.getKeyUp("Down") && Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop() && _crouchTime < 0.25f)
                {
                    _crouchTime = 0f;
                }


                if (Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop() && inputManager.getKey("Down") && _crouchTime >= 0.25f)
                {
                    _bc.enabled = false;
                }*/

        /*
        if (!Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop())
        {
            Physics2D.IgnoreCollision(_bc, Config.getPlayer().GetComponent<BoxCollider2D>());
        }

        if (Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop() && !_playerPassedOff)
        {
            Debug.Log("e");
            _playerPassedOff = false;
            Physics2D.IgnoreCollision(_bc, Config.getPlayer().GetComponent<BoxCollider2D>(), false);
            Config.getPlayer().GetComponent<PlayerMovement>().setCanGoDown(false);
        }
        if (inputManager.getKey("Down") && Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop())
        {
            _crouchTime += Time.deltaTime;
        }
        if (inputManager.getKeyUp("Down") && Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop() && _crouchTime < 0.25f)
        {
            _crouchTime = 0f;
        }


        if (Config.getPlayer().GetComponent<CollisionController>().getIsOnLadderTop() && inputManager.getKey("Down") && _crouchTime >= 0.25f)
        {
            Debug.Log("e");
            Physics2D.IgnoreCollision(_bc, Config.getPlayer().GetComponent<BoxCollider2D>());
            _playerPassedOff = true;
            _crouchTime = 0f;
            Config.getPlayer().GetComponent<PlayerMovement>().setCanGoDown(true);

            if (Config.getPlayer().GetComponent<PlayerMovement>().getCanClimb())
            {
                Config.getPlayer().GetComponent<PlayerMovement>().setGravity(0f);
                Config.getPlayer().GetComponent<PlayerMovement>().setRigidBodyVelocity(new Vector2(0, 0));
            }
        }

        if (_playerPassedOff)
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= 0.25f)
        {
            _playerPassedOff = false;
            _timer = 0f;
        }*/
    }
}
