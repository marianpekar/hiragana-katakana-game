using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField]
    private Button playButton = null;

    [SerializeField]
    private Button quitButton = null;

    [SerializeField]
    private Text bestTimeText = null;

    [SerializeField]
    private GameObject bestTimeGO =  null;

    private TimeSpan bestTime = TimeSpan.Zero;

    private void Awake()
    {
        bestTime = PersistenceManager.Instance.BestTime;
    }

    void Start()
    {
        playButton.onClick.AddListener(() => SceneLoader.LoadGame());
        quitButton.onClick.AddListener(() => Application.Quit());

        if (bestTime != TimeSpan.Zero)
        {
            bestTimeText.text = string.Format("{0:00}\n{1:00}", bestTime.Minutes, bestTime.Seconds);
            bestTimeGO.SetActive(true);
        }
    }
}
