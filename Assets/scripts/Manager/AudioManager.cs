using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]private AudioSource audioSource;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip BGM;

    private HashSet<AudioClip> audioCheck = new();
    private int lastFrameCount = -1;

    private void Awake()
    {
        Instance = this;
    }
    public void Playsound(AudioClip clip)
    {
        audioSource.pitch = 1f + Random.Range(-0.1f, 0.1f);
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
        if(bgmSource.isPlaying == false)
        {
            bgmSource.clip = BGMClip;
            bgmSource.Play();
        }
    }
    // ‚¢‚Á‚½‚ñ‚±‚±‚ÅBGM Update
    private void Update()
    {
        PlayBGM(BGM);
    }
}
