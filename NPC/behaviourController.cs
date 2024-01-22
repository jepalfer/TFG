using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviourController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 _velocity;
    float gravity = 2f;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _velocity = rb.velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ground == (ground | (1 << collision.gameObject.layer)))
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ground == (ground | (1 << collision.gameObject.layer)))
        {
            rb.gravityScale = gravity;
            rb.velocity = _velocity;
        }
    }

}
