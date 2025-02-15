using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour, IObserver
{
    [SerializeField]
    GameManager gameManager = null;

    [SerializeField]
    Button reloadButton = null;

    [SerializeField]
    Button exitButton = null;

    void Awake()
    {
        gameManager.RegisterObserver(this);

        reloadButton.onClick.AddListener(gameManager.RestartGame);
        exitButton.onClick.AddListener(() => SceneLoader.LoadMenu());
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
