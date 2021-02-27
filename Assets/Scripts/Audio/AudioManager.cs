using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IObserver
{
    [SerializeField]
    private float delayBeforeReadSign = 0.33f;

    [SerializeField]
    private AudioSource speechSource;

    [SerializeField]
    private AudioSource musicSource;

    private Dictionary<Sign, AudioClip> SignSounds = new Dictionary<Sign, AudioClip>();

    public void Start()
    {
        Array signs = Enum.GetValues(typeof(Sign));
        foreach(Sign sign in signs) {
            AudioClip sound = Resources.Load<AudioClip>($"Sounds/{sign}");
            SignSounds.Add(sign, sound);
        }
    }

    public void ReadSign() {
        speechSource.Play();
    }

    public void OnNotify(ISubject subject, ActionType actionType)
    {
        Stone stone = subject as Stone;
        if(stone) {
            speechSource.clip = SignSounds[stone.GetSign()];
            Invoke(nameof(ReadSign), delayBeforeReadSign);
        }
    }
}
