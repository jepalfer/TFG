using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// oneWayPlatform es una clase que se encarga de manejar la lógica de las plataformas de tipo "one way".
/// </summary>
public class oneWayPlatform : MonoBehaviour
{
    /// <summary>
    /// Flag booleano para saber cuándo tenemos que desactivar la Collider.
    /// </summary>
    [SerializeField] private bool _playerJumpedOff = false;

    /// <summary>
    /// Timer para no activar la Collider antes de tiempo.
    /// </summary>
    [SerializeField] private float _timer = 0f;

    /// <summary>
    /// Referencia al Collider de la plataforma.
    /// </summary>
    [SerializeField] private Collider2D _bc;

    /// <summary>
    /// Método que se ejecuta al iniciar el script.
    /// </summary>
    void Start()
    {
        _bc = GetComponent<Collider2D>();
    }
    
    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica
    /// </summary>
    void Update()
    {
        //Si aún no hemos saltado
        if (!_playerJumpedOff)
        {
            //Activamos el collider si estamos por encima
            if (config.getPlayer().GetComponent<BoxCollider2D>().bounds.min.y > _bc.bounds.max.y && 
               !config.getPlayer().GetComponent<playerMovement>().getIsJumping())
            {
                Debug.Log("no estoy saltando");
                _bc.enabled = true;
            }
            else
            {
                _bc.enabled = false;
            }
        }
        else //Comenzamos a contar
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= 0.25f) //Si ha pasado 1 cuarto de segundo
        {
            _timer = 0;
            _playerJumpedOff = false;
        }
        //Se maneja la bajada de la plataforma
        if (config.getPlayer().GetComponent<collisionController>().getIsOnOneWay() && inputManager.GetKey(inputEnum.down) && 
            inputManager.GetKey(inputEnum.jump))
        {
            _bc.enabled = false;
            _playerJumpedOff = true;
            config.getPlayer().GetComponent<playerMovement>().setIsLookingDown(false);
            config.getPlayer().GetComponent<playerMovement>().setCanMove(true);
            config.getPlayer().GetComponent<playerMovement>().setCanRoll(true);
        }

    }

}
