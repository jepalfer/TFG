using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ladderTopInteraction es una clase que se encarga de manejar la lógica de las plataformas que se ponen encima de las escaleras.
/// </summary>
public class ladderTopInteraction : MonoBehaviour
{
    /// <summary>
    /// Flag booleano para saber cuándo ha pasado el jugador.
    /// </summary>
    [SerializeField] private bool _playerPassedOff = false;

    /// <summary>
    /// Timer para no activar el BoxCollider2D antes de tiempo.
    /// </summary>
    [SerializeField] private float _timer = 0f;

    /// <summary>
    /// Timer para contar el tiempo que el jugador ha estado pulsando hacia abajo.
    /// </summary>
    [SerializeField] private float _crouchTime = 0f;

    /// <summary>
    /// Límite de tiempo con el que se activa la plataforma tras estar pulsando hacia abajo.
    /// </summary>
    private float _timeLimit = 0.5f;

    /// <summary>
    /// Referencia al BoxCollider de la plataforma.
    /// </summary>
    [SerializeField] private BoxCollider2D _bc;

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica
    /// </summary>
    void Update()
    {
        //Si el jugador no ha utilizado la plataforma
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
        else // si no, comenzamos a contar
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= 0.25f)
        {
            _timer = 0;
            _playerPassedOff = false;
        }
        
        //Comenzamos a contar el tiempo que el jugador está agachado
        if (inputManager.GetKey(inputEnum.down) && config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop())
        {
            _crouchTime += Time.deltaTime;
        }

        //Reseteamos este contador si se ha dejado de pulsar hacia abajo
        if (inputManager.GetKeyUp(inputEnum.down) && config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop() && 
            _crouchTime < _timeLimit)
        {
            _crouchTime = 0f;
        }

        //Desactivamos el BoxCollider si ha pasado el tiempo límite
        if (config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop() && inputManager.GetKey(inputEnum.down) && 
            _crouchTime >= _timeLimit)
        {
            _bc.enabled = false;
            _playerPassedOff = true;
            _crouchTime = 0f;
        }

       
    }
}
