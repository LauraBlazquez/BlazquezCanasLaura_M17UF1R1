using UnityEngine;


public class CanvasSingleton : MonoBehaviour
{
    public static CanvasSingleton instance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
}
