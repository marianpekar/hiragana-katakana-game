using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IObserver
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    Button reloadButton;

    void Awake()
    {
        gameManager.RegisterObserver(this);

        reloadButton.onClick.AddListener(gameManager.RestartGame);
    }

    public void OnNotify(ISubject subject, ActionType actionType = ActionType.Unspeficied)
    {
        switch (actionType)
        {
            case ActionType.GameStarts:
                reloadButton.interactable = false;
                break;
            case ActionType.GameEnds:
                reloadButton.interactable = true;
                break;
            default:
                break;
        }
    }

}
