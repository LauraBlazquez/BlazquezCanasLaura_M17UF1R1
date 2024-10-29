using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public LayerMask floor;
    public AudioClip jumpSound;
    public static bool forward = true;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool lookingLeft = false;
    private static PlayerMovement instance;
    private Vector3 backPos, forwardPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        Walking();
        Jumping();  
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    bool OnFloor(float gravity)
    {
        Vector2 direction = gravity > 0 ? Vector2.up : Vector2.down;
        Vector2 boxCenter = (Vector2)transform.position + direction * 0.1f;
        Vector2 boxSize = new Vector2(0.5f, 0.1f);
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, direction, 0.5f);

        Vector2 topLeft = boxCenter + new Vector2(-boxSize.x / 2, boxSize.y / 2);
        Vector2 topRight = boxCenter + new Vector2(boxSize.x / 2, boxSize.y / 2);
        Vector2 bottomLeft = boxCenter + new Vector2(-boxSize.x / 2, -boxSize.y / 2);
        Vector2 bottomRight = boxCenter + new Vector2(boxSize.x / 2, -boxSize.y / 2);

        // Draw lines to show the box
        Debug.DrawLine(topLeft, topRight, Color.red);
        Debug.DrawLine(topRight, bottomRight, Color.red);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red);
        Debug.DrawLine(bottomLeft, topLeft, Color.red);



        return hit.collider != null;
    }
    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnFloor(rb.gravityScale))
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
    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject backPortal = GameObject.Find("Back");
        GameObject forwardPortal = GameObject.Find("Forward");

        if (backPortal != null)
        {
            backPos = backPortal.transform.position;
        }
        if (forwardPortal != null)
        {
            forwardPos = forwardPortal.transform.position;
        }
        if (forward)
        {
            transform.position = backPos;
        }
        if (!forward)
        {
            transform.position = forwardPos;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }
}
