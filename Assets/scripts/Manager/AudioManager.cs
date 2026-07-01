using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]private AudioSource audioSource;
    [SerializeField] private AudioClip BGM;

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

    public void PlayBGM(AudioClip BGMClip)
    {
        if(audioSource.isPlaying == false)
        {
            audioSource.clip = BGMClip;
            audioSource.Play();
        }
    }
    // ‚¢‚Á‚½‚ñ‚±‚±‚ÅBGM Update
    private void Update()
    {
        PlayBGM(BGM);
    }
}
