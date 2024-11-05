using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioSource JumpSound;
    public AudioSource Respawn;
    public AudioSource Slime;
    public AudioSource Celtics;
    public static AudioManager instance;

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

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Celtics.Play();
    }

    public void PlaySoundJump()
    {
        JumpSound.Play();
    }

    public void PlaySoundSlime()
    {
        Slime.Play();
    }

    public void PlaySoundRespawn()
    {
        Respawn.Play();
    }
}
