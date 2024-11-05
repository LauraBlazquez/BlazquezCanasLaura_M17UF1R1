using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement.instance = null;
            CameraMovement.instance = null;
            CanvasSingleton.instance = null;
            Destroy(GameObject.Find("Ranita"));
            Destroy(GameObject.Find("Main Camera"));
            Destroy(GameObject.Find("Canvas"));
            SceneManager.LoadScene("GameOver");
        }
    }
}
