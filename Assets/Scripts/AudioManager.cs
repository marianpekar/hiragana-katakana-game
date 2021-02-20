using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    private Dictionary<Sign, AudioClip> SignSounds = new Dictionary<Sign, AudioClip>();

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Array signs = Enum.GetValues(typeof(Sign));
        foreach(Sign sign in signs) {
            AudioClip sound = Resources.Load<AudioClip>($"Sounds/{sign}");
            SignSounds.Add(sign, sound);
        }
    }

    public void ReadSign(Sign sign) {
        audioSource.clip = SignSounds[sign];
        audioSource.Play();
    }
}
