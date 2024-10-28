using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectiles : MonoBehaviour
{
    private Rigidbody2D projectile;

    void Start()
    {
        projectile = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Slime")
        {
            Destroy(gameObject);
        }
    }
}
