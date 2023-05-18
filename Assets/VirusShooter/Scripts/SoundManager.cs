using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip squirtSound;
    [SerializeField] private AudioClip splatSound;

    void Awake()
    {
        Instance = this;
    }

    public void PlaySplatSound()
    {
        soundSource.PlayOneShot(splatSound);
    }

    public void PlaySquirtSound()
    {
        soundSource.PlayOneShot(squirtSound);
    }
}
