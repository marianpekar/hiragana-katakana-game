using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Volume = AudioManager.Volume;

public class UIMusicVolumeController : MonoBehaviour
{
    [SerializeField] private Sprite volumeMute = null;
    [SerializeField] private Sprite volume25 = null;
    [SerializeField] private Sprite volume50 = null;
    [SerializeField] private Sprite volume75 = null;
    [SerializeField] private Sprite volumeMax = null;
    [SerializeField] private Button volumeButton = null;

    [SerializeField] 
    private AudioManager audioManager = null;

    [SerializeField]
    private PersistenceManager persistenceManager = null;

    private enum Direction {
        Up, 
        Down
    }

    private struct VolumeData {
        public Sprite Icon;
        public float Value;
    }

    private readonly Dictionary<Volume, VolumeData> volumeData = new Dictionary<Volume, VolumeData>();

    private Volume currentVolume;
    private Direction currentDirection = Direction.Down;

    private void Start()
    {
        volumeData.Add(Volume.Max,      new VolumeData { Icon = volumeMax,  Value = 1f   });
        volumeData.Add(Volume.Volume75, new VolumeData { Icon = volume75,   Value = .75f });
        volumeData.Add(Volume.Volume50, new VolumeData { Icon = volume50,   Value = .50f });
        volumeData.Add(Volume.Volume25, new VolumeData { Icon = volume25,   Value = .25f });
        volumeData.Add(Volume.Mute,     new VolumeData { Icon = volumeMute, Value = 0f   });

        volumeButton.onClick.AddListener(ChangeVolume);

        currentVolume = persistenceManager.Volume;
        SetVolume();
        UpdateUI();
    }

    private void ChangeVolume()
    {
        if (currentDirection == Direction.Down)
            currentVolume--;
        else if (currentDirection == Direction.Up)
            currentVolume++;

        if (currentVolume < Volume.Mute) {
            currentDirection = Direction.Up;
            currentVolume += 2;
        }
        else if (currentVolume > Volume.Max) {
            currentDirection = Direction.Down;
            currentVolume -= 2;
        }

        SetVolume();
        UpdateUI();
        persistenceManager.Volume = currentVolume;
    }

    private void UpdateUI() {
        volumeButton.image.sprite = volumeData[currentVolume].Icon;
    }

    private void SetVolume() {
        audioManager.SetMusicVolume(volumeData[currentVolume].Value);
    }
}
