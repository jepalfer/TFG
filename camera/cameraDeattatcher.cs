using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// cameraDeattatcher es una clase usada para desasignar la cámara del jugador.
/// </summary>
public class cameraDeattatcher : MonoBehaviour
{
    [SerializeField] private bool _isOnLeftSide;
    [SerializeField] private bool _isOnRightSide;
    [SerializeField] private bool _isOnTopSide;
    [SerializeField] private bool _isOnBottomSide;
    /// <summary>
    /// Método que se ejecuta al entrar en contacto con el trigger del GameObject.
    /// </summary>
    /// <param name="collision">Colisión que entra en contacto.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es jugador
        if (collision.gameObject.GetComponent<playerMovement>() != null)
        {
            config.getPlayer().GetComponent<playerMovement>().getCamera().GetComponent<cameraController>().setCanMove(false);
            config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.parent = null;
            
            //Colocamos la cámara a la derecha del GameObject que contiene el script

            if (_isOnLeftSide)
            {
                config.getPlayer().GetComponent<playerMovement>().getCamera().GetComponent<cameraController>().setCanMoveUpDown(true);
                Vector3 cameraPos = new Vector3(transform.position.x + (GetComponent<BoxCollider2D>().size.x / 2 * transform.localScale.x) +
                                                GetComponent<BoxCollider2D>().offset.x + (config.getPlayer().GetComponent<BoxCollider2D>().size.x / 2),
                                                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position.y,
                                                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position.z);

                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position = cameraPos;
            }
            else if (_isOnRightSide)
            {
                config.getPlayer().GetComponent<playerMovement>().getCamera().GetComponent<cameraController>().setCanMoveUpDown(true);
                Vector3 cameraPos = new Vector3(transform.position.x - (GetComponent<BoxCollider2D>().size.x / 2 * transform.localScale.x) +
                                                GetComponent<BoxCollider2D>().offset.x - (config.getPlayer().GetComponent<BoxCollider2D>().size.x / 2),
                                                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position.y,
                                                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position.z);

                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position = cameraPos;
            }
            else if (_isOnTopSide)
            {
                config.getPlayer().GetComponent<playerMovement>().getCamera().GetComponent<cameraController>().setCanMoveLeftRight(true);
                Vector3 cameraPos = new Vector3(config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position.x,
                                                config.getPlayer().transform.position.y,
                                                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position.z);

                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position = cameraPos;
            }
            else if (_isOnBottomSide)
            {
                config.getPlayer().GetComponent<playerMovement>().getCamera().GetComponent<cameraController>().setCanMoveLeftRight(true);
                Vector3 cameraPos = new Vector3(config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position.x,
                                                config.getPlayer().transform.position.y,
                                                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position.z);

                config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.position = cameraPos;
            }
        }
    }

    /// <summary>
    /// Método que se ejecuta al salir del trigger del GameObject.
    /// </summary>
    /// <param name="collision">Colisión que sale.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si es jugador
        if (collision.gameObject.GetComponent<playerMovement>() != null)
        {
            config.getPlayer().GetComponent<playerMovement>().getCamera().GetComponent<cameraController>().setCanMove(true);
            config.getPlayer().GetComponent<playerMovement>().getCamera().GetComponent<cameraController>().setCanMoveLeftRight(false);
            config.getPlayer().GetComponent<playerMovement>().getCamera().GetComponent<cameraController>().setCanMoveUpDown(false);
            config.getPlayer().GetComponent<playerMovement>().getCamera().gameObject.transform.parent = config.getPlayer().gameObject.transform;
        }
    }
}
