using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Portals : MonoBehaviour
{
    [SerializeField] Tilemap portal;
    public int indexScene;

    private void Start()
    {
        indexScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (portal.name == "PortalForward")
            {
                indexScene++;
                PlayerMovement.forward = true;

            }
            if (portal.name == "PortalBack")
            {
                indexScene--;
                PlayerMovement.forward = false;
            }
            SceneManager.LoadScene(indexScene);
        }
    }
}
