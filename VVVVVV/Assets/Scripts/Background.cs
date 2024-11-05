using UnityEngine;

public class Background : MonoBehaviour
{
    public Vector2 movementSpeed;

    private Vector2 offset;
    private Material material;
    private Rigidbody2D player;
    private Vector2 lastPosition;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        player = GameObject.Find("Ranita").GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = player.GetComponent<Rigidbody2D>();
        lastPosition = player.position;
    }

    void Update()
    {
        offset = (player.velocity.x / 10) * movementSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
