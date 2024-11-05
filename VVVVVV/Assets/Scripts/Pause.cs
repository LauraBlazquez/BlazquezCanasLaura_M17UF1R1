using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject Menu;
    private bool activated = false;

    void Start()
    {
        Menu.SetActive(activated);   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activated = !activated;
        }
        Menu.SetActive(activated);
    }
}
