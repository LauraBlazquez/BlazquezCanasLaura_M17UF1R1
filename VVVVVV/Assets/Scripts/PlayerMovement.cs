using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public LayerMask floor;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool lookingLeft = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        Walking();
        Jumping();  
    }

    bool OnFloor()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x,boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, floor);
        return raycastHit.collider != null;
    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnFloor())
        {
            rb.gravityScale *= -1;
            rb.GetComponent<SpriteRenderer>().flipY = rb.gravityScale < 0; 
        }
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
