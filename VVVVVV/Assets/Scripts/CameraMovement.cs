using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public Vector2 minPos, maxPos;

    void Update()
    {
        float posX = player.transform.position.x;
        float posY = player.transform.position.y;
        transform.position = new Vector3(Mathf.Clamp(posX, minPos.x, maxPos.x),Mathf.Clamp(posY, minPos.y, maxPos.y),transform.position.z);
    }
}
