using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField]private AudioSource audioSource;
    private HashSet<AudioClip> audioCheck = new();
    private int lastFrameCount = -1;

    private void Awake()
    {
        Instance = this;
    }
    public void Playsound(AudioClip clip)
    {
        if (lastFrameCount !=Time.frameCount) {
            audioCheck.Clear();
            lastFrameCount = Time.frameCount;
        }

        if (audioCheck.Contains(clip))
        {
            return;
        }
        audioSource.PlayOneShot(clip);
        audioCheck.Add(clip);
    }
}
