using UnityEngine;

public class GameEvaluator : MonoBehaviour, IObserver
{
    [SerializeField]
    private GameManager gameManager = null;

    private int NumOfStonesOnBoard;

    private void Awake()
    {
        gameManager.RegisterObserver(this);
    }

    public void OnNotify(ISubject subject, ActionType actionType)
    {
        Stone stone = subject as Stone;
        if (stone)
        {
            NumOfStonesOnBoard--;
            Evaluate();
        }

        GameManager gameManager = subject as GameManager;
        if(gameManager) {
            if(actionType == ActionType.GameEnds) {
                Debug.Log("Game Ends");
            }
            else if (actionType == ActionType.GameStarts) {
                Debug.Log("Game Starts");
                NumOfStonesOnBoard = GameManager.GameStonesPairCount * 2;
            }
        }
    }

    private void Evaluate() {
        if(NumOfStonesOnBoard <= 0) {
            gameManager.EndGame();
        }
    }
}
