using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ladderTopInteraction : MonoBehaviour
{
    [SerializeField] private bool _playerPassedOff = false;
    [SerializeField] private float _timer = 0f;
    [SerializeField] private float _crouchTime = 0f;
    private float _timeLimit = 0.5f;

    [SerializeField] private BoxCollider2D _bc;

    // Start is called before the first frame update
    void Start()
    {
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
        
        if (inputManager.GetKey(inputEnum.down) && config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop())
        {
            _crouchTime += Time.deltaTime;
        }

        if (inputManager.GetKeyUp(inputEnum.down) && config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop() && _crouchTime < _timeLimit)
        {
            _crouchTime = 0f;
        }


        if (config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop() && inputManager.GetKey(inputEnum.down) && _crouchTime >= _timeLimit)
        {
            _bc.enabled = false;
            _playerPassedOff = true;
            _crouchTime = 0f;
        }

       
    }
}
