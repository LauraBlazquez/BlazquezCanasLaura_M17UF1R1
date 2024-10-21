using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private bool lookingLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Walking();
    }

    void Walking()
    {
        float movementInput = Input.GetAxis("Horizontal");
        if (movementInput != 0)
        {
            rb.GetComponent<Animator>().SetBool("isWalking", true);
        }
        else
        {
            rb.GetComponent<Animator>().SetBool("isWalking", false);
        }
        rb.velocity = new Vector2(movementInput*speed,rb.velocity.y);
        Flip(movementInput);
    }

    void Flip(float movement)
    {
        if ((lookingLeft == false && movement < 0) || (lookingLeft == true && movement > 0))
        {
            lookingLeft = !lookingLeft;
            transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
        }
    }
}
