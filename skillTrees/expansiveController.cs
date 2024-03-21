using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expansiveController : MonoBehaviour
{
    [SerializeField] private bool _isGoingLeft;
    private float _distanceDone;
    private float _velocity;
    private float _totalDistance;
    private List<int> _enemiesID;
    private int _direction;
    [SerializeField] private LayerMask _enemiesLayer;
    [SerializeField] private LayerMask _breakableLayer;

    [SerializeField] private Vector3 _newPos;
    private void Start()
    {
        _distanceDone = 0f;
        _totalDistance = 1.75f;
        _velocity = 6f;

        if (_isGoingLeft)
        {
            _velocity *= -1;
        }

        _enemiesID = new List<int>();
    }

    public void setGoingLeft(bool val)
    {
        _isGoingLeft = val;
    }

    private void Update()
    {
        if (_distanceDone < _totalDistance)
        {
            float displacement = _velocity * Time.deltaTime;
            if (displacement + _distanceDone > _totalDistance)
            {
                displacement = _totalDistance - _distanceDone;
            }
            _distanceDone += Mathf.Abs(displacement);
            _newPos = gameObject.transform.position;
            _newPos.x += displacement;

            transform.position = _newPos;
            doRayCasting();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void doRayCasting ()
    {
        if (_isGoingLeft)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
        int step = 5;

        float distanceBetweenRays = (transform.position.y + ((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2)) -
                                    (transform.position.y - ((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2));

        distanceBetweenRays = distanceBetweenRays / step;
        for (int i = 0; i <= step; i++)
        {
            Vector3 initialPos = new Vector3(transform.position.x + (_direction * ((GetComponent<BoxCollider2D>().size.x * transform.localScale.x) / 2)),
                                             transform.position.y + ((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2),
                                             1.0f);

            Vector3 finalPos = new Vector3(transform.position.x + (_direction * ((GetComponent<BoxCollider2D>().size.x * transform.localScale.x) / 2)),
                                           transform.position.y - ((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2),
                                           1.0f);

            Debug.DrawLine(initialPos, initialPos - _direction * new Vector3((GetComponent<BoxCollider2D>().size.x * transform.localScale.x), 0, 0), Color.blue);
            Vector3 initialRayPosition = new Vector3(initialPos.x, initialPos.y - (distanceBetweenRays * i));

            Debug.DrawRay(initialRayPosition, _direction * Vector2.left, Color.red);
            float rayDistance = (GetComponent<BoxCollider2D>().size.x * transform.localScale.x);
            RaycastHit2D hit = Physics2D.Raycast(initialRayPosition, _direction * Vector2.left, rayDistance, _enemiesLayer | _breakableLayer);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<enemy>() != null)
                {
                    if (_enemiesID.FindIndex(index => index == hit.collider.gameObject.GetComponent<enemy>().getEnemyID()) == -1)
                    {

                        _enemiesID.Add(hit.collider.gameObject.GetComponent<enemy>().getEnemyID());
                        float bleed = 0f, penetration = config.getPlayer().GetComponent<downWardBlowController>().getPenDamage(), crit = config.getPlayer().GetComponent<downWardBlowController>().getCritDamage();
                        config.getPlayer().GetComponent<combatController>().calculateExtraDamages(ref penetration, ref bleed, ref crit);

                        hit.collider.gameObject.GetComponent<enemy>().receiveDMG(config.getPlayer().GetComponent<downWardBlowController>().getBaseDamage(), crit, penetration, 0);
                    }
                }
                else if (hit.collider.gameObject.GetComponent<breakableWallBehaviour>() != null)
                {
                    hit.collider.gameObject.GetComponent<breakableWallBehaviour>().destroyWall();
                }
            }
        }
    }
}
