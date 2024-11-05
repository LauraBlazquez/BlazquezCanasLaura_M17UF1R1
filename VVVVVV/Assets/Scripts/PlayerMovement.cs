using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public LayerMask floor;
    public AudioClip jumpSound;
    public static bool forward = true;
    public static PlayerMovement instance;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool lookingLeft = false;
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

    void Awake()
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
        LayerMask groundLayer = LayerMask.GetMask("Floor");

        Vector2 positionRight = new Vector2(transform.position.x + 0.5f, transform.position.y);
        Vector2 positionLeft = new Vector2(transform.position.x - 0.5f, transform.position.y);

        RaycastHit2D[] raycastHitUp = Physics2D.RaycastAll(positionRight, Vector2.up, 1.0f);
        RaycastHit2D[] raycastHitDown = Physics2D.RaycastAll(positionRight, Vector2.down, 1.0f);
        RaycastHit2D[] raycastHitUp2 = Physics2D.RaycastAll(positionLeft, Vector2.up, 1.0f);
        RaycastHit2D[] raycastHitDown2 = Physics2D.RaycastAll(positionLeft, Vector2.down, 1.0f);

        return Array.Exists(raycastHitUp, x => x.collider.gameObject.name.Equals("Forest")) 
            || Array.Exists(raycastHitDown, x => x.collider.gameObject.name.Equals("Forest")) 
            || Array.Exists(raycastHitUp2, x => x.collider.gameObject.name.Equals("Forest")) 
            || Array.Exists(raycastHitDown2, x => x.collider.gameObject.name.Equals("Forest"));
    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnFloor(rb.gravityScale))
        {
            rb.gravityScale *= -1;
            rb.GetComponent<SpriteRenderer>().flipY = rb.gravityScale < 0;
            AudioManager.instance.PlaySoundJump();
        }
    }

    void Walking()
    { 
        if (rb.GetComponent<Animator>().GetBool("isDead") == false)
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
    }

    void Flip(float movement)
    {        
        if ((lookingLeft == false && movement < 0) || (lookingLeft == true && movement > 0))
        {
            lookingLeft = !lookingLeft;
            transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Finish")
        {
            if (forward)
            {
                transform.position = backPos;
            }
            if (!forward)
            {
                transform.position = forwardPos;
            }
            if (rb.gravityScale < 0)
            {
                rb.gravityScale *= -1;
                rb.GetComponent<SpriteRenderer>().flipY = rb.gravityScale < 0;
            }
            rb.velocity = new Vector2 (0, 0);
            rb.GetComponent<Animator>().SetBool("isDead", true);
            StartCoroutine(GettingHurt());
            AudioManager.instance.PlaySoundRespawn();
        }
    }

    private IEnumerator GettingHurt()
    {
        yield return new WaitForSeconds(1f);
        rb.GetComponent<Animator>().SetBool("isDead", false);
    }
}
