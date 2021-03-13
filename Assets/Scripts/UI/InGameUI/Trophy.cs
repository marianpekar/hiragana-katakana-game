using System;
using UnityEngine;
using UnityEngine.UI;

public class Trophy : MonoBehaviour
{
    private Image image;
    private bool isVisible;

    public bool IsVisible { 
        get => image.enabled; 
        private set => isVisible = value; 
    }

    public void Awake() {
        image = GetComponent<Image>();

        if(PersistenceManager.Instance.BestTime == TimeSpan.Zero) {
            Show(false);
        }
    }

    public void Show(bool show) {
        image.enabled = isVisible = show;
    }
}
