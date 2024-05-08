using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// expansiveController es una clase que controla la onda expansiva del ataque en picado.
/// </summary>
public class expansiveController : MonoBehaviour
{
    /// <summary>
    /// Flag que indica si la parte de la onda expansiva va hacia la izquierda (true) o derecha (false).
    /// </summary>
    [SerializeField] private bool _isGoingLeft;

    /// <summary>
    /// Float que almacena la distancia recorrida por la onda expansiva.
    /// </summary>
    private float _distanceDone;
    
    /// <summary>
    /// Float que almacena la velocidad de la onda expansiva.
    /// </summary>
    private float _velocity;

    /// <summary>
    /// Float que almacena la distancia total que va a recorrer la onda expansiva.
    /// </summary>
    private float _totalDistance;

    /// <summary>
    /// Lista con los IDs de los enemigos que ha golpeado la onda expansiva para evitar un doble hit.
    /// </summary>
    private List<int> _enemiesID;

    /// <summary>
    /// Int que almacena la dirección en la que va la onda expansiva para trazar hacia un lado u otro el rayo.
    /// </summary>
    private int _direction;

    /// <summary>
    /// Capa de los enemigos para dañarlos.
    /// </summary>
    [SerializeField] private LayerMask _enemiesLayer;

    /// <summary>
    /// Capa de los obstáculos rompibles para destruirlos.
    /// </summary>
    [SerializeField] private LayerMask _breakableLayer;

    /// <summary>
    /// Vector3 que almacena la posición de la onda expansiva para ir moviéndola.
    /// </summary>
    [SerializeField] private Vector3 _newPos;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        //Inicializamos las variables
        _distanceDone = 0f;
        _totalDistance = 1.75f;
        _velocity = 6f;

        if (_isGoingLeft)
        {
            _velocity *= -1;
        }

        _enemiesID = new List<int>();
    }

    /// <summary>
    /// Setter que modifica <see cref="_isGoingLeft"/>.
    /// </summary>
    /// <param name="val">Flag booleano que indica la dirección de la onda.</param>
    public void setGoingLeft(bool val)
    {
        _isGoingLeft = val;
    }

    /// <summary>
    /// Método que se ejecuta cada frame para ejecutar la lógica.
    /// </summary>
    private void Update()
    {
        //La onda expansiva no ha recorrido la distancia total
        if (_distanceDone < _totalDistance)
        {
            //Desplazamos la onda expansiva
            float displacement = _velocity * Time.deltaTime;
            if (displacement + _distanceDone > _totalDistance)
            {
                displacement = _totalDistance - _distanceDone;
            }
            _distanceDone += Mathf.Abs(displacement);
            _newPos = gameObject.transform.position;
            _newPos.x += displacement;

            transform.position = _newPos;

            //Trazamos los rayos
            doRayCasting();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Método auxiliar para hacer el raycasting.
    /// </summary>
    private void doRayCasting ()
    {
        //Asignamos la dirección del rayo
        if (_isGoingLeft)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
        //Número de rayos que se van a lanzar
        int step = 5;

        //Calculamos la distancia entre los rayos
        float distanceBetweenRays = (transform.position.y + ((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2)) -
                                    (transform.position.y - ((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2));

        distanceBetweenRays = distanceBetweenRays / step;

        //Creamos todos los rayos
        for (int i = 0; i <= step; i++)
        {
            //Posición donde el rayo se inicia
            Vector3 initialPos = new Vector3(transform.position.x + (_direction * ((GetComponent<BoxCollider2D>().size.x * transform.localScale.x) / 2)),
                                             transform.position.y + ((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2),
                                             1.0f);

            //Vector3 finalPos = new Vector3(transform.position.x + (_direction * ((GetComponent<BoxCollider2D>().size.x * transform.localScale.x) / 2)),
            //                               transform.position.y - ((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2),
            //                               1.0f);

            Debug.DrawLine(initialPos, initialPos - _direction * new Vector3((GetComponent<BoxCollider2D>().size.x * transform.localScale.x), 0, 0), Color.blue);
            Vector3 initialRayPosition = new Vector3(initialPos.x, initialPos.y - (distanceBetweenRays * i));

            Debug.DrawRay(initialRayPosition, _direction * Vector2.left, Color.red);
                
            //Calculamos la distancia del rayo
            float rayDistance = (GetComponent<BoxCollider2D>().size.x * transform.localScale.x);
            RaycastHit2D hit = Physics2D.Raycast(initialRayPosition, _direction * Vector2.left, rayDistance, _enemiesLayer | _breakableLayer);

            //Comprobamos que el rayo haya colisionado
            if (hit.collider != null)
            {
                //Si es un enemigo
                if (hit.collider.gameObject.GetComponent<enemy>() != null)
                {
                    //Buscamos al enemigo
                    if (_enemiesID.FindIndex(index => index == hit.collider.gameObject.GetComponent<enemy>().getEnemyID()) == -1)
                    {
                        //Añadimos el ID a la lista 
                        _enemiesID.Add(hit.collider.gameObject.GetComponent<enemy>().getEnemyID());

                        //Calculamos el daño total de la onda expansiva
                        float bleed = 0f, penetration = config.getPlayer().GetComponent<downWardBlowController>().getPenDamage(), crit = config.getPlayer().GetComponent<downWardBlowController>().getCritDamage();
                        config.getPlayer().GetComponent<combatController>().calculateExtraDamages(ref penetration, ref bleed, ref crit);

                        //Golpeamos al enemigo
                        hit.collider.gameObject.GetComponent<enemy>().receiveDMG(config.getPlayer().GetComponent<downWardBlowController>().getBaseDamage(), crit, penetration, 0);
                    }
                }
                else if (hit.collider.gameObject.GetComponent<breakableWallBehaviour>() != null) //Si el rayo ha golpeado un obstáculo rompible
                {
                    hit.collider.gameObject.GetComponent<breakableWallBehaviour>().destroyWall();
                }
            }
        }
    }
}
