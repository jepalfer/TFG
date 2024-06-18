using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemyHitController es una clase que controla si un enemigo recibe daño cuando entra en contacto con la hurtbox del jugador.
/// </summary>
public class enemyHitController : MonoBehaviour
{
    /// <summary>
    /// La referencia al enemigo.
    /// </summary>
    [SerializeField] private GameObject _enemy;

    /// <summary>
    /// Flag booleano que indica que el ataque ya ha golpeado al jugador.
    /// </summary>
    private bool _hasHitPlayer;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        _hasHitPlayer = false;
    }

    /// <summary>
    /// Método que se ejecuta cuando un trigger entra en contacto y hace que el enemigo reciba daño.
    /// </summary>
    /// <param name="collision">Collider que ha entrado en contacto.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerHurtbox"))
        {
            Debug.Log("hurtbox");
            //config.getPlayer().GetComponent<statsController>().receiveDMG(_enemy.GetComponent<enemy>().getDamage());
            //config.getPlayer().GetComponent<characterSFXController>().playHurtSFX();
        }
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        //Debug.Log("Hitbox activada con hitplayer = " + _hasHitPlayer);
        int step = 5;
        float distanceBetweenRays = (transform.position.x + (GetComponent<BoxCollider2D>().size.x / 2)) -
                                    (transform.position.x - (GetComponent<BoxCollider2D>().size.x / 2));
        distanceBetweenRays = distanceBetweenRays / step;

        float rayDistance = GetComponent<BoxCollider2D>().size.x;
        //Trazamos todos los rayos
        for (int i = 0; i <= step; i++)
        {
            //Calculamos la posición inicial de la que parte el rayo
            Vector3 initialPos = new Vector3(transform.position.x - (GetComponent<BoxCollider2D>().size.x / 2) -
                                        (Mathf.Abs(GetComponent<BoxCollider2D>().offset.x / 2)),
                                         transform.position.y + (GetComponent<BoxCollider2D>().size.y / 2),
                                         1.0f);
            Vector3 initialRayPosition = new Vector3(initialPos.x, initialPos.y - (distanceBetweenRays * i));

            Debug.DrawRay(initialRayPosition, Vector2.right, Color.red);

            ////Trazamos cada rayo
            RaycastHit2D hit = Physics2D.Raycast(initialRayPosition, Vector2.right, rayDistance, _enemy.GetComponent<enemy>().getHurtboxLayer());
            //Debug.Log(hit.collider);
            //Comprobamos la colisión
            //Debug.Log("Debug");
            
            if (hit.collider != null && !_hasHitPlayer)
            {
                //Debug.Log("Rayo " + i + " ha chocado");
                //Añadimos el ID del enemigo
                _hasHitPlayer = true;

                config.getPlayer().GetComponent<statsController>().receiveDMG(_enemy.GetComponent<enemy>().getDamage());
                config.getPlayer().GetComponent<characterSFXController>().playHurtSFX();
            }
        }
    }

    /// <summary>
    /// Setter que modifica <see cref="_hasHitPlayer"/>.
    /// </summary>
    /// <param name="flag">Flag booleano a asignar.</param>
    public void setHasHitPlayer(bool flag)
    {
        _hasHitPlayer = flag;
    }
}
