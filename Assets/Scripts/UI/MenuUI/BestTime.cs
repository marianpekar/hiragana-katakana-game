using System;
using UnityEngine;
using UnityEngine.UI;

public class BestTime : MonoBehaviour
{
    [Serializable]
    private class BestTimeUIElements
    {
        public Text text = null;
        public Text dash = null;
        public Button resetButton = null;
        public Image resetButtonImage = null;
    };

    [SerializeField]
    private BestTimeUIElements bestTimeUIElements = null;

    private TimeSpan bestTime = TimeSpan.Zero;

    private void Start()
    {
        bestTime = PersistenceManager.Instance.BestTime;

        if (bestTime != TimeSpan.Zero)
        {
            bestTimeUIElements.text.text = string.Format("{0:00}\n{1:00}", bestTime.Minutes, bestTime.Seconds);
            EnableUI(true);
        }
    }

    public void ResetBestTime()
    {
        PersistenceManager.Instance.BestTime = TimeSpan.Zero;
        EnableUI(false);
    }

    private void EnableUI(bool enable) {
        bestTimeUIElements.text.enabled = enable;
        bestTimeUIElements.dash.enabled = enable;
        bestTimeUIElements.resetButton.enabled = enable;
        bestTimeUIElements.resetButtonImage.enabled = enable;
    }
}
