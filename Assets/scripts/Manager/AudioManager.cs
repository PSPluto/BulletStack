using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField]private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    public void Playsound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
