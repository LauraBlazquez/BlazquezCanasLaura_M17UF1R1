using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    private static CameraMovement instance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        float posX = player.transform.position.x;
        float posY = player.transform.position.y;
        transform.position = new Vector3(posX,posY,transform.position.z);
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
}
