using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField]
    private Button playButton = null;

    [SerializeField]
    private Button quitButton = null;

    [SerializeField]
    private Button resetBestTimeButton = null;

    [SerializeField]
    private BestTime bestTime = null;

    void Start()
    {
        playButton.onClick.AddListener(() => SceneLoader.LoadGame());
        quitButton.onClick.AddListener(() => Application.Quit());
        resetBestTimeButton.onClick.AddListener(() => bestTime.ResetBestTime());
    }
}
