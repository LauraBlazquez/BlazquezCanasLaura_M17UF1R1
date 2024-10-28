using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public LayerMask floor;
    public AudioClip jumpSound;

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
        RaycastHit2D[] raycastHitUp = Physics2D.RaycastAll(transform.position, Vector2.up, 1f);
        RaycastHit2D[] raycastHitDown = Physics2D.RaycastAll(transform.position, Vector2.down, 1f);
        return Array.Exists(raycastHitUp, x => x.collider.gameObject.name.Equals("Forest")) || Array.Exists(raycastHitDown, x => x.collider.gameObject.name.Equals("Forest"));
    }
    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnFloor())
        {
            rb.gravityScale *= -1;
            rb.GetComponent<SpriteRenderer>().flipY = rb.gravityScale < 0; 
            AudioManager.instance.PlaySound(jumpSound);
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
