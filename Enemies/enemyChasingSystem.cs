using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChasingSystem : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _sightDistance;
    // Start is called before the first frame update
    void Start()
    {
        _player = config.getPlayer();
        _sightDistance = GetComponent<enemy>().getDetectionRange();
        Physics2D.IgnoreCollision(_player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= _sightDistance && Vector3.Distance(transform.position, _player.transform.position) >= (_player.GetComponent<BoxCollider2D>().size.x))
        {
            if (transform.position.x < _player.transform.position.x)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + (_movementSpeed * Time.deltaTime), gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + (-_movementSpeed * Time.deltaTime), gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }
}
